using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System;
using System.IO;
using UnityEngine.UI;

public class Server : MonoBehaviour
{
        TcpClient client;
        NetworkStream stream;
        string host = "127.0.0.1";
        int port = 65432;
        public RawImage rawImageToSend;
    
        void Start() {
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

        public void sendImageCommand()
        {
            try
            {
                byte[] shutdownMessage = Encoding.UTF8.GetBytes("imagesend");
                stream.Write(shutdownMessage, 0, shutdownMessage.Length);
                Debug.Log("Shutdown command sent to Python server.");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error sending shutdown command: {e.Message}");
            }
            finally
            {
                SendImage();
            }
            
            
        }
        
        void SendImage()
        {
            if (client == null || stream == null)
            {
                Debug.LogError("Not connected to server.");
                return;
            }

            // 1. Check if the RawImage has a valid Texture
            if (rawImageToSend == null || rawImageToSend.texture == null)
            {
                Debug.LogError("No RawImage or Texture found.");
                return;
            }

            // 2. Convert the RawImage’s texture to Texture2D
            // RawImage.texture is a 'Texture', so we need to cast it if it's actually a Texture2D
            Texture2D tex2D = rawImageToSend.texture as Texture2D;
            if (tex2D == null)
            {
                Debug.LogError("The RawImage does not contain a Texture2D. " +
                               "You may need to create a new Texture2D from a RenderTexture or other source.");
                return;
            }

            

            // 2. Encode it to PNG (byte[])
            byte[] imageData = tex2D.EncodeToPNG();

            try
            {
                // 3. Send the length of the image data (so Python knows how many bytes to read)
                // We'll send this length as a 4-byte integer
                
                int length = imageData.Length;
                byte[] lengthBytes = BitConverter.GetBytes(length);
                stream.Write(lengthBytes, 0, lengthBytes.Length);
                Debug.Log($"[SendImage] Sending length={length} bytes for the image.");
                Debug.Log($"[SendImage] Sending length={lengthBytes} bytes for the image.");

                // 4. Send the actual image bytes
                stream.Write(imageData, 0, imageData.Length);

                Debug.Log("Image sent to Python.");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error sending image: {e.Message}");
            }
        }
        
        void StopServer()
        {
            if (client != null && stream != null)
            {
                try
                {
                    byte[] shutdownMessage = Encoding.UTF8.GetBytes("shutdown");
                    stream.Write(shutdownMessage, 0, shutdownMessage.Length);
                    Debug.Log("Shutdown command sent to Python server.");
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error sending shutdown command: {e.Message}");
                }
                finally
                {
                    stream.Close();
                    client.Close();
                }
            }
        }
}
