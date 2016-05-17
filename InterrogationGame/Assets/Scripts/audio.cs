using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;



public class audio : MonoBehaviour
{
    public AudioMixerSnapshot trackOne;
    public AudioMixerSnapshot trackTwo;
    public AudioMixerSnapshot trackThree;
    public AudioSource stingOneSource;
    public AudioSource stingTwoSource;
    public float bpm = 128;

    public UIManager checkIfPaused;

    private float m_TransitionIn;
    private float m_QuarterNote;

    //interrogator's voice track
    private AudioSource int_voice;
    private AudioSource cer_voice;

    void Start()
    {
        int_voice = gameObject.AddComponent<AudioSource>();
        cer_voice = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        

        if(checkIfPaused.isPaused == true)
        {
            int_voice.Pause();
            cer_voice.Pause();
            //AudioListener.pause = true;  // pauses the entire audio system including music and soundFX
        }
        else if(checkIfPaused.isPaused == false)
        {
            int_voice.UnPause();
            cer_voice.UnPause();
            // AudioListener.pause = false;
        }


    }

    public void change_audio(int consecutive_false)
    {
        m_QuarterNote = 10 / bpm;
        m_TransitionIn = m_QuarterNote;          //controls the length of fade in/fade out

        //changes audio depending on consecutive falses    
        if (consecutive_false == 0)
        { //plays first track as default
            trackOne.TransitionTo(m_TransitionIn);
        }
        else if (consecutive_false == 1)
            { //adds on second track if answered one incorrectly

            stingOneSource.Play();

            trackTwo.TransitionTo(m_TransitionIn);
            Debug.Log("Nah");
        }
        else if (consecutive_false > 1)
        { //plays all three tracks if answered two incorrectly

            stingTwoSource.Play();

            trackThree.TransitionTo(m_TransitionIn);
            Debug.Log("Nah");


        }

    }

    //plays the appropriate dialogue for the interrogator
    public float play_int_voice(int index)
    {
        int_voice.clip = Resources.Load("Voices/Interrogator/node" + index + "neutral") as AudioClip;
        int_voice.Play();
        return int_voice.clip.length;

    } 
    //plays the appropriate dialogue for Cervantes
    //voices/interrogator/node[file_index]-[selectedbuttonnum]neutral
    public float play_cer_voice(int file_index, int button_index)
    {
       cer_voice.clip = Resources.Load("Voices/Cervantes/node" + file_index + "-" + button_index + "neutral") as AudioClip;
       cer_voice.Play();
        return cer_voice.clip.length;

    }
    
}

    
    
