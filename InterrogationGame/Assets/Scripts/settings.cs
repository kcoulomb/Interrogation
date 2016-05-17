using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class settings : MonoBehaviour {

    private float music_volume = 1;
   
	void Awake()
    {
            DontDestroyOnLoad(transform.gameObject);

        if(FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    public void changeMusicVolume(float new_volume)
    {
        music_volume = new_volume;
    }

    public float getMusicVolume()
    {
        return music_volume;
    }
}
