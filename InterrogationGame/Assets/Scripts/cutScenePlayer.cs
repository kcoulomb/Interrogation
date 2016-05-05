// Thanks to DimasTheDriver for the tutorial for this script

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUITexture))]
[RequireComponent(typeof(AudioSource))]

public class cutScenePlayer : MonoBehaviour
{
    //the GUI texture  
    private GUITexture videoGUItex;
    //the Movie texture  
    private MovieTexture mTex;
    //the AudioSource  
    private AudioSource movieAS;

    private GameObject cutsceneHandler;

    private int playedCutscenes = 0;

    void Start()
    {
        cutsceneHandler = GameObject.FindGameObjectWithTag("CutSceneHandler");

        if(Application.loadedLevelName == "credit")
        {
            StartCoroutine(playCredits());
        }
    }

    public bool cutscenePlayed(string cutscene)
    {
        if(playedCutscenes == 0 && (cutscene == "cutscene1" || cutscene == "cutscene2")) {
            //first cutscene not played
            playedCutscenes++;
            return false;
        } else if(playedCutscenes == 1 && cutscene == "cutscene3") {
            //second cutscene not played
            playedCutscenes++;
            return false;
        } 

        //cutscene was played
        return true;
    }

    public float playCutscene(string cutscene)
    {
        if (cutscenePlayed(cutscene)) { return 0; }

        setVideoInfo(cutscene);

        movieAS.clip = mTex.audioClip;

        playVideo();

        return movieAS.clip.length;
    }

    IEnumerator playCredits()
    {
        setVideoInfo("credits");

        playVideo();

        yield return new WaitForSeconds(mTex.duration);

        Application.LoadLevel("menu");
    }

    private void setVideoInfo(string cutscene)
    {
        //get the attached GUITexture  
        videoGUItex = cutsceneHandler.GetComponent<GUITexture>();

        //get the attached AudioSource  
        movieAS = cutsceneHandler.GetComponent<AudioSource>();

        //load the movie texture from the resources folder  
        mTex = (MovieTexture)Resources.Load("Other/" + cutscene);

        //anamorphic fullscreen  
        videoGUItex.pixelInset = new Rect(Screen.width / 2, Screen.height / 2, 0, 0);

        //set the videoGUItex.texture to be the same as mTex  
        videoGUItex.texture = mTex;
    }

    private void playVideo()
    {
        //Plays the movie  
        mTex.Play();
        //plays the audio from the movie  
        movieAS.Play();
    }

    public void returnCutsceneTexture()
    {
        if(cutsceneHandler.GetComponent<GUITexture>().texture != null)
        {
            cutsceneHandler.GetComponent<GUITexture>().texture = null;
        }
    }
}
