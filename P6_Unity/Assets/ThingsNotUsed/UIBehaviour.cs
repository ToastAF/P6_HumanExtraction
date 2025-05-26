using UnityEngine;
using UnityEngine.UI;


public class UIBehaviour : MonoBehaviour
{
    public Button startButton;
    public Button peopleButton;
    public Button modelbutton;
    public Button choooseButton;
    public Button placeButton;
    public Button generateButton;
    public Button findButton;

    private Image picture;


    public GameObject guys;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void choosePicture()
    {

    }

    public void setGameObjectActive()
    {
        guys.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
