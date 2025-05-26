using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine.UI;


[System.Serializable]
public class ClothingDetection
{
    public string label;
    public float confidence;
    public List<float> bbox;
}

[System.Serializable]
public class CombinedDetectionData
{
    public List<ClothingDetection> clothes;
    public string hair;
}

public static class ClothingDetectionData
{
    public static List<ClothingDetection> CurrentClothes = new List<ClothingDetection>();
    public static string CurrentHairType = "";
}

[System.Serializable]
public class ColorData
{
    public string label;
    public string hex;
    public float[] rgb;
}


[System.Serializable]
public class CombinedColorData
{
    public List<ColorData> items;
}


public static class ClothingColorManager
{
    public static List<ColorData> CurrentColors = new List<ColorData>();
}

[System.Serializable]
public class ColorDataWrapper
{
    public List<ColorData> colors;
}

public static class JsonHelper
{
    public static T FromJson<T>(string json)
    {
        return JsonUtility.FromJson<T>(json);
    }
}

public class Client : MonoBehaviour
{
        TcpClient client;
        NetworkStream stream;
        string host = "127.0.0.1";
        int port = 65432;
        public RawImage rawImageToSend;
        public Transform imageContainer;
        private int checkmarkCount;
        private RawImage rawPeopleToSend;
        public RawImage displayImage;

        public GameObject hoomanManager;
        public LoadingScreen loadingScreen;

        private GameObject activeCheckmark;

        void Start()
        {
            ConnectToPython();
       
        }

        void OnApplicationQuit()
        {
            StopServer();
        }
    
        void ConnectToPython() {
            client = new TcpClient(host, port);
            stream = client.GetStream();
        }

        public void sendPeopleCommand()
        {
            try
            {
                byte[] commandBytes = Encoding.UTF8.GetBytes("peoplesend");
                stream.Write(commandBytes, 0, commandBytes.Length);
                Debug.Log("Image command sent");
                SendPeople();
            }
            catch (Exception e)
            {
                Debug.LogError($"Command error: {e.Message}");
            }
        }
        void SendPeople()
        {
            if (client == null || stream == null)
            {
                Debug.LogError("Not connected to server.");
                return;
            }

            // check if the RawImage has a valid Texture
            if (rawPeopleToSend == null || rawPeopleToSend.texture == null)
            {
                Debug.LogError("No RawImage or Texture found.");
                return;
            }

           
            Texture2D tex2D = rawPeopleToSend.texture as Texture2D;
            
            
            // encode it to PNG byte[]
            byte[] imageData = tex2D.EncodeToPNG();
            
            int length = imageData.Length;
            byte[] lengthBytes = BitConverter.GetBytes(length);
            stream.Write(lengthBytes, 0, lengthBytes.Length);
            // Debug.Log($"[SendImage] Sending length={length} bytes for the image.");
            // Debug.Log($"[SendImage] Sending length={lengthBytes} bytes for the image.");
            
            // here the image data is sent
            stream.Write(imageData, 0, imageData.Length);

            // Debug.Log("Image sent to Python.");
           
            ReceiveClothingData();
                
            
        }

        public void sendImageCommand()
        {
            byte[] commandBytes = Encoding.UTF8.GetBytes("imagesend");
                stream.Write(commandBytes, 0, commandBytes.Length);
                Debug.Log("Image command sent");
                SendImage();
        }
        
        void SendImage()
        {
            if (client == null || stream == null)
            {
                Debug.LogError("Not connected to server.");
                return;
            }

            // check if the RawImage has a valid Texture
            if (rawImageToSend == null || rawImageToSend.texture == null)
            {
                Debug.LogError("No RawImage or Texture found.");
                return;
            }

            // convert the RawImage’s texture to Texture2D
            Texture2D tex2D = rawImageToSend.texture as Texture2D;
            

            // encode it to PNG (byte[])
            byte[] imageData = tex2D.EncodeToPNG();
            
            int length = imageData.Length;
            byte[] lengthBytes = BitConverter.GetBytes(length);
            stream.Write(lengthBytes, 0, lengthBytes.Length);
            // Debug.Log($"[SendImage] Sending length={length} bytes for the image.");
            // Debug.Log($"[SendImage] Sending length={lengthBytes} bytes for the image.");

            // here the image data is sent
            stream.Write(imageData, 0, imageData.Length);

            // Debug.Log("Image sent to Python.");
            ReceivePeopleData();

        }
        
        async void ReceivePeopleData()
    {
        try
        {

            await Task.Run(async () =>
            {
                byte[] countBytes = await ReadBytesAsync(4);
                int personCount = BitConverter.ToInt32(ConvertEndian(countBytes), 0);
                Debug.Log($"Received {personCount} people");
                
                for (int i = 0; i < personCount; i++)
                {
                    byte[] sizeBytes = await ReadBytesAsync(4);
                    int imageSize = BitConverter.ToInt32(ConvertEndian(sizeBytes), 0);
                    
                    byte[] imageData = await ReadBytesAsync(imageSize);
                    
                    CreateTextureFromBytes(imageData, i);
                }
                loadingScreen.hasFoundPeople = true;
            });
        }

        catch (Exception e)
        {
            Debug.LogError($"Receive error: {e.Message}");
        }

    }
        

    byte[] ConvertEndian(byte[] bytes)
    {
        if (BitConverter.IsLittleEndian)
            Array.Reverse(bytes);
        return bytes;
    }

    async Task<byte[]> ReadBytesAsync(int length)
    {
        byte[] buffer = new byte[length];
        int read = 0;
        while (read < length)
        {
            int bytesReceived = await stream.ReadAsync(buffer, read, length - read);
            if (bytesReceived == 0) throw new Exception("Connection closed");
            read += bytesReceived;
        }
        return buffer;
    }

    void CreateTextureFromBytes(byte[] data, int index)
{
    MainThreadDispatcher.ExecuteOnMainThread(() =>
    {
        try
        {
            // texture from bytes
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(data);

            GameObject buttonObject = new GameObject($"PersonBtn_{index}");
            
            var button = buttonObject.AddComponent<Button>();
            var rectTransform = buttonObject.AddComponent<RectTransform>();
            var canvasRenderer = buttonObject.AddComponent<CanvasRenderer>();
            
            GameObject imageObject = new GameObject("Image");
            RawImage rawImage = imageObject.AddComponent<RawImage>();
            rawImage.texture = tex;
            
            
            AspectRatioFitter aspectFitter = imageObject.AddComponent<AspectRatioFitter>();
            aspectFitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
            aspectFitter.aspectRatio = (float)tex.width / tex.height;
            
            // this creates the checkmark overlay
            GameObject checkmarkObject = new GameObject("Checkmark");
            Image checkmarkImage = checkmarkObject.AddComponent<Image>();
            checkmarkImage.sprite = Resources.Load<Sprite>("CheckmarkSprite.png"); // Load your checkmark sprite
            checkmarkImage.color = Color.green;
            checkmarkObject.SetActive(false);
            
            imageObject.transform.SetParent(buttonObject.transform);
            checkmarkObject.transform.SetParent(buttonObject.transform);
            
            var imageTransform = imageObject.GetComponent<RectTransform>();
            imageTransform.anchorMin = Vector2.zero;
            imageTransform.anchorMax = Vector2.one;
            imageTransform.sizeDelta = Vector2.zero;
            imageTransform.offsetMin = Vector2.zero;
            imageTransform.offsetMax = Vector2.zero;
            
            // positioning the checkmark
            var checkTransform = checkmarkObject.GetComponent<RectTransform>();
            checkTransform.anchorMin = new Vector2(1, 1);
            checkTransform.anchorMax = new Vector2(1, 1);
            checkTransform.pivot = new Vector2(1, 1);
            checkTransform.anchoredPosition = new Vector2(-10, -10);
            checkTransform.sizeDelta = new Vector2(20, 20);
            
            button.onClick.AddListener(() => ToggleSelection(buttonObject, checkmarkObject));
            
            buttonObject.transform.SetParent(imageContainer);
            buttonObject.transform.localScale = Vector3.one;
            
            rectTransform.sizeDelta = new Vector2(150, 150);
        }
        catch (Exception e)
        {
            Debug.LogError($"Button creation failed: {e.Message}");
        }
    });
}

    public async void ReceiveClothingData()
    {
        try
        {
            await Task.Run(async () =>
            {
                // receiving clothing + hair data
                byte[] headerBytes = await ReadBytesAsync(10);
                int dataLength = int.Parse(Encoding.UTF8.GetString(headerBytes).Trim());
                byte[] jsonBytes = await ReadBytesAsync(dataLength);
                string jsonData = Encoding.UTF8.GetString(jsonBytes);

                // receiving color data
                byte[] colorHeaderBytes = await ReadBytesAsync(10);
                int colorDataLength = int.Parse(Encoding.UTF8.GetString(colorHeaderBytes).Trim());
                byte[] colorJsonBytes = await ReadBytesAsync(colorDataLength);
                string colorJsonData = Encoding.UTF8.GetString(colorJsonBytes);

                MainThreadDispatcher.ExecuteOnMainThread(() =>
                {
                    var combinedData = JsonHelper.FromJson<CombinedDetectionData>(jsonData);
                    ClothingDetectionData.CurrentClothes = combinedData.clothes ?? new List<ClothingDetection>();
                    ClothingDetectionData.CurrentHairType = combinedData.hair ?? "";
                    
                    var colorData = JsonHelper.FromJson<ColorDataWrapper>(colorJsonData);
                    ClothingColorManager.CurrentColors = colorData.colors ?? new List<ColorData>();

                    // Debug.Log($"Received {ClothingDetectionData.CurrentClothes.Count} clothing items " + $"and {ClothingColorManager.CurrentColors.Count} color entries");
                    // DetectExample();
                    // LogColorData();
                    
                    PutTheClothes();
                    hoomanManager.GetComponent<ColorAndClothesChanger>().ChangeHair(ClothingDetectionData.CurrentHairType);
                    PutTheColorOn();


                    loadingScreen.hasLoaded = true; // Så' vi færdige :)
                });
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"Data receive error: {e.Message}");
        }
    }
    
    public void removeToggle()
    {
        if (activeCheckmark != null)
        {
            activeCheckmark.SetActive(false);
            activeCheckmark = null;
        }
        checkmarkCount = 0;
    }

    void ToggleSelection(GameObject button, GameObject checkmark)
    {
        if (!checkmark.activeSelf && checkmarkCount == 0)
        {
            checkmark.SetActive(true);
            checkmarkCount = 1;
            activeCheckmark = checkmark;

            rawPeopleToSend = button.GetComponentInChildren<RawImage>();
            displayImage.texture = rawPeopleToSend.texture;

            displayImage.GetComponent<AspectRatioFitter>().aspectRatio =
                button.GetComponentInChildren<AspectRatioFitter>().aspectRatio;
        }
        else if (checkmark.activeSelf && checkmarkCount == 1)
        {
            checkmark.SetActive(false);
            checkmarkCount = 0;
            activeCheckmark = null;
        }
    }

        
        void StopServer()
        {
            if (client != null && stream != null)
            {
                
                    byte[] shutdownMessage = Encoding.UTF8.GetBytes("shutdown");
                    stream.Write(shutdownMessage, 0, shutdownMessage.Length);
                    Debug.Log("Shutdown command sent to Python server.");
                    
                    stream.Close();
                    client.Close();
                
            }
        }

        public void DetectExample()
        {
            foreach (var detection in ClothingDetectionData.CurrentClothes)
            {
                Debug.Log($"Found {detection.label} " + 
                          $"at ({detection.bbox[0]}, {detection.bbox[1]}) " +
                          $"with {detection.confidence:P0} confidence");
            }
    
            Debug.Log($"Detected hair type: {ClothingDetectionData.CurrentHairType}");
        }

        private void PutTheClothes()
        {
            foreach (var Clothes in ClothingDetectionData.CurrentClothes)
            {
                ColorAndClothesChanger temp = hoomanManager.GetComponent<ColorAndClothesChanger>();
                temp.ChangeClothing(Clothes.label);
                
                Debug.Log($"I put {Clothes.label} on");
            }
        }
        
        private void PutTheColorOn()
        {
            foreach (var Cloth in ClothingColorManager.CurrentColors)
            {
                hoomanManager.GetComponent<ColorAndClothesChanger>().ChangeColor(Cloth.hex,Cloth.label);
                Debug.Log($"I change color of {Cloth.label} to be {Cloth.hex}!");
            }
        }
        private void LogColorData()
        {
            foreach (var color in ClothingColorManager.CurrentColors)
            {
                Debug.Log($"{color.label}: " + 
                          $"RGB ({color.rgb[0]}, {color.rgb[1]}, {color.rgb[2]}) " +
                          $"HEX {color.hex}");
            }
        }
}

