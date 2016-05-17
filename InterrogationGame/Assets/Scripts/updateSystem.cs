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
    //reference to script that takes care of everything audio
    public audio audioTracks;

    public changeScene scene;

    public UIManager checkIfPaused;

    public cutScenePlayer cutsceneHandler;

    public float wait_cer = 0;
    public float wait_int = 0;

    int prevConsecFalse = 0;
    private GameObject[] buttons;
    private GameObject question_text;
    private GameObject question_box;


    void Awake()
    {
        buttons = GameObject.FindGameObjectsWithTag("Button");
        question_text = GameObject.FindGameObjectWithTag("Question");
        question_box = GameObject.FindGameObjectWithTag("QuestionBox");

        for(int i = 0; i < buttons.Length; i++)
        {
            button.GetComponent<Image>().CrossFadeColor(Color.black, 0f, true, true);
        }

    }

    // ------ update ------
    // 1. determines if buttons can be clicked/which button was clicked
    // 2. plays Cervantes' dialogue w/ subtitles
    // 3. determines if the vest should activate
    public void update()
    {
        //GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");
        //GameObject question_text = GameObject.FindGameObjectWithTag("Question");
        //GameObject question_box = GameObject.FindGameObjectWithTag("QuestionBox");

        if (!get_dialogue.check_click()) {
            return;
        } else
        {
            get_dialogue.change_can_click();

            //question_text.GetComponent<Text>().text = "";
            fadeOutPic(question_text);
            fadeOutPic(question_box);
        }        

        int selected_button_num = 0;
        while (buttons[selected_button_num] != button){
            selected_button_num++;
        }
        
        if (get_dialogue.getCurrentIndex() == get_dialogue.getConnection(selected_button_num)) { get_dialogue.change_can_click(); return; }      
        
        wait_cer = audioTracks.play_cer_voice(get_dialogue.getCurrentIndex(), selected_button_num + 1);

        //question_text.GetComponent<Text>().text = get_dialogue.getSubtitle(selected_button_num);

        get_dialogue.true_or_false(selected_button_num);

        audioTracks.change_audio(get_dialogue.dialogue_tree.consecutive_false);

        //looks at the selected button num declared above to see which answer was selected
        //takes the other answers and fades them out
        for (int x = 0; x < 3; x++)
        {
            if (x != selected_button_num) {
                fadeOutAnswerText(buttons[x]);
            }
            else if ((x == selected_button_num) && (prevConsecFalse < get_dialogue.numFalses()))
            {
                redButton(buttons[x]);
                prevConsecFalse = get_dialogue.numFalses();
                //this is the button that was selected
                //this button we change the color of, not fade in any way
            }
        }
        
        StartCoroutine(changeText(selected_button_num, buttons, question_text, question_box, wait_cer));
       
    }

    // ------ changeText ------
    // 1. updates the heat map
    // 2. adds new node to the tree
    // 3. changes interrogator's image
    // 4. changes the button and question text
    IEnumerator changeText(int selected_button_num, GameObject[] buttons, GameObject question_text, GameObject question_box, float wait_cer)
    {
        yield return new WaitForSeconds(wait_cer);

        yield return new WaitForSeconds(sceneHandler(selected_button_num));

        cutsceneHandler.returnCutsceneTexture();

        get_dialogue.updateHeatmap(get_dialogue.current_system.children[selected_button_num]);

        get_dialogue.addNode(get_dialogue.getConnection(selected_button_num));

        GameObject interrogatorPictures = GameObject.FindGameObjectWithTag("Interrogator");

        get_dialogue.change_interrogator_image();

        yield return new WaitForSeconds(0.8f);

        interrogatorPictures.GetComponent<Image>().sprite = get_dialogue.getCurrentImage();

        fadeInPic(interrogatorPictures);

        yield return new WaitForSeconds(1f);

        if (get_dialogue.transition)
        {

            if (get_dialogue.dialogue_tree.consecutive_false == 0)
            {

                get_dialogue.dialogue_tree.interrogator.current_image = UnityEngine.Random.Range(0, 2);

            }
            else if (get_dialogue.dialogue_tree.consecutive_false == 2)
            {

                get_dialogue.dialogue_tree.interrogator.current_image = UnityEngine.Random.Range(5, 8);

            }
            else if (get_dialogue.dialogue_tree.consecutive_false == 3)
            {

                get_dialogue.dialogue_tree.interrogator.current_image = 9;

            }
            else if (get_dialogue.dialogue_tree.consecutive_false > 3)
            {

                get_dialogue.dialogue_tree.interrogator.current_image = UnityEngine.Random.Range(10, 11);

            }

            interrogatorPictures.GetComponent<Image>().sprite = get_dialogue.getCurrentImage();

            fadeInPic(interrogatorPictures);

            yield return new WaitForSeconds(1f);

        }

        wait_int = audioTracks.play_int_voice(get_dialogue.current_system.file_index);

        List<string> new_text = get_dialogue.getDialogueSystem();

        question_text.GetComponent<Text>().text = get_dialogue.getQuestion();

        fadeInPic(question_box);
        fadeInPic(question_text);

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i != selected_button_num)
            {
                fadeInAnswerText(buttons[i]);
                buttons[selected_button_num].GetComponent<Image>().CrossFadeColor(Color.black, 1.0f, false, true);
            }            
            buttons[i].GetComponentInChildren<Text>().text = new_text[i];
        }

        yield return new WaitForSeconds(wait_int);

        get_dialogue.change_can_click();
        
    }

    void fadeOutPic(GameObject pic)
    {
        pic.GetComponent<Graphic>().CrossFadeAlpha(0, 0.5f, false);
    }

    void fadeInPic(GameObject pic)
    {
        pic.GetComponent<Graphic>().CrossFadeAlpha(0f, 0f, false);
        pic.GetComponent<Graphic>().CrossFadeAlpha(1f, 0.5f, false);
    }
    
    void fadeOutAnswerText(GameObject button)
    {
        button.GetComponent<Graphic>().CrossFadeAlpha(0.0f, 1.0f, false);
        button.GetComponentInChildren<Text>().CrossFadeAlpha(0.0f, 1.0f, false);
    }

    void fadeInAnswerText(GameObject button)
    {
        button.GetComponent<Graphic>().CrossFadeAlpha(1.0f, 1.0f, false);
        button.GetComponentInChildren<Text>().CrossFadeAlpha(1.0f, 1.0f, false);
    }

    // Changes button color to red if player answers incorrectly
    void redButton(GameObject button)
    {
        button.GetComponent<Image>().CrossFadeColor(Color.red, 1.0f, true, true);
    }

    // Triggers cutscenes or moves to credits
    private float sceneHandler(int selected_button_num)
    {
        int destination_node = get_dialogue.current_system.children[selected_button_num];
        float cutsceneWait = 0;

        Debug.Log(destination_node);

        if (destination_node == 100)
        {
            //end of game, go to credits
            scene.moveScene("credit");
        } else if (destination_node == 24 || destination_node == 27 || destination_node == 28 || destination_node == 31) {
            //second cutscene
            cutsceneWait = cutsceneHandler.playCutscene("cutscene2");          
        } else if (destination_node == 68 || destination_node == 73 || destination_node == 74 || destination_node == 75 || destination_node == 76) {
            //third cutscene
            cutsceneWait = cutsceneHandler.playCutscene("cutscene3");
        }
        return cutsceneWait;
    }
}