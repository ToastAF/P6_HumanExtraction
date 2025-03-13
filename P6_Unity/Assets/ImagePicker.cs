using UnityEngine;
using UnityEngine.UI;
using System.Windows.Forms;
using System.IO;

public class ImagePicker : MonoBehaviour
{
    public RawImage displayImage;  // UI RawImage
    private AspectRatioFitter aspectRatioFitter;

    void Start()
    {
        aspectRatioFitter = displayImage.GetComponent<AspectRatioFitter>();
    }

    public void OpenFileExplorer()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            InitialDirectory = @"C:\", // Fixed escape character
            Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif", // Fixed filter syntax
            Title = "Select an Image"
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            string filePath = openFileDialog.FileName;
            StartCoroutine(LoadImage(filePath));
        }
    }

    private System.Collections.IEnumerator LoadImage(string filePath)
    {
        byte[] imageBytes = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageBytes);

        displayImage.texture = texture;
        aspectRatioFitter.aspectRatio = (float)texture.width / texture.height;  // Prevent stretching

        yield return null;
    }
}