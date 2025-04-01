using UMA;
using UMA.CharacterSystem;
using UnityEngine;

public class ColorChangerPerhaps : MonoBehaviour
{
    DynamicCharacterAvatar avatarScript;
    UMATextRecipe clothItem;

    void Start()
    {
        avatarScript = GetComponent<DynamicCharacterAvatar>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeClothing();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeColor();
        }
        
        //clothItem = avatarScript.GetWardrobeItem("MaleShirt2");
    }

    public void ChangeClothing()
    {
        avatarScript.SetSlot("Chest", "MaleHoodie_Recipe");
        avatarScript.BuildCharacter();
    }

    public void ChangeColor()
    {
        avatarScript.SetColor("Shirt", Color.black);
        avatarScript.BuildCharacter();
    }
}
