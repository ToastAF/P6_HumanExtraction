using NUnit.Framework;
using UMA;
using UMA.CharacterSystem;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ColorAndClothesChanger : MonoBehaviour
{
    [Header ("The Hoomans")]
    public GameObject guy, gal; // The two models

    [Header ("The Other Stuff")]
    public DynamicCharacterAvatar guyScript, galScript;
    UMATextRecipe clothItem;

    string UMABodyPart;
    public string TestChangeSTRING;

    string testColor = "#332c2d";
    string testHairType = "short";

    void Start()
    {
        guyScript = guy.GetComponent<DynamicCharacterAvatar>();
        galScript = gal.GetComponent<DynamicCharacterAvatar>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeClothing("jacket");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeColor(testColor, TestChangeSTRING);
        }

        //clothItem = avatarScript.GetWardrobeItem("MaleShirt2");
    }

    public void ChangeGuyHair(string hairType)
    {
        //Skitf hår baseret på hairType variablen
        if (hairType == "short")
        {
            guyScript.SetSlot("Hair", "MaleHairSlick01_Recipe"); // <-- Input kort hår recipe
        }
        else if (hairType == "long")
        {
            guyScript.SetSlot("Hair", "MaleHair2"); // <-- Input lang hår recipe
        }
        else
        {
            guyScript.ClearSlot("Hair"); // Vi fjerner bare alt hår her
        }
        guyScript.BuildCharacter();
    }

    public void ChangeGalHair(string hairType)
    {
        
        if (hairType == "short")
        {
            galScript.SetSlot("Hair", "FemaleHair2"); // <-- Input kort hår recipe
        }
        else if (hairType == "long")
        {
            galScript.SetSlot("Hair", "FemaleHair3"); // <-- Input lang hår recipe
        }
        else
        {
            galScript.ClearSlot("Hair"); // Vi fjerner bare alt hår her
        }
        galScript.BuildCharacter();
    }

    public void ChangeHair(string hairType) // Det er den her vi kalder
    {
        ChangeGuyHair(hairType);
        ChangeGalHair(hairType);
    }

    public void ChangeClothing(string clothing)
    {
        string chosenCloth = MaleClothing(clothing); // Guy først

        //Skift tøj baseret på clothing variablen
        if(chosenCloth != "")
        {
            guyScript.SetSlot(UMABodyPart, chosenCloth);
        }


        chosenCloth = FemaleClothing(clothing); // Gal bagefter

        if (chosenCloth != "")
        {
            galScript.SetSlot(UMABodyPart, chosenCloth);
        }

        guyScript.BuildCharacter();
        galScript.BuildCharacter();
    }

    public void ChangeColor(string inputColor, string colorArea) //colorArea er gennemsnitsfarvens område. Fx "upper clothes"
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
                guyScript.SetColor("Hair", tempColor);

                galScript.SetColor("Hair", tempColor);
                break;
            case "Upper-clothes":
                guyScript.SetRawColor("Vest Color", raw, true);
                guyScript.SetRawColor("Shirt", raw, true);
                guyScript.SetRawColor("Coat", raw, true);
                guyScript.SetRawColor("Jacket", raw, true);

                galScript.SetRawColor("Vest Color", raw, true);
                galScript.SetRawColor("Shirt", raw, true);
                galScript.SetRawColor("Coat", raw, true);
                galScript.SetRawColor("Jacket", raw, true);
                break;
            case "Pants":
                guyScript.SetRawColor("Trousers", raw, true);
                guyScript.SetRawColor("Pants", raw, true);

                galScript.SetRawColor("Trousers", raw, true);
                galScript.SetRawColor("Pants", raw, true);
                galScript.SetRawColor("Skirt", raw, true);
                break;
            case "Left-shoe":
                guyScript.SetColor("Shoes", tempColor);

                galScript.SetColor("Shoes", tempColor);
                break;
            case "Right-shoe":
                guyScript.SetColor("Shoes", tempColor);

                galScript.SetColor("Shoes", tempColor);
                break;
            case "Face":
                float h, s, v;
                Color.RGBToHSV(tempColor, out h, out s, out v);
                if (v > 0.42f)
                {
                    tempColor.r = 245f;
                    tempColor.g = 220f;
                    tempColor.b = 220f;
                    guyScript.SetColor("Skin", tempColor);

                    galScript.SetColor("Skin", tempColor);
                }
                else
                {
                    guyScript.SetColor("Skin", tempColor);

                    galScript.SetColor("Skin", tempColor);
                }
                break;
            case "FaceInUI":
                guyScript.SetColor("Skin", tempColor);

                galScript.SetColor("Skin", tempColor);
                break;
            default:
                break;
        }
        guyScript.UpdateColors();
        guyScript.BuildCharacter();

        galScript.UpdateColors();
        galScript.BuildCharacter();
        Debug.Log("Changed color!");
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

    public string MaleClothing(string clothingType)
    {
        switch (clothingType)
        {
            case "shirt, blouse":
                UMABodyPart = "Chest"; //Hvor p� kroppen det skal sidde
                return "M_T-Shirt"; //Navn p� UMA clothing recipe
            case "top, t-shirt, sweatshirt":
                UMABodyPart = "Chest";
                return "M_ThinShirt";
            case "sweater":
                UMABodyPart = "Chest";
                return "M_ThinShirt";
            case "cardigan":
                UMABodyPart = "Chest";
                return "M_ThinShirt";
            case "jacket":
                UMABodyPart = "Shoulders";
                return "M_Jacket_2";
            case "vest":
                UMABodyPart = "Shoulders";
                return "M_Vest_4";
            case "pants":
                UMABodyPart = "Legs";
                return "M_WorkPants_1";
            case "shorts":
                UMABodyPart = "Legs";
                return "M_ShortsT2";
            case "skirt":
                UMABodyPart = "Legs";
                return "";
            case "coat":
                UMABodyPart = "Chest";
                return "";
            case "dress":
                UMABodyPart = "Chest";
                return "";
            case "jumpsuit":
                UMABodyPart = "Body"; //Ved ikke om den skal v�re her
                return "";
            case "cape":
                UMABodyPart = "Cape";
                return "";
            case "glasses":
                UMABodyPart = "Face";
                return "";
            case "hat":
                UMABodyPart = "Helmet";
                return "";
            case "headband, head covering, hair accessory":
                UMABodyPart = "Helmet";
                return "";
            case "tie":
                return "";
            case "glove":
                return "";
            case "watch":
                return "";
            case "belt":
                return "";
            case "leg warmer":
                return "";
            case "tights, stockings":
                return "";
            case "sock":
                return "";
            case "shoe":
                UMABodyPart = "Feet";
                return "M_Shoes";
            case "bag":
                return "";
            case "scarf":
                return "";
            case "umbrella":
                return "";
            case "hood":
                return "";
            case "collar":
                return "";
            default:
                return "";
        }
    }

    public string FemaleClothing(string clothingType)
    {
        switch (clothingType)
        {
            case "shirt, blouse":
                UMABodyPart = "Chest"; //Hvor p� kroppen det skal sidde
                return "F_Shirt"; //Navn p� UMA clothing recipe
            case "top, t-shirt, sweatshirt":
                UMABodyPart = "Chest";
                return "F_TurtleNeck_1";
            case "sweater":
                UMABodyPart = "Chest";
                return "F_TurtleNeck_1";
            case "cardigan":
                UMABodyPart = "Chest";
                return "F_TurtleNeck_1";
            case "jacket":
                UMABodyPart = "Shoulders";
                return "F_Jacket_1";
            case "vest":
                UMABodyPart = "Shoulders";
                return "";
            case "pants":
                UMABodyPart = "Legs";
                return "F_Trousers";
            case "shorts":
                UMABodyPart = "Legs";
                return "M_ShortsT1";
            case "skirt":
                UMABodyPart = "Legs";
                return "F_Mini_Skirt_2";
            case "coat":
                UMABodyPart = "FullOutfit";
                return "F_Coat_3";
            case "dress":
                UMABodyPart = "Chest";
                return "";
            case "jumpsuit":
                UMABodyPart = "Body"; //Ved ikke om den skal v�re her
                return "";
            case "cape":
                UMABodyPart = "Cape";
                return "";
            case "glasses":
                UMABodyPart = "Face";
                return "F_Glasses";
            case "hat":
                UMABodyPart = "Helmet";
                return "";
            case "headband, head covering, hair accessory":
                UMABodyPart = "Helmet";
                return "";
            case "tie":
                return "";
            case "glove":
                return "";
            case "watch":
                return "";
            case "belt":
                return "";
            case "leg warmer":
                return "";
            case "tights, stockings":
                return "";
            case "sock":
                return "";
            case "shoe":
                UMABodyPart = "Feet";
                return "F_Shoes";
            case "bag":
                return "";
            case "scarf":
                return "";
            case "umbrella":
                return "";
            case "hood":
                return "";
            case "collar":
                return "";
            default:
                return "";
        }
    }
}
