using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class outro : MonoBehaviour {

    public changeScene goto_credits;

	// Use this for initialization
	IEnumerator Start () {
        //audiosource for all of our sound effects
        AudioSource sound_effects = gameObject.AddComponent<AudioSource>();
        //audiosource for interrogator's voice
        AudioSource int_voice = gameObject.AddComponent<AudioSource>();

        int_voice.clip = Resources.Load("Voices/Cutscenes/end") as AudioClip;
        int_voice.Play();

        yield return new WaitForSeconds(int_voice.clip.length / 2);

        sound_effects.clip = Resources.Load("SoundEffects/chair") as AudioClip;
        sound_effects.Play();

        GameObject background_image = GameObject.FindGameObjectWithTag("Interrogator");
        Sprite background = new Sprite();

        background = Resources.Load<Sprite>("Interrogator/picG");
        background_image.GetComponent<Image>().sprite = background;

        yield return new WaitForSeconds(int_voice.clip.length/2);

        sound_effects.clip = Resources.Load("SoundEffects/foot") as AudioClip;
        sound_effects.Play();

        background = Resources.Load<Sprite>("Other/background1");
        background_image.GetComponent<Image>().sprite = background;

        yield return new WaitForSeconds(sound_effects.clip.length);

        sound_effects.clip = Resources.Load("SoundEffects/door") as AudioClip;
        sound_effects.Play();

        yield return new WaitForSeconds(sound_effects.clip.length);

        goto_credits.moveScene("credit");
    }
}
