using NUnit.Framework;
using UMA;
using UMA.CharacterSystem;
using UnityEngine;
using System.Collections.Generic;

public class ColorChangerPerhaps : MonoBehaviour
{
    DynamicCharacterAvatar avatarScript;
    UMATextRecipe clothItem;

    string UMABodyPart;

    string testColor = "#ff5733";
    string testHairType = "short";

    void Start()
    {
        avatarScript = GetComponent<DynamicCharacterAvatar>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeClothing("shirt, blouse");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeColor(testColor, "");
        }
        
        //clothItem = avatarScript.GetWardrobeItem("MaleShirt2");
    }

    public string ChooseClothing(string clothingType)
    {
        //['shirt, blouse', 'top, t-shirt, sweatshirt', 'sweater', 'cardigan', 'jacket',
        //'vest', 'pants', 'shorts', 'skirt', 'coat', 'dress', 'jumpsuit', 'cape', 'glasses',
        //'hat', 'headband, head covering, hair accessory', 'tie', 'glove', 'watch', 'belt',
        //'leg warmer', 'tights, stockings', 'sock', 'shoe', 'bag, 'scarf', 'umbrella', 'hood', 'collar']

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

    public void ChangeHair(string hairType)
    {
        //Skitf hår baseret på hairType variablen
        if (hairType == "short")
        {
            avatarScript.SetSlot("Hair", "MaleHairSlick01_Recipe"); // <-- Input kort hår recipe
        }
        else if (hairType == "long")
        {
            avatarScript.SetSlot("Hair", "FemaleHair1"); // <-- Input lang hår recipe
        }
        else
        {
            avatarScript.ClearSlot("Hair"); // Vi fjerner bare alt hår her
        }
        avatarScript.BuildCharacter();
    }

    public void ChangeClothing(string clothing)
    {
        string chosenCloth = ChooseClothing(clothing);

        //Skift tøj baseret på clothing variablen
        if(chosenCloth != "")
        {
            avatarScript.SetSlot(UMABodyPart, chosenCloth);
        }
        avatarScript.BuildCharacter();
    }

    public void ChangeColor(string inputColor, string colorArea) //colorArea er gennemsnitsfarvens område. Fx "upper clothes"
    {
        switch (colorArea)
        {
            //Labels: 0: "Background", 1: "Hat", 2: "Hair", 3: "Sunglasses", 4: "Upper-clothes",
            //5: "Skirt", 6: "Pants", 7: "Dress", 8: "Belt", 9: "Left-shoe", 10: "Right-shoe", 11:
            //"Face", 12: "Left-leg", 13: "Right-leg", 14: "Left-arm", 15: "Right-arm", 16: "Bag", 17: "Scarf"
            case "Hair":
                avatarScript.SetColor("Hair", HexToColor32(inputColor));
                break;
            case "Upper-clothes":
                avatarScript.SetColor("Vest Color", HexToColor32(inputColor));
                avatarScript.SetColor("Shirt", HexToColor32(inputColor));
                avatarScript.SetColor("Coat", HexToColor32(inputColor));
                break;
            case "Pants":
                avatarScript.SetColor("Trousers", HexToColor32(inputColor));
                break;
            case "Left-shoe":
                avatarScript.SetColor("Shoes", HexToColor32(inputColor));
                break;
            case "Right-shoe":
                avatarScript.SetColor("Shoes", HexToColor32(inputColor));
                break;
            case "Face":
                avatarScript.SetColor("Skin", HexToColor32(inputColor));
                break;
            default:
                break;
        }
        avatarScript.BuildCharacter();
    }

    Color32 HexToColor32(string hex) //Den her konverterer 
    {
        hex = hex.Replace("#", "");

        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        byte a = (hex.Length >= 8) ? byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber) : (byte)255; // Optional alpha

        return new Color32(r, g, b, a);
    }
}
