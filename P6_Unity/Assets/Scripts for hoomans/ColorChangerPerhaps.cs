using NUnit.Framework;
using UMA;
using UMA.CharacterSystem;
using UnityEngine;
using System.Collections.Generic;

public class ColorChangerPerhaps : MonoBehaviour
{
    [Header ("The Hoomans")]
    public GameObject guy, gal; // The two models
    public bool isGuy; // Gender, sex, osv...

    [Header ("The Other Stuff")]
    public DynamicCharacterAvatar avatarScript;
    UMATextRecipe clothItem;

    string UMABodyPart;
    public string TestChangeSTRING;

    string testColor = "#8f746e";
    string testHairType = "short";

    void Start()
    {
    
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

        ChooseGenderAvatar();
    }

    public void ChooseGenderAvatar()
    {
        if (isGuy == true)
        {
            avatarScript = guy.GetComponent<DynamicCharacterAvatar>();
            gal.SetActive(false);
            guy.SetActive(true);
        }
        else
        {
            avatarScript = gal.GetComponent<DynamicCharacterAvatar>();
            gal.SetActive(true);
            guy.SetActive(false);
        }
    }

    public string ChooseClothing(string clothingType)
    {
        //['shirt, blouse', 'top, t-shirt, sweatshirt', 'sweater', 'cardigan', 'jacket',
        //'vest', 'pants', 'shorts', 'skirt', 'coat', 'dress', 'jumpsuit', 'cape', 'glasses',
        //'hat', 'headband, head covering, hair accessory', 'tie', 'glove', 'watch', 'belt',
        //'leg warmer', 'tights, stockings', 'sock', 'shoe', 'bag, 'scarf', 'umbrella', 'hood', 'collar']

        if(isGuy == true)
        {
            return MaleClothing(clothingType);
        }
        else
        {
            return FemaleClothing(clothingType);
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
            avatarScript.SetSlot("Hair", "FemaleHair3"); // <-- Input lang hår recipe
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
        OverlayColorData raw = new OverlayColorData(1);
        raw.channelAdditiveMask[0] = HexToColor32(inputColor);

        OverlayColorData cooked = new OverlayColorData(2);
        cooked.channelMask[1] = HexToColor32(inputColor);

        switch (colorArea)
        {
            //Labels: 0: "Background", 1: "Hat", 2: "Hair", 3: "Sunglasses", 4: "Upper-clothes",
            //5: "Skirt", 6: "Pants", 7: "Dress", 8: "Belt", 9: "Left-shoe", 10: "Right-shoe", 11:
            //"Face", 12: "Left-leg", 13: "Right-leg", 14: "Left-arm", 15: "Right-arm", 16: "Bag", 17: "Scarf"
            case "Hair":
                avatarScript.SetColor("Hair", HexToColor32(inputColor));
                break;
            case "Upper-clothes":
                avatarScript.SetRawColor("Vest Color", raw, true);
                avatarScript.SetRawColor("Shirt", raw, true);
                avatarScript.SetRawColor("Coat", raw, true);
                avatarScript.SetRawColor("Jacket", raw, true);
                break;
            case "Pants":
                avatarScript.SetRawColor("Trousers", raw, true);
                avatarScript.SetRawColor("Pants", raw, true);
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
        avatarScript.UpdateColors();
        avatarScript.BuildCharacter();
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
                UMABodyPart = "Chest";
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
