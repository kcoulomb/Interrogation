using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;

public class system : MonoBehaviour {

    public dialoguetree dialogue_tree = null;
    public node current_system = null;
    public blink effects;
    string[] textFile = null;
    private bool can_click = true;    

    //variables for heat map
    public string[] heatmap = null;
    public string playthroughPath = null;
    public int fuck = 103;
    public Boolean transition = false;
    public int previousFalseValue = 0;

    // ------ Start ------
    // 1. Creates the dialogue tree to hold all of the nodes
    // 2. Reads in the text file and stores each line in an array
    // 3. Reads in the heatmap
    // 4. Creates the initial node and sets the most recent node to the first node
    void Start()
    {

        dialogue_tree = new dialoguetree();

        textFile = System.IO.File.ReadAllLines(@"Assets\Scripts\test-text.txt");
        heatmap = System.IO.File.ReadAllLines(@"Assets\Scripts\heatmap.txt");

        addNode(0);
        dialogue_tree.last_node = dialogue_tree.nodes[0];
    }

    // ------ Add Node ------
    // 1. Creates a new node with the information from the textfile
    // 2. Adds the new node to the dialogue tree
    // 3. Sets the current system to the new node
    // 4. Changes the interrogator's image
    public void addNode(int textFileIndex)
    {
        string[] words = textFile[textFileIndex].Split(';');

        dialogue_tree.nodes.Add(new node(words[0], words[1], words[2], words[3],
            convertBool(words[4]), convertBool(words[5]), convertBool(words[6]), words[10], words[11], words[12]));

        dialogue_tree.nodes[dialogue_tree.nodes.Count - 1].createGraph(convertStringtoInt(words[7]),
            convertStringtoInt(words[8]), convertStringtoInt(words[9]));

        dialogue_tree.nodes[dialogue_tree.nodes.Count - 1].file_index = textFileIndex;

        current_system = dialogue_tree.nodes[dialogue_tree.nodes.Count - 1];

        dialogue_tree.last_node = current_system;
        dialogue_tree.last_node_index = dialogue_tree.nodes.Count - 1;
    }

    public void updateHeatmap(int index)
    {
        playthroughPath += "-->" + (index + 1).ToString();
        heatmap[index] += "*";
    }

    // ----------- GET FUNCTIONS -------------

    // ------ getDialogueSystem ------
    // 1. gets the list of player responses avaliable at the current system
    public List<string> getDialogueSystem()
    {
        List<string> curr_dialogue = new List<string>(current_system.responses);
        return curr_dialogue;
    }

    // ------ getCurrentImage ------
    // 1. gets the interrogator's current image
    public Sprite getCurrentImage()
    {
        return dialogue_tree.interrogator.pictures[dialogue_tree.interrogator.current_image];
    }

    // ------ getQuestion ------
    // 1. gets the question presented to the player at the current system
    public string getQuestion()
    {
      return current_system.question;
        
    }

    // ------ getSubtitle ------
    // 1. gets the subtitle of the chosen answer
    public string getSubtitle(int subtitle_index)
    {
     return current_system.subtitles[subtitle_index];
        
    }


    // ------ getConnection ------
    // 1. gets the connection number of the chosen answer
    public int getConnection(int conn_index)
    {
        return current_system.children[conn_index];
    }

    // ------ getCurrentIndex ------
    // 1. gets the current index of the system
    public int getCurrentIndex()
    {
        return current_system.file_index;
    }

    // ------ numFalses ------
    // 1. gets the current number of consecutives falses
    public int numFalses()
    {
        return dialogue_tree.consecutive_false;
    }

    // ----------- OTHER FUNCTIONS -------------

    // ------ true_or_false------
    // 1. determines whether the player's response was correct/true or not
    // 2. increases/decreases consecutive falses
    public void true_or_false(int truth_index)
     {  
        if (!current_system.truths[truth_index])
        {
            dialogue_tree.consecutive_false++;
          
            //effects.change_effect(true, dialogue_tree.consecutive_false);
        }
        else
        {
            dialogue_tree.consecutive_false = 0;

            //effects.change_effect(false, dialogue_tree.consecutive_false);
        }
        
    }

    // ------ check_click ------
    //  1. Checks if the player is allowed to click buttons
    public bool check_click()
    {
        return can_click;
    }

    // ------ change_can_click ------
    //  1. switches the true/false value of can_click
    public void change_can_click()
    {
        can_click = !can_click;
    }

    // ------ change_interrogator_image ------
    // 1. changes interrogator's image
    // 2. plays sound effects attached to image
    public void change_interrogator_image()
    {
        transition = false;
        int current_img = dialogue_tree.interrogator.current_image;
        Debug.Log("before: " + current_img);
        if (dialogue_tree.consecutive_false == 0)
        {
            if (previousFalseValue == 2 || previousFalseValue > 3)
            {
                dialogue_tree.interrogator.current_image = 13;
                transition = true;
            }
            else
            {
                dialogue_tree.interrogator.current_image = UnityEngine.Random.Range(0, 2);
            }
        }
        else if (dialogue_tree.consecutive_false == 1)
        {
            dialogue_tree.interrogator.current_image = UnityEngine.Random.Range(3, 4);
        }
        else if (dialogue_tree.consecutive_false == 2)
        {
            dialogue_tree.interrogator.current_image = 12;
            transition = true;
        }
        else if (dialogue_tree.consecutive_false == 3)
        {
            dialogue_tree.interrogator.current_image = 13;
            transition = true;
        }
        else if (dialogue_tree.consecutive_false > 3 && previousFalseValue == 3)
        {
            dialogue_tree.interrogator.current_image = 12;
            transition = true;
        }
        else if (dialogue_tree.consecutive_false > 3 && previousFalseValue > 3)
        {
            dialogue_tree.interrogator.current_image = UnityEngine.Random.Range(10, 11);
        }

        if (dialogue_tree.interrogator.current_image == 1 && dialogue_tree.interrogator.current_image != current_img)
        {
            AudioSource paperSound;
            paperSound = gameObject.AddComponent<AudioSource>();
            paperSound.clip = Resources.Load("SoundEffects/paper") as AudioClip;
            paperSound.Play();
        }
        else if (dialogue_tree.consecutive_false == 2 && dialogue_tree.interrogator.current_image != current_img)
        {
            AudioSource chairSound;
            chairSound = gameObject.AddComponent<AudioSource>();
            chairSound.clip = Resources.Load("SoundEffects/chair2") as AudioClip;
            chairSound.Play();
        }
        else if (dialogue_tree.consecutive_false == 3 && dialogue_tree.interrogator.current_image != current_img)
        {
            AudioSource chairSound;
            chairSound = gameObject.AddComponent<AudioSource>();
            chairSound.clip = Resources.Load("SoundEffects/chair2") as AudioClip;
            chairSound.Play();
        }
        else if (dialogue_tree.interrogator.current_image == 7 && dialogue_tree.interrogator.current_image != current_img)
        {
            AudioSource exhaleSound;
            exhaleSound = gameObject.AddComponent<AudioSource>();
            exhaleSound.clip = Resources.Load("SoundEffects/exhale") as AudioClip;
            exhaleSound.Play();
        }
        else if (dialogue_tree.consecutive_false > 3 && dialogue_tree.interrogator.current_image != current_img)
        {
            AudioSource tableSlam;
            tableSlam = gameObject.AddComponent<AudioSource>();
            tableSlam.clip = Resources.Load("SoundEffects/table") as AudioClip;
            tableSlam.Play();
        }

        previousFalseValue = dialogue_tree.consecutive_false;
    }

    // ------ convertBool ------
    // 1. converts a "true" or "false" string to a boolean
    private bool convertBool(string temp)
    {
        if (temp == "false")
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // ------ convertStringtoInt ------
    // 1. converts a string to an integer
    private int convertStringtoInt(string temp)
    {
        int connection_num;
        int.TryParse(temp, out connection_num);
        return connection_num;
    }

    void OnApplicationQuit()
    {
        heatmap[124] = heatmap[122];
        heatmap[122] = heatmap[120];
        heatmap[120] = heatmap[118];
        heatmap[118] = heatmap[116];
        heatmap[116] = heatmap[114];
        heatmap[114] = heatmap[112];
        heatmap[112] = heatmap[110];
        heatmap[110] = heatmap[108];
        heatmap[108] = heatmap[106];
        heatmap[106] = heatmap[104];

        heatmap[104] = playthroughPath;

        Debug.Log("Application ending");
        Debug.Log(playthroughPath);

        System.IO.File.WriteAllText(@"Assets\Scripts\heatmap.txt", string.Empty);
        System.IO.File.WriteAllLines(@"Assets\Scripts\heatmap.txt", heatmap);
    }

}