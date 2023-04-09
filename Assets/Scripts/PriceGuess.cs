using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PriceGuess : BaseGame
{
    public TMP_InputField guessInput;
    string[] priceGuessInstructions = new string[] {
        "The Price Guessing game is very simple! Guess the price of the house to the best of your ability!",
        "You can win up to $100,000! The closer you are to the actual value, the more money'll get!"
    };
    AnimCategory[] priceGuessAnims = new AnimCategory[] { AnimCategory.CorrectAnswer, AnimCategory.CorrectAnswer };

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        //StartGame();
    }

    public override void StartGame()
    {
        base.PlayInstructions(priceGuessInstructions, priceGuessAnims);
        base.StartGame();
        HouseUI.instance.PopulateData(currentData, true, true, true, true, true, false, false, false);
    }

    public void ConvertValueToMoney()
    {
        int caretAdjust = 0;
        if (guessInput.text.Length == 1)
        {
            caretAdjust += 1;
        }
        if (Int32.TryParse(guessInput.text, out int value))
        {
            guessInput.text = value.ToString("C0");
        }
        guessInput.caretPosition += caretAdjust + 1;
    }

    public void SubmitGuess()
    {
        int value = Int32.Parse(guessInput.text);
        int difference = Math.Abs(currentData.price - value);
        int prize = maxPrize - difference;
        if (prize < 0)
        {
            prize = 0;
        }
        List<Dialogue> dialogueList = new List<Dialogue>();
        string displayText;
        if (value == 0)
        {
            displayText = String.Format("Really? You think they're just giving these things away?");
            dialogueList.Add(new Dialogue("Bestudo", displayText, base.dialogueSystem.bestudoCam, AnimCategory.WrongAnswer));
        }
        else if (prize == 0) {
            displayText = String.Format("The actual listing is valued at {0}.\nYour guess was too far off!", currentData.price.ToString("C0"));
            dialogueList.Add(new Dialogue("Bestudo", displayText, base.dialogueSystem.bestudoCam, AnimCategory.WrongAnswer));
        }
        else {
            displayText = String.Format("The actual listing is valued at {0}.\nYou won {1}!", currentData.price.ToString("C0"), prize.ToString("C0"));
            dialogueList.Add(new Dialogue("Bestudo", displayText, base.dialogueSystem.bestudoCam, AnimCategory.CorrectAnswer));
        }
        base.dialogueSystem.PlayDialogue(dialogueList, false, base.gameshowManager.StartNextGame, false);
        //Popup.instance.StartPopup(displayText);
        GameshowManager.instance.AddMoney(prize);
    }

    
}