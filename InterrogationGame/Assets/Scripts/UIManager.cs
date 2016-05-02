using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameObject[] pauseObjects;
    GameObject[] returnToMain;
    GameObject[] cutsceneObjects;
    public bool isPaused = false;
    public bool quit = false;
    public bool mainMenu = false;

    // Use this for initialization
    void Start()
    {
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        returnToMain = GameObject.FindGameObjectsWithTag("ShowBeforeReturnToMenu");
        hidePaused();
        hideAreYouSure();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == false)
            {
                showPaused();
                isPaused = true;
                AudioListener.pause = true;
                Time.timeScale = 0.0f;

                Debug.Log("Game is now paused");
            }
            else if (isPaused == true)
            {
                hidePaused();
                isPaused = false;
                AudioListener.pause = false;
                Time.timeScale = 1.0f;

                Debug.Log("Game is now unpaused");
            }
        }
    }
    
    //MENU BUTTONS----------------------------------
    public void Resume()
    {
        hidePaused();
        isPaused = false;

        AudioListener.pause = false;
        Time.timeScale = 1.0f;
    }

    public void Menu()
    {
        mainMenu = true;
        showAreYouSure();
    }

    public void Quit()
    {
        quit = true;
        showAreYouSure();
        Application.Quit();
    }

    public void Yes()
    {
        if (mainMenu)
        {
            Application.LoadLevel("menu");
            AudioListener.pause = false;
        }
        else if (quit)
        {
            Application.Quit();
        }
    }

    public void No()
    {
        hideAreYouSure();
    }

    //----------------------------------------------

    //shows objects with ShowOnPause tag
    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    public void showAreYouSure()
    {
        foreach (GameObject g in returnToMain)
        {
            g.SetActive(true);
        }
    }

    public void hideAreYouSure()
    {
        foreach (GameObject g in returnToMain)
        {
            g.SetActive(false);
        }
    }

    void OnApplicationQuit()
    {
        AudioListener.pause = false;
    }
}
