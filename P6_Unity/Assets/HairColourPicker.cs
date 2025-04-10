using UnityEngine;

public class HairColorPicker : MonoBehaviour
{
    public FlexibleColorPicker fcp;
    public ColorChangerPerhaps ColorChanger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ApplyHairButton()
    {
        ColorChanger.ChangeColor(fcp.hexInput.text, "Hair");
    }
}
