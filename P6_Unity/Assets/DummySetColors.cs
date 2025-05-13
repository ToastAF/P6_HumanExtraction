using UMA;
using UMA.CharacterSystem;
using UnityEngine;

public class DummySetColors : MonoBehaviour
{
    public DynamicCharacterAvatar script;

    public void DummyChangeColor(string inputColor, string colorArea) //colorArea er gennemsnitsfarvens område. Fx "upper clothes"
    {
        Color tempColor = HexToColor32(inputColor);
        //Color tempColor = inputColor;

        OverlayColorData raw = new OverlayColorData(1); // Det er sådan her UMA laver colors til deres ting. De bruger OverlayColorData...
        raw.channelAdditiveMask[0] = tempColor;

        OverlayColorData cooked = new OverlayColorData(2);
        cooked.channelMask[1] = tempColor;

        switch (colorArea)
        {
            //Labels: 0: "Background", 1: "Hat", 2: "Hair", 3: "Sunglasses", 4: "Upper-clothes",
            //5: "Skirt", 6: "Pants", 7: "Dress", 8: "Belt", 9: "Left-shoe", 10: "Right-shoe", 11:
            //"Face", 12: "Left-leg", 13: "Right-leg", 14: "Left-arm", 15: "Right-arm", 16: "Bag", 17: "Scarf"
            case "Hair":
                script.SetColor("Hair", tempColor);
                break;
            case "Upper-clothes":
                script.SetRawColor("Vest Color", raw, true);
                script.SetRawColor("Shirt", raw, true);
                script.SetRawColor("Coat", raw, true);
                script.SetRawColor("Jacket", raw, true);
                break;
            case "Pants":
                script.SetRawColor("Trousers", raw, true);
                script.SetRawColor("Pants", raw, true);
                script.SetRawColor("Skirt", raw, true);
                break;
            case "Left-shoe":
                script.SetColor("Shoes", tempColor);
                break;
            case "Right-shoe":
                script.SetColor("Shoes", tempColor);
                break;
            case "Face":
                float h, s, v;
                Color.RGBToHSV(tempColor, out h, out s, out v);
                if (v > 0.42f)
                {
                    tempColor.r = 245f;
                    tempColor.g = 220f;
                    tempColor.b = 220f;
                    script.SetColor("Skin", tempColor);
                }
                else
                {
                    script.SetColor("Skin", tempColor);
                }
                break;
            case "FaceInUI":
                script.SetColor("Skin", tempColor);
                break;
            default:
                break;
        }
        script.UpdateColors();
        script.BuildCharacter();
    }

    Color32 HexToColor32(string hex) //Den her konverterer 
    {
        hex = hex.Replace("#", "");

        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        //byte a = (hex.Length >= 8) ? byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber) : (byte)255; // Optional alpha

        return new Color32(r, g, b, 255);
    }
}
