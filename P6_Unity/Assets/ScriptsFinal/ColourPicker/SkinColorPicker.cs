using UnityEngine;

public class SkinColorPicker : MonoBehaviour
{
    public FlexibleColorPicker fcp;
    public ColorAndClothesChanger ColorChanger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ApplySkinButton()
    {
        ColorChanger.ChangeColor(fcp.hexInput.text, "FaceInUI");
    }
}
