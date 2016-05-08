using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class dialoguetree
{
    public List<node> nodes;

    public int consecutive_false;

    public interrogator interrogator;

    public node last_node;

    public int last_node_index = 0;

    public dialoguetree()
    {
        nodes = new List<node>();
        last_node = null;

        interrogator = new interrogator();

        interrogator.pictures = Resources.LoadAll<Sprite>("Interrogator");

        consecutive_false = 0;

    }
}