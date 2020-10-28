using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText; 
    private Queue<string> sentences;
    public bool finished = false;
    public bool possible = false; 



    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartDialogue(dialogue1 dialogue)
    {
        finished = false; 
        sentences.Clear();
        print(dialogue.name);
        nameText.text = dialogue.name; 
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence); 
        }
        possible = true; 
        DisplayNextSentence(); 
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return; 
        }
        if (possible == true)
        {
            string sentence = sentences.Dequeue();
            StartCoroutine(TypeSentence(sentence));
        }

    }

    IEnumerator TypeSentence (string sentence)
    {
        possible = false; 
        dialogueText.text = ""; 
        foreach (char letter in sentence.ToCharArray())
        {

            dialogueText.text += letter;
            yield return null; 
        }
        possible = true; 
    }


    void EndDialogue()
    {
        finished = true; 

    }
}
