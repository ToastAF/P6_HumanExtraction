using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public GameObject guy, gal, loadingScreen;

    public bool hasLoaded = false;
    public bool guyIsActive;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hasLoaded == true)
        {
            if(guyIsActive == true)
            {
                guy.SetActive(true);
                gal.SetActive(false);
            }
            else
            {
                guy.SetActive(false);
                gal.SetActive(true);
            }

            loadingScreen.SetActive(false);
        }
    }

    public void ChangeActivePersonToGuy()
    {
        guyIsActive = true;
        Debug.Log("Changed to guy!");
    }

    public void ChangeActivePersonToGal()
    {
        guyIsActive = false;
        Debug.Log("Changed to gal!");
    }
}
