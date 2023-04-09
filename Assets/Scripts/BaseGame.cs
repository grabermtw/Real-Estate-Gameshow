using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class BaseGame : MonoBehaviour
{
    public GameObject screensaver;
    protected HouseData currentData;
    public int maxPrize = 100000;
    protected DialogueSystem dialogueSystem;
    protected GameshowManager gameshowManager;

    public virtual void Awake()
    {
        dialogueSystem = FindObjectsOfType<DialogueSystem>()[0];
        gameshowManager = FindObjectsOfType<GameshowManager>()[0];
    }

    public virtual void StartGame()
    {
        screensaver.SetActive(false);
        currentData = PropertyManager.instance.GetRandomProperty();
    }

    public virtual void EndGame()
    {

    }

    public void PlayInstructions(string[] instructions, AnimCategory[] anims, bool endOnBedBathCam = false)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        for (int i = 0; i < instructions.Length; i++)
        {
            dialogueList.Add(new Dialogue("Bestudo", instructions[i], dialogueSystem.bestudoCam, anims[i]));
        }
        dialogueSystem.PlayDialogue(dialogueList, true, () => {}, endOnBedBathCam);
    }
}