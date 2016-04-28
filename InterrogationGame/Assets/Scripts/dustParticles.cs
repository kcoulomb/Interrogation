using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class dustParticles : MonoBehaviour {

	private Sprite[] dust;
	private int i = 1;
	// Use this for initialization
	void Start () {
		dust = new Sprite[48];
		for(int j = 0; j < 48; j++)
		{
			if (j < 10) {
				dust [j] = Resources.Load<Sprite> ("Other/Dust_Frames/Full Scene_smoke particle animation_v4-export00" + (j));		
			} else if (j >= 10) {
				dust [j] = Resources.Load<Sprite> ("Other/Dust_Frames/Full Scene_smoke particle animation_v4-export0" + (j));
			}
		}
		//Time.captureFramerate = 10;
	}

	// Update is called once per frame
	void Update () {

		// Animation changes every 5 frames
		if(Time.frameCount % 16 != 0)
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

		GameObject dust_frames = GameObject.FindGameObjectWithTag("DustParticles");
        StartCoroutine(fadeOut(dust_frames));
        dust_frames.GetComponent<SpriteRenderer>().sprite = dust[i];
        StartCoroutine(fadeIn(dust_frames));
    }

    IEnumerator fadeIn(GameObject dust_frames)
    {
        dust_frames.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f, Mathf.SmoothStep(0f, 0.8f, 1f));
        yield return new WaitForSeconds(1f);
    }

    IEnumerator fadeOut(GameObject dust_frames)
    {
        dust_frames.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, Mathf.SmoothStep(0.8f, 0f, 1f));
        yield return new WaitForSeconds(1f);
        StopCoroutine("fadeOut");
    }
}