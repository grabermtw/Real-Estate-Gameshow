using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameshowManager : MonoBehaviour
{
    public DialogueSystem dialogueSystem;

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
        testDialogue.Add(new Dialogue("Bestudo", "Welcome to the show!", dialogueSystem.vcameras[2], AnimCategory.CorrectAnswer));
        testDialogue.Add(new Dialogue("Bestudo", "So you want to be a realtor?", dialogueSystem.vcameras[1], AnimCategory.CorrectAnswer));
        testDialogue.Add(new Dialogue("Bestudo", "Well, you've got a lot of learning to do!", dialogueSystem.vcameras[1], AnimCategory.WrongAnswer));
        testDialogue.Add(new Dialogue("Bestudo", "So whattdya say? Wanna show your real estate smarts?", dialogueSystem.vcameras[1], AnimCategory.CorrectAnswer));
        testDialogue.Add(new Dialogue("Bestudo", "Let's play some games!", dialogueSystem.vcameras[1], AnimCategory.CorrectAnswer));
        dialogueSystem.PlayDialogue(testDialogue, true);
    }
}
