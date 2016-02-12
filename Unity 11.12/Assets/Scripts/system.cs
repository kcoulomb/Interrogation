using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class system : MonoBehaviour {

    public dialoguetree dialogue_tree = null;
    public node current_system = null;
    public blink effects;
    public interrogator interrogator = null;
    public int current_image = 0;
    string[] textFile = null;

    public AudioMixerSnapshot trackOne;
    public AudioMixerSnapshot trackTwo;
    public AudioMixerSnapshot trackThree;
    public AudioSource stingOneSource;
    public AudioSource stingTwoSource;
    public float bpm = 128;

    private float m_TransitionIn;
    private float m_TransitionOut;
    private float m_QuarterNote;

    void Start()
    {
        //creating our dialogue_tree, which will hold all the nodes the player
        //has reached
        dialogue_tree = new dialoguetree();

        //reads in all the lines of the textfile and stores into an array
        //the textfile contains the information of all nodes that can be created
        //in a playthrough
        textFile = System.IO.File.ReadAllLines(@"Assets\Scripts\test-text.txt");
        
        //creates the first node in the dialogue system
        //introductory node
        addNode(0);

        //makes our most recent node the first node created
        dialogue_tree.last_node = dialogue_tree.nodes[0];



        //calculates length of quarternote in miliseconds
        m_QuarterNote = 0/bpm;
        m_TransitionIn = m_QuarterNote; //quick fade in
        m_TransitionOut = m_QuarterNote; //longer fade out
    }

    //adds a new node to the tree at the specified index in the text file
    //      grabs the new node's information at like textFileIndex and creates a node with that information
    //      the new node is then added to our dialogue_tree
    //      the current_system is then updated to the new node
    public void addNode(int textFileIndex)
    {
        //splits up the line of text in textfile that contains the new node's information
        string[] words = textFile[textFileIndex].Split(';');

        //creates the new node and adds it to the last spot in the tree
        dialogue_tree.nodes.Add(new node(words[0], words[1], words[2], words[3], convertBool(words[4]), convertBool(words[5]), convertBool(words[6])));
        dialogue_tree.nodes[dialogue_tree.nodes.Count - 1].createFam(convertStringtoInt(words[7]), convertStringtoInt(words[8]), convertStringtoInt(words[9]));
        dialogue_tree.nodes[dialogue_tree.nodes.Count - 1].file_index = textFileIndex;
        //updates the current system to the new node
        current_system = dialogue_tree.nodes[dialogue_tree.nodes.Count - 1];

        //makes the most recent truth the new current system
        dialogue_tree.last_node = current_system;
        dialogue_tree.last_node_index = dialogue_tree.nodes.Count - 1;

        //change interrogator image
        change_interrogator_image();
    }

    //gets the list of player responses avaliable at the current system
    public List<string> getDialogueSystem()
    {
        List<string> curr_dialogue = new List<string>(current_system.responses);
        return curr_dialogue;
    }

    public Sprite getCurrentImage()
    {
        return dialogue_tree.interrogator.pictures[dialogue_tree.interrogator.current_image];
    }

    //gets the question presented to the player at the current system
    public string getQuestion()
    {
        return current_system.question;
    }

    //determines whether the player's response was correct/true or not
    public void true_or_false(int index)
     {
        
        if (!current_system.truths[index])
        {
            //Is true that we're going to change the effect

            //increase consecutive falses
            dialogue_tree.consecutive_false++;
          

            //continues or starts effect
            effects.change_effect(true, dialogue_tree.consecutive_false);
        }
        else
        {
            //Is false that we're going to change the effect

            //resets consecutive falses to 0
            dialogue_tree.consecutive_false = 0;

            //changes next effect to be featured
            effects.change_effect(false, dialogue_tree.consecutive_false);
        }
        
    }

    public void check_consecutive_false()
    {
        //creates the proxy mode and appends it the last spot in the tree
        //the proxy's children are the index in the file to most recent true player choice
        dialogue_tree.nodes.Add(new node("Stop lying to me you piece of shit.", "Soz.", "My bad homes.", "Fuck off", false, false, false));
        dialogue_tree.nodes[dialogue_tree.nodes.Count - 1].createFam(dialogue_tree.nodes[dialogue_tree.last_node_index].file_index, dialogue_tree.nodes[dialogue_tree.last_node_index].file_index, dialogue_tree.nodes[dialogue_tree.last_node_index].file_index);

        //updates the current system to the new node
        current_system = dialogue_tree.nodes[dialogue_tree.nodes.Count - 1];

        //resets consecutive falses
        dialogue_tree.consecutive_false = -1;

        //changes interrogator's image
        change_interrogator_image();
    }

    //changes audio depending on consecutive falses    
    public void change_audio()
    {
        if (dialogue_tree.consecutive_false == 1){
            stingOneSource.Play();
             trackTwo.TransitionTo(m_TransitionIn); //fade in Track Two
            
        }
        else if (dialogue_tree.consecutive_false >= 2){
           stingTwoSource.Play();
             trackThree.TransitionTo(m_TransitionIn); //fade in Track Three
           
        }
        else if(dialogue_tree.consecutive_false == 0){
             trackOne.TransitionTo(m_TransitionIn); //fade in Track one
             trackTwo.TransitionTo(m_TransitionOut); //fade out Track Two
             trackThree.TransitionTo(m_TransitionOut); //fade out Track Three
            
        }

    }

    //changes interrogator's image
    private void change_interrogator_image()
    {
        dialogue_tree.interrogator.current_image += 1;
        if (dialogue_tree.interrogator.current_image > 2)
        {
            dialogue_tree.interrogator.current_image = 0;
        }
    }

    //converts a "true" or "false" string to a boolean
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

    //converts a string to an integer
    private int convertStringtoInt(string temp)
    {
        int connection_num;
        int.TryParse(temp, out connection_num);
        return connection_num;
    }
}
    