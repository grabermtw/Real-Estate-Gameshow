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

public enum AnimCategory {
    Idle = 4,
    CorrectAnswer = 8,
    WrongAnswer = 5
}

public class Dialogue {
    public string characterName;
    public string message;
    public CinemachineVirtualCamera camera;
    public AnimCategory animCategory;

    public Dialogue(string characterName, string message, CinemachineVirtualCamera camera, AnimCategory animCategory) {
        this.characterName = characterName;
        this.message = message;
        this.camera = camera;
        this.animCategory = animCategory;
    }
}

public class DialogueSystem : MonoBehaviour
{
    public Animator bestudoAnim;
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameField;
    public TextMeshProUGUI messageField;
    public TextMeshProUGUI clickMessage;
    public CinemachineVirtualCamera wholeStageCam;
    public CinemachineVirtualCamera bestudoCam;
    public CinemachineVirtualCamera gameplayCam;
    public CinemachineVirtualCamera bedbathCam;

    // Start is called before the first frame update
    void Start()
    {

    }

    // TODO: add voice acting
    public void PlayDialogue(List<Dialogue> dialogues, bool gestureOnCompletion, bool endOnBedBathCam = false)
    {
        StartCoroutine(PlayDialogueConversation(dialogues, gestureOnCompletion, endOnBedBathCam));
    }

    private IEnumerator PlayDialogueConversation(List<Dialogue> dialogues, bool gestureOnCompletion, bool endOnBedBathCam)
    {
        dialoguePanel.SetActive(true);
        for(int i = 0; i < dialogues.Count; i++)
        {
            CameraChanger(dialogues[i].camera);

            // Handle Animation
            bestudoAnim.SetInteger("RandomChoice", Random.Range(0,(int) dialogues[i].animCategory));
            switch(dialogues[i].animCategory)
            {
                case AnimCategory.Idle:
                {
                    bestudoAnim.SetBool("WrongAnswer", false);
                    bestudoAnim.SetBool("CorrectAnswer", false);
                    break;
                }
                case AnimCategory.CorrectAnswer:
                {
                    bestudoAnim.SetBool("WrongAnswer", false);
                    bestudoAnim.SetBool("CorrectAnswer", true);
                    break;
                }
                case AnimCategory.WrongAnswer:
                {
                    bestudoAnim.SetBool("WrongAnswer", true);
                    bestudoAnim.SetBool("CorrectAnswer", false);
                    break;
                }
                default: break;
            }

            // Handle Dialogue
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
        if (gestureOnCompletion) {
            bestudoAnim.SetTrigger("Gesture");
            CameraChanger(gameplayCam);
        }
        if (endOnBedBathCam) {
            CameraChanger(bedbathCam);
        }
    }

    private void CameraChanger(CinemachineVirtualCamera cam)
    {
        // Handle cameras
        wholeStageCam.Priority = 10;
        bestudoCam.Priority = 10;
        gameplayCam.Priority = 10;
        bedbathCam.Priority = 10;
        
        cam.Priority = 11;
    }
}
