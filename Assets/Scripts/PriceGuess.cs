using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PriceGuess : BaseGame
{
    public TMP_InputField guessInput;    

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
        Popup.instance.StartPopup(String.Format("The actual listing is valued at {0}.\nYou won {1}!", currentData.price.ToString("C0"), prize.ToString("C0")));
    }

    
}