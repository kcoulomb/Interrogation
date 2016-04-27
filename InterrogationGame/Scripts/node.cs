using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class node
{
    public string question;

    public List<string> responses;

    public List<string> subtitles;

    public List<bool> truths;

    public node parent;

    public List<int> children;

    public int file_index;

    public node()
    {
        responses = new List<string>();

        question = null;
        responses.Add(null);
        responses.Add(null);
        responses.Add(null);

    }

    public node(string ques, string ans1, string ans2, string ans3, bool truth1, 
                bool truth2, bool truth3, string sub1, string sub2, string sub3)
    {
        //Added: the lists need to be constructed for the strings to be added to them
        responses = new List<string>();
        truths = new List<bool>();
        subtitles = new List<string>();

        question = ques;
        responses.Add(ans1);
        responses.Add(ans2);
        responses.Add(ans3);
        truths.Add(truth1);
        truths.Add(truth2);
        truths.Add(truth3);
        subtitles.Add(sub1);
        subtitles.Add(sub2);
        subtitles.Add(sub3);
    }


    //creates links among laist that infers a tree. 
    //the children are your connection based on your response to the prompted question

    public void createGraph(int con0, int con1, int con2)
    {
        children = new List<int>();

        children.Add(con0);
        children.Add(con1);
        children.Add(con2);

    }

}
