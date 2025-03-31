using UMA;
using UMA.CharacterSystem;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Events;

public class ColorToClothing : MonoBehaviour
{
    public GameObject character;
    private UMAData UMAData;
    public GameObject button;
    public string dataFromtxt;
    public GameObject dataHolder;
    public CSVToMatrix CSVToMatrix;
    public DynamicCharacterAvatar DynamicCharacterAvatar;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        UMAData = character.GetComponent<UMAData>();
        CSVToMatrix = dataHolder.GetComponent<CSVToMatrix>();
        DynamicCharacterAvatar = character.GetComponent<DynamicCharacterAvatar>();

    }

    public void changeColor()
    {
        //DynamicCharacterAvatar.SetSlot( UMATextRecipe "wardrobe recipe" )
    }
}
