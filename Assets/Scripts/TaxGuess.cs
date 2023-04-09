using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaxGuess : BaseGame
{
    public TMP_InputField guessInput;    

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
        base.StartGame();
        HouseUI.instance.PopulateData(currentData, true, true, false, true, false, true, false, false);
    }

    public void SubmitGuess()
    {
        int value = Int32.Parse(guessInput.text);
        int difference = Math.Abs(currentData.propertyTax - value);
        Debug.Log("Diff "+difference);
        int prize = (int)((float)maxPrize - ((float)maxPrize * ((float)difference / 1000f)));
        if (prize < 0)
        {
            prize = 0;
        }
        Popup.instance.StartPopup(String.Format("The actual tax is {0}.\nYou won {1}!", currentData.propertyTax.ToString("C0"), prize.ToString("C0")));
    }

    
}