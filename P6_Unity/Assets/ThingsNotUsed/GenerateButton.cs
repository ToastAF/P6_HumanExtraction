using UnityEngine;

public class GenerateButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void setButtonActive()
    {
        gameObject.SetActive(true);
    }
    
    public void setButtonNotActive()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
