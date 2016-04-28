using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class cigSmokeGif : MonoBehaviour {

	private Sprite[] smoke;
	private int i = 1;
	private GameObject smoke_frames;
	// Use this for initialization
	void Start () {
		smoke = new Sprite[33];
		for(int j = 0; j < 34; j++)
		{
			smoke[j] = Resources.Load<Sprite>("Other/ashe_smoke_frames/smoke_frame" + (j+1));
		}
		//Time.captureFramerate = 10;
	}

	// Update is called once per frame
	void Update () {

		Debug.Log ("udpate smoke");
		// Animation changes every 5 frames
		if(Time.frameCount % 5 != 0)
		{
			return;
		}

		if (i >= smoke.Length - 1)
		{
			i = 1;
		}
		else {
			i++;
		}

		smoke_frames = GameObject.FindGameObjectWithTag("CigSmoke"); 
		smoke_frames.GetComponent<SpriteRenderer>().sprite = smoke[i];
	}
}