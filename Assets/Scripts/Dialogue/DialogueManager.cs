﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    // UI elements for dialogue.
    public Text nameText;
    public Text dialogueText;

    // Animation for the dialogues popping up.
    public Animator animator;

    private Queue<string> sentences;

	void Start () {
        sentences = new Queue<string>();
	}

    /// <summary>
    /// Start the dialogue.
    /// </summary>
    /// <param name="dialogue">Sentences that the dialogue has.</param>
    public void StartDialogue(Dialogue dialogue) {
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            DisplayNextSentence();
        }
    }

    /// <summary>
    /// Display the next sentence in the dialogue. 
    /// </summary>
    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    /// <summary>
    /// Types the sentences character by character.
    /// </summary>
    /// <param name="sentence">Sentence to type.</param>
    IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return null;
        }
    }

    /// <summary>
    /// Animation which closes the dialogue UI.
    /// </summary>
    void EndDialogue() {
        animator.SetBool("IsOpen", false);
    }
	
}
