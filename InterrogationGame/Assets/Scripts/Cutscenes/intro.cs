using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class intro : MonoBehaviour {

    private AudioSource sound_effects;
    public blink vest_tutorial;
    public changeScene move_to;
    public cutScenePlayer cutsceneHandler;

    // Use this for initialization
    IEnumerator Start () {
        //make button disappear/inactive
        GameObject button = GameObject.FindGameObjectWithTag("Button");
        button.SetActive(false);

        //play door close and open
        //background is currently empty
        sound_effects = gameObject.AddComponent<AudioSource>();
        sound_effects.clip = Resources.Load("SoundEffects/door") as AudioClip;

        sound_effects.Play();

        yield return new WaitForSeconds(sound_effects.clip.length);
        
        //once the doors closes, play footstep sounds
        //background still empty
        sound_effects.clip = Resources.Load("SoundEffects/foot") as AudioClip;

        sound_effects.Play();

        yield return new WaitForSeconds(sound_effects.clip.length);
        
        //then play seat moving back & sitting down noise
        sound_effects.clip = Resources.Load("SoundEffects/chair") as AudioClip;

        sound_effects.Play();

        yield return new WaitForSeconds(sound_effects.clip.length);

        //background becomes interrogator
        Sprite background = new Sprite();
        background = Resources.Load<Sprite>("Interrogator/pic2");
        GameObject background_image = GameObject.FindGameObjectWithTag("Interrogator");
        background_image.GetComponent<Image>().sprite = background;

        //plays the paper shuffle SFX on pic2
        AudioSource paperSound;
        paperSound = gameObject.AddComponent<AudioSource>();
        paperSound.clip = Resources.Load("SoundEffects/paper") as AudioClip;
        paperSound.Play();

        //replace with interrogator's opening statement pt.1
        sound_effects.clip = Resources.Load("Voices/Cutscenes/intro1") as AudioClip;
        sound_effects.Play();
        yield return new WaitForSeconds(sound_effects.clip.length);

        background = Resources.Load<Sprite>("Interrogator/pic4");
        background_image.GetComponent<Image>().sprite = background;

        sound_effects.clip = Resources.Load("Voices/Cutscenes/intro2") as AudioClip;
        sound_effects.Play();
        yield return new WaitForSeconds(sound_effects.clip.length);

        background = Resources.Load<Sprite>("Interrogator/pic1");
        background_image.GetComponent<Image>().sprite = background;

        sound_effects.clip = Resources.Load("Voices/Cutscenes/intro3") as AudioClip;
        sound_effects.Play();
        yield return new WaitForSeconds(sound_effects.clip.length);

        //plays the paper shuffle SFX on pic2
        paperSound = gameObject.AddComponent<AudioSource>();
        paperSound.clip = Resources.Load("SoundEffects/paper") as AudioClip;
        paperSound.Play();

        background = Resources.Load<Sprite>("Interrogator/pic2");
        background_image.GetComponent<Image>().sprite = background;

        sound_effects.clip = Resources.Load("Voices/Cutscenes/intro4") as AudioClip;
        sound_effects.Play();
        yield return new WaitForSeconds(sound_effects.clip.length);

        //change background to the mug shot picture
        //& make button reappear and active
        background = Resources.Load<Sprite>("Other/mug_shot");
        background_image.GetComponent<Image>().sprite = background;
        
        button.SetActive(true);        
    }

    public void answer_false(){
        //literally just here cause we can't have a button go to an IEnumerator :(
        //UNCOMMENT THIS WHEN CONNECTED TO VEST
        StartCoroutine("after_false");
    }

    IEnumerator after_false()
    {
        //turns on vest
        //vest_tutorial.change_effect(true);

        //needs to change to correct sound effect
        //interrogator explains the vest's reacting
        //replace with interrogator's opening statement pt.1
        sound_effects = gameObject.AddComponent<AudioSource>();
        sound_effects.clip = Resources.Load("Voices/Cutscenes/introvest") as AudioClip;

        sound_effects.Play();

        yield return new WaitForSeconds(sound_effects.clip.length);

        //turns off vest
        //vest_tutorial.change_effect(false);

        //interrogator begins game
        yield return new WaitForSeconds(2);

        float cutsceneWait = cutsceneHandler.playCutscene("cutscene1");

        yield return new WaitForSeconds(cutsceneWait);

        //move to game
        move_to.moveScene("test");
    }

}