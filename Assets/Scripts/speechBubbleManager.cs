using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class speechBubbleManager : MonoBehaviour
{

    //gameobject stuff to turn bubble + text sprites on and off
    public GameObject miniBubble, bigBubble, sentence, dots;/*, playerBubble, playerSpeech*/

    //dialogue states
    /*bool playerSpoke0 = true;
    bool playerSpoke1 = false;
    bool NPCSpoke1 = false;
    bool playerSpoke2 = false;
    bool NPCSpoke2 = false;*/

    //visibility checkers
    bool speechVisible = false;
    bool touched = false;
    bool canContinue = false;

    //these are all the text + sentence variables
    public TextMeshProUGUI textDisplay, playerTextDisplay;
    public float typingSpeed;
    public string[] sentences;
    public string[] playerSentences;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        //starts typing the sentences
        StartCoroutine(Type()); 

        //makes all the speech bubbles + text invisible
        //playerBubble.SetActive(false);
        //playerSpeech.SetActive(false);
        bigBubble.SetActive(false);
        sentence.SetActive(false);
        miniBubble.SetActive(false);
        dots.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && touched == true){ //if x is pressed and is touching the npc
           
            Debug.Log("show player bubble");

            //show the big bubble
            bigBubble.SetActive(true);
            sentence.SetActive(true);

            //show big player bubble
            //playerBubble.SetActive(true);
            //playerSpeech.SetActive(true);

            //hide the small bubble
            miniBubble.SetActive(false);
            dots.SetActive(false);
            speechVisible = true;
        }

        if (Input.GetKeyDown(KeyCode.X) && speechVisible == true){ //if x is pressed again
            //NextPlayerSentence(); //start typing next sentence
            NextSentence();
        }
        
        if(textDisplay.text == sentences[4]){
            //hide the big bubble too
            bigBubble.SetActive(false);
            sentence.SetActive(false);
        }

        if(textDisplay.text == sentences[index]){
            canContinue = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other){ 
        if(other.gameObject.name == "showBubbleCollider"){ //if player touches npc collider
            
            Debug.Log("touched NPC");

            //show the mini bubble
            miniBubble.SetActive(true);
            dots.SetActive(true);
            touched = true;
        }
    }

    void OnTriggerExit2D(Collider2D other){ 
        if(other.gameObject.name == "showBubbleCollider"){ //if player is no longer touching the npc collider
            
            Debug.Log("not touching NPC anymore");

            //hide the mini bubble
            miniBubble.SetActive(false);
            dots.SetActive(false);
            touched = false;

            //hide the big bubble too
            bigBubble.SetActive(false);
            sentence.SetActive(false);
        }
    }

    //THIS IS ALL NPC TALKING STUFF
    IEnumerator Type(){
        foreach(char letter in sentences[index].ToCharArray()){ //gets each letter in sentence
            textDisplay.text += letter; //adds each letter to text
            yield return new WaitForSeconds(typingSpeed); //sets speed of scroll
        }
    }

    public void NextSentence(){
        if(index < sentences.Length - 1){ 
            index++; //get next sentence
            textDisplay.text = ""; //set text back to nothing
            StartCoroutine(Type()); //start typing new sentences
        } else {
            textDisplay.text = ""; //otherwise just keep text empty
        }
    }
}
