using NUnit.Framework;
using UMA;
using UMA.CharacterSystem;
using UnityEngine;
using System.Collections.Generic;

public class ColorAndClothes : MonoBehaviour
{
    DynamicCharacterAvatar avatarScript;
    UMATextRecipe clothItem;

    string UMABodyPart;

    // List<float> bruh = [200, 0, 0];

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
            ChangeColor();
        }
        
        //clothItem = avatarScript.GetWardrobeItem("MaleShirt2");
    }

    // Condition først før man kører ChooseClothing hvor man vælger bodyType -> den kører en af 2 switch cases
    
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
                UMABodyPart = "Chest";
                return "M_Jacket_2";
            case "vest":
                UMABodyPart = "Chest";
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
                return "noClothes";
        }
    }

    public void ChangeClothing(string clothing)
    {
        string TypeClothes = ChooseClothing(clothing);
        
        avatarScript.SetSlot(UMABodyPart, TypeClothes);
        avatarScript.BuildCharacter();
    }

    public void ChangeColor()
    {
        avatarScript.SetColor("Shirt", Color.black);
        avatarScript.BuildCharacter();
    }
    
    
    // Farver er i ClothingColorManager.CurrentColors.hex
    // Labels er i ClothingColorManager.CurrentColors.label
    
    // Akkurate labels er i ClothingDetectionData.CurrentClothes.label;
    // Hair type er i ClothingDetectionData.CurrentHairType;
    // -> får enten short, long eller none
    
    
    /*
    public static class ClothingDetectionData
    {
        public static List<ClothingDetection> CurrentClothes = new List<ClothingDetection>();
        public static string CurrentHairType = "";
    }
    */
    
    /*
    public class ClothingDetection
    {
        public string label;
        public float confidence;
        public List<float> bbox; // [x_min, y_min, x_max, y_max]
        // Remove hairType from here since it's now separate
    }
    */
}
