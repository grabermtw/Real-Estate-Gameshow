using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

/*
    TO USE THIS DIALOGUE SYSTEM:
    Simply make a list of Dialogue objects and call PlayDialogue while passing that
    list in as an argument. Each Dialogue object should have the name of the character,
    the character's message, and a reference to the camera that should be used for that line of dialogue.
*/


public class Dialogue {
    public string characterName;
    public string message;
    public CinemachineVirtualCamera camera;

    public Dialogue(string characterName, string message, CinemachineVirtualCamera camera) {
        this.characterName = characterName;
        this.message = message;
        this.camera = camera;
    }
}

public class DialogueSystem : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameField;
    public TextMeshProUGUI messageField;
    public TextMeshProUGUI clickMessage;
    private CinemachineVirtualCamera[] vcameras;

    void Awake()
    {
        vcameras = FindObjectsOfType<CinemachineVirtualCamera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Test/demo dialogue
        /*
        List<Dialogue> testDialogue = new List<Dialogue>();
        testDialogue.Add(new Dialogue("Vorsteg", "Hello there it's pretty cool", vcameras[0]));
        testDialogue.Add(new Dialogue("Vorsteg", "Still very fun look at me talk", vcameras[0]));
        testDialogue.Add(new Dialogue("Vorsteg", "Wait my name is Bestudo not Vorsteg", vcameras[1]));
        testDialogue.Add(new Dialogue("Bestudo", "That's better!", vcameras[1]));
        testDialogue.Add(new Dialogue("Bestudo", "Who are you?", vcameras[2]));
        testDialogue.Add(new Dialogue("Bestudo", "Wanna play a game?", vcameras[0]));
        PlayDialogue(testDialogue);
        */
    }

    // TODO: add voice acting support and animation support
    public void PlayDialogue(List<Dialogue> dialogues)
    {
        StartCoroutine(PlayDialogueConversation(dialogues));
    }

    private IEnumerator PlayDialogueConversation(List<Dialogue> dialogues)
    {
        dialoguePanel.SetActive(true);
        for(int i = 0; i < dialogues.Count; i++)
        {
            foreach (CinemachineVirtualCamera vcam in vcameras)
            {
                vcam.Priority = 10;
            }
            dialogues[i].camera.Priority = 11;
            clickMessage.enabled = false;
            nameField.SetText(dialogues[i].characterName);
            messageField.SetText(dialogues[i].message);
            yield return new WaitForSeconds(1);
            clickMessage.enabled = true;
            bool wait = true;
            while (wait) {
                if (Input.anyKeyDown)
                {
                    wait = false;
                }
                yield return null;
            }
        }
        dialoguePanel.SetActive(false);
    }
}
