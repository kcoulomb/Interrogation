using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UIMenuManager : MonoBehaviour {

    private GameObject options_panel;
    public Slider volume_slider;
    //public settings set;
    private GameObject set_object;
    private settings set;

    void Awake()
    {
        options_panel = GameObject.FindGameObjectWithTag("OptionsPanel");
        options_panel.SetActive(false);

        set_object = GameObject.FindGameObjectWithTag("OptionsKeep");
        set = set_object.GetComponent<settings>();

        volume_slider.GetComponent<Slider>().value = set.getMusicVolume();
    }

    public void showOptions()
    {
        options_panel.SetActive(true);
    }

    public void hideOptions()
    {
        options_panel.SetActive(false);
    }

    public void volumeChanged()
    {
        AudioListener.volume = volume_slider.GetComponent<Slider>().value;
        set.changeMusicVolume(AudioListener.volume);
    }

    public void quit()
    {
        Application.Quit();
    }
}
