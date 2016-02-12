using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class updateSystem : MonoBehaviour
{
    //clicked button
    public GameObject button;
    //reference to arrayTest script = so we can use their functions
    public system get_dialogue;

    public changeScene scene;

    public void changeText()
    {

        //grab all three buttons on the scene
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");
        GameObject question_box = GameObject.FindGameObjectWithTag("Question");
        GameObject interrogatorPictures = GameObject.FindGameObjectWithTag("Interrogator");

        //find out which button was selected
        //  1 = top-right
        //  2 = top-left
        //  3 = bottom-middle
        int selected_button_num = 0;
        while(buttons[selected_button_num] != button) {
            selected_button_num++;
        }

        //determine if the player response was true/correct
        get_dialogue.true_or_false(selected_button_num);

        //change the current dialogue system
        if (get_dialogue.current_system.children[selected_button_num] == 100) {
            scene.moveScene("menu");
        }

        //if the player has answered few consecutive falses, the game continues as usual
        //but if the player has 2+ consecutive falses, they are given the proxy question

        get_dialogue.change_audio(); //<-------------- Change audio here

        if (get_dialogue.dialogue_tree.consecutive_false < 2) { 
            get_dialogue.addNode(get_dialogue.current_system.children[selected_button_num]);
        }
        else
        {
            get_dialogue.check_consecutive_false();
        }
        

        //get the current dialogue system
        List<string> new_text = get_dialogue.getDialogueSystem();

        question_box.GetComponent<Text>().text = get_dialogue.getQuestion();

        interrogatorPictures.GetComponent<Image>().sprite = get_dialogue.getCurrentImage();

        //change the text of each button
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].GetComponentInChildren<Text>().text = new_text[i];
        }
    }
   
}
