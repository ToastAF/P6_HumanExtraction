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

    List<float> bruh = [200, 0, 0];

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

    public string ChooseClothing(string clothingType)
    {
        //['shirt, blouse', 'top, t-shirt, sweatshirt', 'sweater', 'cardigan', 'jacket',
        //'vest', 'pants', 'shorts', 'skirt', 'coat', 'dress', 'jumpsuit', 'cape', 'glasses',
        //'hat', 'headband, head covering, hair accessory', 'tie', 'glove', 'watch', 'belt',
        //'leg warmer', 'tights, stockings', 'sock', 'shoe', 'bag, 'scarf', 'umbrella', 'hood', 'collar']

        switch (clothingType)
        {
            case "shirt, blouse":
                UMABodyPart = "Chest"; //Hvor på kroppen det skal sidde
                return "Shirt"; //Navn på UMA clothing recipe
            case "top, t-shirt, sweatshirt":
                UMABodyPart = "Chest";
                return "";
            case "sweater":
                UMABodyPart = "Chest";
                return "";
            case "cardigan":
                UMABodyPart = "Chest";
                return "";
            case "jacket":
                UMABodyPart = "Chest";
                return "";
            case "vest":
                UMABodyPart = "Chest";
                return "";
            case "pants":
                UMABodyPart = "Legs";
                return "";
            case "shorts":
                UMABodyPart = "Legs";
                return "";
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
                UMABodyPart = "Body"; //Ved ikke om den skal være her
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
                return "";
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


        avatarScript.SetSlot(UMABodyPart, ChooseClothing(clothing));
        avatarScript.BuildCharacter();
    }

    public void ChangeColor()
    {
        avatarScript.SetColor("Shirt", Color.black);
        avatarScript.BuildCharacter();
    }
}
