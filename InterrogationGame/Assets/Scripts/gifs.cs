using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class gifs : MonoBehaviour
{

    private Sprite[] animation;
    private int i = 1, x = 0;
    private GameObject animation_object;

    public string folder_name = "", object_tag = "";
    // Use this for initialization
    void Start()
    {

        int bounds = find_bounds();

        x = find_update();

        animation = new Sprite[33];
        for (int j = 1; j < bounds; j++)
        {
            animation[j] = Resources.Load<Sprite>(folder_name + j);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Animation changes every x frames
        if (Time.frameCount % x != 0) {
            return;
        }

        if (i >= animation.Length - 1) {
            i = 1;
        } else {
            i++;
        }

        animation_object = GameObject.FindGameObjectWithTag(object_tag);

        StartCoroutine(fadeOut(animation_object));

        animation_object.GetComponent<SpriteRenderer>().sprite = animation[i];

        StartCoroutine(fadeIn(animation_object));
    }

    IEnumerator fadeIn(GameObject frame)
    {
        frame.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f, Mathf.SmoothStep(0f, 0.8f, 1f));
        yield return new WaitForSeconds(1f);
    }

    IEnumerator fadeOut(GameObject frame)
    {
        frame.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, Mathf.SmoothStep(0.8f, 0f, 1f));
        yield return new WaitForSeconds(1f);
        StopCoroutine("fadeOut");
    }

    private int find_bounds()
    {
        if (folder_name == "Other/smoke_frames/smoke_frame") {
            return 33;
        } else if (folder_name == "Other/menu_smoke_frames/Menu Scene_smoke animation_") {
            return 22;
        } else if (folder_name == "Other/dust_frames/smoke-") {
            return 48;
        } else {
            return 0;
        }
    }

    private int find_update()
    {
        if (folder_name == "Other/smoke_frames/smoke_frame") {
            return 7;
        } else if (folder_name == "Other/menu_smoke_frames/Menu Scene_smoke animation_") {
            return 5;
        } else if (folder_name == "Other/dust_frames/smoke-") {
            return 16;
        } else {
            return 0;
        }

    }
}