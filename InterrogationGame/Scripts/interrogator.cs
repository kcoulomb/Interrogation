using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class interrogator
{
    public Dictionary<string, int> moods;
    public Sprite[] pictures;
    public int current_image;

    public interrogator()
    {

        pictures = new Sprite[6];
        current_image = 0;

    }

    public interrogator(List<string> moodArray, Sprite[] picturesArray)
    {
        int temp = 1;

        for (int x = 0; x < moodArray.Count; x++)
        {
            if (moods.ContainsKey(moodArray[x]))
            {
                temp = moods[moodArray[x]] + 1;
            }
            moods.Add(moodArray[x], temp);
        }

        pictures = new Sprite[picturesArray.Length];
        current_image = 0;

        pictures = picturesArray;

    }

    public Sprite returnPicture(int n)
    {
        return pictures[n];
    }
}
