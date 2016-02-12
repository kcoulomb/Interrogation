using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class interrogator
{
    public List<string> moods;
    public Sprite[] pictures;
    public int current_image;

    public interrogator()
    {
        moods = new List<string>();
        pictures = new Sprite[10];
        current_image = 0;

        for (int n = 0; n < 5; n++)
            {
                moods.Add(null);
            }

        }

    public interrogator(List<string> moodArray, Sprite[] picturesArray)
    {
        moods = new List<string>();
        pictures = new Sprite[picturesArray.Length];
        current_image = 0;

        moods = moodArray;
        pictures = picturesArray;

    }

    public Sprite returnPicture(int n)
    {
        return pictures[n];
    }

    public string returnMood(int n)
    {
        return moods[n];
    }
}
