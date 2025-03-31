using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;

public class CSVToMatrix : MonoBehaviour
{
    public string fileName = "Data/person_0.csv"; // Path to your CSV file
    public List<Color> rgbColors = new List<Color>(); // List to store colors
    public List<int[]> matrix = new List<int[]>(); // Stores the extracted matrix
    public List<float[]> pureRGB = new List<float[]>(); // Stores the extracted matrix
    void Start()
    {
        ReadCSVAndExtractMatrix();
        PrintMatrix();
        PrintColors();
    }

    /// <summary>
    /// Reads the CSV file and extracts columns 3, 4, and 5 as RGB values.
    /// </summary>
    void ReadCSVAndExtractMatrix()
    {
        string path = "person_0"; // No file extension needed
        TextAsset csvFile = Resources.Load<TextAsset>(path);

        if (csvFile != null)
        {
            string[] lines = csvFile.text.Split('\n');

            for (int i = 1; i < lines.Length; i++) // Skip the first line
            {
                string[] values = lines[i].Trim().Split(',');

                if (values.Length >= 5)
                {
                    // Parse values as integers
                    int r = int.Parse(values[2]);
                    int g = int.Parse(values[3]);
                    int b = int.Parse(values[4]);

                    // Add to matrix
                    matrix.Add(new int[] { r, g, b });

                    // Normalize values for Color (0 - 1)
                    float rNorm = Mathf.Clamp01(r / 255f);
                    float gNorm = Mathf.Clamp01(g / 255f);
                    float bNorm = Mathf.Clamp01(b / 255f);

                    // Create Color and add to list
                    rgbColors.Add(new Color(rNorm, gNorm, bNorm));
                    pureRGB.Add(new float[] {rNorm, gNorm, bNorm});
                }
                else
                {
                    Debug.LogWarning($"Skipping row {i + 1}: Not enough columns.");
                }
            }
        }
        else
        {
            Debug.LogError("File not found in Resources.");
        }
    }

    /// <summary>
    /// Prints the matrix to the Unity Console.
    /// </summary>
    void PrintMatrix()
    {
        Debug.Log("Extracted RGB Matrix (Integers):");

        foreach (int[] row in matrix)
        {
            Debug.Log($"[R: {row[0]}, G: {row[1]}, B: {row[2]}]");
        }
    }

    /// <summary>
    /// Prints the colors to the Unity Console.
    /// </summary>
    void PrintColors()
    {
        Debug.Log("Extracted Colors (Normalized):");

        foreach (Color color in rgbColors)
        {
            Debug.Log($"Color - R: {color.r}, G: {color.g}, B: {color.b}");
        }
        Debug.Log($"Try this: R={pureRGB[0][0]}, G={pureRGB[0][1]}, B={pureRGB[0][2]}");

    } 
    
    
    /* Reference for values in matrix
     [1] = Nose
     [2] = Neck
     [3] = RShoulder
     [4] = RElbow
     [5] = RWrist
     [6] = LShoulder
     [7] = LElbow
     [8] = LWrist
     [9] = RHip
     [10] = RKnee
     [11] = RAnkle
     [12] = LHip
     [13] = LKnee
     [14] = LAnkle
     [15] = REye
     [16] = LEye
     [17] = REar
     [18] = LEar
     */
    
}
