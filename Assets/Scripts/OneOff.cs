using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OneOff : BaseGame
{
    public GameObject digitPrefab;
    public Transform digitParent;
    private List<OneOffDigit> digitList;
    private DialogueSystem dialogueSystem;
    private GameshowManager gameshowManager;
    
    private void Start()
    {
        dialogueSystem = FindObjectsOfType<DialogueSystem>()[0];
        gameshowManager = FindObjectsOfType<GameshowManager>()[0];
    }

    public override void StartGame()
    {
        base.StartGame();
        HouseUI.instance.PopulateData(currentData, true, true, true, true, true, false, false, false);

        digitList = new List<OneOffDigit>();

        // break actual price out into digits and either add or subtract 1
        foreach (char c in currentData.price.ToString())
        {
            int val = c - '0';

            OneOffDigit newDigit = Instantiate(digitPrefab, digitParent).GetComponent<OneOffDigit>();

            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                newDigit.initialValue = (val + 1) % 10;
            }
            else
            {
                newDigit.initialValue = (val + 9) % 10;
            }
            newDigit.currentValue = newDigit.initialValue;
            newDigit.text.text = newDigit.initialValue.ToString();
            digitList.Add(newDigit);
        }
    }

    public void SubmitGuess()
    {
        string finalGuess = "";
        foreach (OneOffDigit digit in digitList)
        {
            finalGuess += digit.currentValue;
        }
        int valGuess = Int32.Parse(finalGuess);
        List<Dialogue> dialogueList = new List<Dialogue>();
        string displayText;
        if (valGuess != currentData.price)
        {
            displayText = String.Format("Better luck next time! The actual price was {0}", currentData.price.ToString("C0"));
            dialogueList.Add(new Dialogue("Bestudo", displayText, dialogueSystem.bestudoCam, AnimCategory.WrongAnswer));
        }
        else
        {
            displayText = String.Format("That is correct! You win 10,000!");
            dialogueList.Add(new Dialogue("Bestudo", displayText, dialogueSystem.bestudoCam, AnimCategory.CorrectAnswer));
        }
        dialogueSystem.PlayDialogue(dialogueList, false, gameshowManager.StartNextGame, false);
        //Popup.instance.StartPopup(displayText);
    }
    
}