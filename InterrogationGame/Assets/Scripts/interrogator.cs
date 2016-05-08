using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class interrogator
{
    public Sprite[] pictures;
    public int current_image;

    public interrogator()
    {

        pictures = new Sprite[6];
        current_image = 0;

    }

    public Sprite returnPicture(int n)
    {
        return pictures[n];
    }
}
