using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public GameObject guy, gal, loadingScreen, imageTab, modelTab, peopleTab;

    public bool hasFoundPeople = false;
    public bool hasLoaded = false;
    public bool guyIsActive;
    public bool loadedPage;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hasFoundPeople == true)
        {
            imageTab.SetActive(false);
            peopleTab.SetActive(true);
            hasFoundPeople = false;
        }

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
            if (!loadedPage)
            {
                modelTab.SetActive(true);
                peopleTab.SetActive(false);
                loadedPage = true;
            }

            

        }
        loadedPage = false;
    }

    public void ChangeHasLoaded()
    {
        hasLoaded = false;
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
