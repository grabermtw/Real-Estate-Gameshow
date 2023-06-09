using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaxGuess : BaseGame
{
    public TMP_InputField guessInput;
    string[] taxGuessInstructions = new string[] {
        "The Tax Guessing game is super easy! Guess the most recent amount of yearly property tax of the house!",
        "You can win up to $50,000! The closer you are to the actual value, the more money'll get!"
    };
    AnimCategory[] taxGuessAnims = new AnimCategory[] { AnimCategory.CorrectAnswer, AnimCategory.CorrectAnswer };

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        StartGame();
    }

    public override void StartGame()
    {
        base.PlayInstructions(taxGuessInstructions, taxGuessAnims);
        screensaver.SetActive(false);
        currentData = PropertyManager.instance.GetRandomProperty(true);
        HouseUI.instance.PopulateData(currentData, true, true, false, true, false, true, false, false);
    }

    public void SubmitGuess()
    {
        int value = Int32.Parse(guessInput.text);
        int difference = Math.Abs(currentData.propertyTax - value);
        int prize = (int)((float)maxPrize - ((float)maxPrize * ((float)difference / 1000f)));
        if (prize < 0)
        {
            prize = 0;
        }
        List<Dialogue> dialogueList = new List<Dialogue>();
        string displayText;
        if (prize == 0) {
            displayText = String.Format("The actual tax is {0}.\nYour guess was way off!", currentData.propertyTax.ToString("C0"), prize.ToString("C0"));
            dialogueList.Add(new Dialogue("Bestudo", displayText, base.dialogueSystem.bestudoCam, AnimCategory.WrongAnswer));
            AudioManager.instance.PlaySadSound();
        }
        else {
            displayText = String.Format("The actual tax is {0}.\nYou won {1}!", currentData.propertyTax.ToString("C0"), prize.ToString("C0"));
            dialogueList.Add(new Dialogue("Bestudo", displayText, base.dialogueSystem.bestudoCam, AnimCategory.CorrectAnswer));
            AudioManager.instance.PlayHappySound();
        }
        base.dialogueSystem.PlayDialogue(dialogueList, false, base.gameshowManager.StartNextGame, false);
        //Popup.instance.StartPopup(displayText);
        GameshowManager.instance.AddMoney(prize);
    }

    
}