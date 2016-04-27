using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class dustParticles : MonoBehaviour {

	private Sprite[] dust;
	private int i = 1;
	private GameObject dust_frames;
	// Use this for initialization
	void Start () {
		dust = new Sprite[144];
		for(int j = 0; j < 144; j++)
		{
			if (j < 10) {
				dust [j] = Resources.Load<Sprite> ("Other/Dust_Frames/Full Scene_smoke particle animation_v4-export00" + (j));		
			} else if (j >= 10 && j < 100) {
				dust [j] = Resources.Load<Sprite> ("Other/Dust_Frames/Full Scene_smoke particle animation_v4-export0" + (j));
			} else {
				dust [j] = Resources.Load<Sprite> ("Other/Dust_Frames/Full Scene_smoke particle animation_v4-export" + (j));
			}
		}
		//Time.captureFramerate = 10;
	}

	// Update is called once per frame
	void Update () {

		Debug.Log ("udpate dust");
		// Animation changes every 5 frames
		if(Time.frameCount % 5 != 0)
		{
			return;
		}

		if (i >= dust.Length - 1)
		{
			i = 1;
		}
		else {
			i++;
		}

		dust_frames = GameObject.FindGameObjectWithTag("DustParticles"); 
		dust_frames.GetComponent<SpriteRenderer>().sprite = dust[i];
	}
}