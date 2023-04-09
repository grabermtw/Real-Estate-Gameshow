using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OneOff : MonoBehaviour
{
    public GameObject digitPrefab;
    public Transform digitParent;
    private List<OneOffDigit> digitList;
    private HouseData currentData;
    
    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        currentData = PropertyManager.instance.GetRandomProperty();
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
        string displayText;
        if (valGuess != currentData.price)
        {
            displayText = String.Format("Better luck next time! The actual price was {0}", currentData.price.ToString("C0"));
        }
        else
        {
            displayText = String.Format("That is correct! You win 10,000!");
        }
        Popup.instance.StartPopup(displayText);
    }
    
}