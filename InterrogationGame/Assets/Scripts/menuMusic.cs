using UnityEngine;
using System.Collections;

public class menuMusic : MonoBehaviour {

    public bool startMusic;
    private GameObject music;

	// Use this for initialization
	void Awake () {
        if (Application.loadedLevelName == "outro")
        {
            music = GameObject.FindGameObjectWithTag("creditMusic");
        }
        
        if (!startMusic)
        {
            music.GetComponent<AudioSource>().Play();
            DontDestroyOnLoad(music);
            startMusic = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if(Application.loadedLevelName == "menu")
        {
            music.GetComponent<AudioSource>().Stop();
            startMusic = false;
            return;
        }
    }
}
