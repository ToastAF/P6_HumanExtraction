using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SFB;


public class FileExplorerTests : MonoBehaviour
{
    
    private ExtensionFilter[] extensions = new[] {
        new ExtensionFilter("Data Files", "dat", "txt", "playerdata"),
        new ExtensionFilter("Other", "*")
    };
    
    
    private void Awake() {
        //SelectFile();
    }
    
    private void SelectFile() {
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, false);
        Debug.Log(paths[0]);
    }
    
}
