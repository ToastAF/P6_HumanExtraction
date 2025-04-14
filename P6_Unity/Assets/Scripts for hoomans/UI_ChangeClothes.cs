using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UMA.CharacterSystem;

public class UI_ChangeClothes : MonoBehaviour
{
    ColorChangerPerhaps otherScript; // ColorChangerPerhaps scriptet på samme objekt

    public TMP_Text maleUpperClothesDropdownLabel, maleLowerClothesDropdownLabel, femaleUpperClothesDropdownLabel, femaleLowerClothesDropdownLabel;

    string convertedString;

    private void Start()
    {
        otherScript = GetComponent<ColorChangerPerhaps>();
    }

    public void ChangeMaleUpperClothes() //                 -------------------------------- MALE --------------------------------
    {
        convertedString = ChooseClothes(maleUpperClothesDropdownLabel);

        otherScript.ChangeClothing(convertedString);
    }

    public void ChangeMaleLowerClothes()
    {
        convertedString = ChooseClothes(maleLowerClothesDropdownLabel);

        otherScript.ChangeClothing(convertedString);
    }

    public void ChangeFemaleUpperClothes() //                 -------------------------------- FEMALE -------------------------------
    {
        convertedString = ChooseClothes(femaleUpperClothesDropdownLabel);

        otherScript.ChangeClothing(convertedString);
    }

    public void ChangeFemaleLowerClothes()
    {
        convertedString = ChooseClothes(femaleLowerClothesDropdownLabel);

        otherScript.ChangeClothing(convertedString);
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

            default:
                return "";
        }
    }
}
