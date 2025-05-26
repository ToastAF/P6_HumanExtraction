using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PythonServerManager : MonoBehaviour
{
    private Process pythonProcess;

    void Start()
    {
        StartPythonServer();
    }

    private void StartPythonServer()
    {
        // Adjust this path as needed:
        //  - it might be simply "python"
        //  - it might be a full path to a python.exe or python script
        //  - or it might be your virtual environment's python
        string pythonPath = @"C:\Path\to\python.exe";
        string scriptPath = @"C:\Path\to\your_server_script.py";

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = pythonPath,
            Arguments = scriptPath,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
        };

        try
        {
            pythonProcess = new Process();
            pythonProcess.StartInfo = startInfo; 
            // Optional: Subscribe to output/error if you want to see server logs
            pythonProcess.OutputDataReceived += (sender, args) => Debug.Log($"[Python STDOUT] {args.Data}");
            pythonProcess.ErrorDataReceived += (sender, args) => Debug.LogError($"[Python STDERR] {args.Data}");
            
            pythonProcess.Start();
            // If you want to read console output, start asynchronous reading:
            pythonProcess.BeginOutputReadLine();
            pythonProcess.BeginErrorReadLine();
            
            Debug.Log("Python server started.");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to start Python server: {e.Message}");
        }
    }
}
