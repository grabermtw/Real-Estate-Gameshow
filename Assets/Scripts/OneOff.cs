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
    string[] oneOffInstructions = new string[] {
        "This is one of my favorites, called \"One Off!\"",
        "Every digit in the price will be one integer away from its actual value, either up or down. It's up to you to figure out what direction!",
        "Press the buttons above and below each digit to create your guess!",
        "Be careful though, you have to guess the number PERFECTLY to win any money!"
    };

    AnimCategory[] oneOffAnims = new AnimCategory[] {
        AnimCategory.CorrectAnswer,
        AnimCategory.CorrectAnswer,
        AnimCategory.CorrectAnswer,
        AnimCategory.WrongAnswer
    };
    
    private void Start()
    {
      
    }

    public override void StartGame()
    {
        base.PlayInstructions(oneOffInstructions, oneOffAnims);
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
            dialogueList.Add(new Dialogue("Bestudo", displayText, base.dialogueSystem.bestudoCam, AnimCategory.WrongAnswer));
        }
        else
        {
            displayText = String.Format("That is correct! You win 10,000!");
            dialogueList.Add(new Dialogue("Bestudo", displayText, base.dialogueSystem.bestudoCam, AnimCategory.CorrectAnswer));
        }
        base.dialogueSystem.PlayDialogue(dialogueList, false, base.gameshowManager.StartNextGame, false);
        //Popup.instance.StartPopup(displayText);
    }
    
}