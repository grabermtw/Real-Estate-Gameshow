using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameshowManager : MonoBehaviour
{
    private DialogueSystem dialogueSystem;

    void Awake()
    {
        dialogueSystem = GetComponent<DialogueSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Opening();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Opening()
    {
        List<Dialogue> testDialogue = new List<Dialogue>();
        testDialogue.Add(new Dialogue("Bestudo", "Welcome to the show!", dialogueSystem.wholeStageCam, AnimCategory.CorrectAnswer));
        testDialogue.Add(new Dialogue("Bestudo", "So you want to be a realtor?", dialogueSystem.bestudoCam, AnimCategory.CorrectAnswer));
        testDialogue.Add(new Dialogue("Bestudo", "Well, you've got a lot of learning to do!", dialogueSystem.bestudoCam, AnimCategory.WrongAnswer));
        testDialogue.Add(new Dialogue("Bestudo", "So whattdya say? Wanna show your real estate smarts?", dialogueSystem.bestudoCam, AnimCategory.CorrectAnswer));
        testDialogue.Add(new Dialogue("Bestudo", "Let's play some games!", dialogueSystem.bestudoCam, AnimCategory.CorrectAnswer));
        dialogueSystem.PlayDialogue(testDialogue, true);
    }
}
