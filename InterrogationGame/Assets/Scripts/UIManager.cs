using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject pause;
    private GameObject returnToMain;
    private GameObject options;

    public bool isPaused = false;
    public bool quit = false;
    public bool mainMenu = false;

    void Awake()
    {
        options = GameObject.FindGameObjectWithTag("OptionsPanel");
        if(options == null) { Debug.Log("WHY"); }
        //options.SetActive(false);
    }
    
    // Use this for initialization
    void Start()
    {
        pause = GameObject.FindGameObjectWithTag("ShowOnPause");
        returnToMain = GameObject.FindGameObjectWithTag("ShowBeforeReturnToMenu");
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
            }
            else if (isPaused == true && !returnToMain.activeSelf && !options.activeSelf)
            {
                hidePaused();
                isPaused = false;
                AudioListener.pause = false;
                Time.timeScale = 1.0f;
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
        pause.SetActive(true);
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {
        pause.SetActive(false);
    }

    public void showAreYouSure()
    {
        returnToMain.SetActive(true);
    }

    public void hideAreYouSure()
    {
        returnToMain.SetActive(false);
    }

    public void showOptions()
    {
        options.SetActive(true);
    }

    public void hideOptions()
    {
        options.SetActive(false);
    }

    void OnApplicationQuit()
    {
        AudioListener.pause = false;
    }
}
