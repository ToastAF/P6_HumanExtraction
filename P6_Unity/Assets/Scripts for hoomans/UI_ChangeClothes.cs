using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UMA.CharacterSystem;

public class UI_ChangeClothes : MonoBehaviour
{
    ColorChangerPerhaps otherScript; // ColorChangerPerhaps scriptet på samme objekt
    DynamicCharacterAvatar dcaGuy, dcaGal;

    public TMP_Text maleUpperClothesDropdownLabel, maleLowerClothesDropdownLabel, femaleUpperClothesDropdownLabel, femaleLowerClothesDropdownLabel;
    public TMP_Text maleShoesDropdownLabel, femaleShoesDropdownLabel;

    string convertedString;

    private void Start()
    {
        otherScript = GetComponent<ColorChangerPerhaps>();
        dcaGuy = otherScript.guy.GetComponent<DynamicCharacterAvatar>();
        dcaGal = otherScript.gal.GetComponent<DynamicCharacterAvatar>();
    }

    public void ChangeMaleUpperClothes() //                 -------------------------------- MALE --------------------------------
    {
        ClearClothes("Chest");
        ClearClothes("Shoulders");

        convertedString = ChooseClothes(maleUpperClothesDropdownLabel);

        otherScript.ChangeClothing(convertedString);
    }

    public void ChangeMaleLowerClothes()
    {
        ClearClothes("Legs");

        convertedString = ChooseClothes(maleLowerClothesDropdownLabel);

        otherScript.ChangeClothing(convertedString);
    }

    public void ChangeFemaleUpperClothes() //                 -------------------------------- FEMALE -------------------------------
    {
        ClearClothes("Chest");
        ClearClothes("Shoulders");
        ClearClothes("FullOutfit");

        convertedString = ChooseClothes(femaleUpperClothesDropdownLabel);

        otherScript.ChangeClothing(convertedString);
    }

    public void ChangeFemaleLowerClothes()
    {
        ClearClothes("Legs");

        convertedString = ChooseClothes(femaleLowerClothesDropdownLabel);

        otherScript.ChangeClothing(convertedString);
    }

    public void ChangeMaleShoes()
    {
        ClearClothes("Feet");

        convertedString = ChooseClothes(maleShoesDropdownLabel);

        otherScript.ChangeClothing(convertedString);
    }

    public void ChangeFemaleShoes()
    {
        ClearClothes("Feet");

        convertedString = ChooseClothes(femaleShoesDropdownLabel);

        otherScript.ChangeClothing(convertedString);
    }

    public void ClearClothes(string clothingType)
    {
        dcaGuy.ClearSlot(clothingType);
        dcaGal.ClearSlot(clothingType);
    }

    public void ClearAllClothes()
    {
        ClearClothes("Chest");
        ClearClothes("Shoulders");
        ClearClothes("FullOutfit");
        ClearClothes("Legs");
        ClearClothes("Feet");

        dcaGuy.BuildCharacter();
        dcaGal.BuildCharacter();
    }

    public string ChooseClothes(TMP_Text dropdownText)
    {
        switch (dropdownText.text)
        {
            case "T-Shirt": // Upper body
                return "shirt, blouse";
            case "Shirt":
                return "cardigan";
            case "Jacket":
                return "jacket";
            case "Vest":
                return "vest";
            case "Turtleneck":
                return "cardigan";
            case "Coat":
                return "coat";

            case "Work pants": // Lower body
                return "pants";
            case "Shorts":
                return "shorts";
            case "Pants":
                return "pants";
            case "Skirt":
                return "skirt";

            case "Shoes":
                return "";

            default:
                return "";
        }
    }
}
