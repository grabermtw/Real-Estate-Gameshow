using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameField;
    public TextMeshProUGUI messageField;
    public TextMeshProUGUI clickMessage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // TODO: add voice acting support
    public void PlayDialogue(string[] characterNames, string[] dialogues)
    {
        StartCoroutine(PlayDialogueConversation(characterNames, dialogues));
    }

    private IEnumerator PlayDialogueConversation(string[] characterNames, string[] dialogues)
    {
        dialoguePanel.SetActive(true);
        for(int i = 0; i < characterNames.Length; i++)
        {
            clickMessage.enabled = false;
            nameField.SetText(characterNames[i]);
            messageField.SetText(dialogues[i]);
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
