using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class menuSmoke : MonoBehaviour {

	private Sprite[] pictures;
	private int i = 1;
	private GameObject smoke_frames;
	// Use this for initialization
	void Start () {
		pictures = new Sprite[22];
		//pictures = Resources.LoadAll<Sprite>("Other/menu_smoke_frames");
        for(int j = 1; j < 22; j++)
        {
            pictures[j] = Resources.Load<Sprite>("Other/menu_smoke_frames/Menu Scene_smoke animation_" + j);
        }
        //Time.captureFramerate = 10;
	}
	
	// Update is called once per frame
	void Update () {

        // Animation changes every 5 frames
        if(Time.frameCount % 5 != 0)
        {
            return;
        }

        if (i >= pictures.Length - 1)
        {
            i = 1;
        }
        else {
            i++;
        }

        smoke_frames = GameObject.FindGameObjectWithTag("MenuSmoke"); 
		smoke_frames.GetComponent<SpriteRenderer>().sprite = pictures[i];
	}
}
