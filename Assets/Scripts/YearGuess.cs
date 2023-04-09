using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YearGuess : BaseGame
{
    public GameObject buttonPrefab;
    public Transform buttonParent;

    private void Start()
    {
        //StartGame();
    }

    public override void StartGame()
    {
        base.StartGame();
        HouseUI.instance.PopulateData(currentData, true, true, false, true, false, true, false, false);
        
        // correct button
        int correctIdx = UnityEngine.Random.Range(0, 4);

        // create buttons
        for (int i = 0; i < 4; i++)
        {
            int decade = (currentData.yearBuilt / 10) * 10;
            if (i != correctIdx)
            {
                while (decade == (currentData.yearBuilt / 10) * 10)
                {
                    decade = UnityEngine.Random.Range(190, 203) * 10;
                }
            }
            Button button = Instantiate(buttonPrefab, buttonParent).GetComponent<Button>();
            button.onClick.AddListener(() => {SubmitGuess(decade);});
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = decade.ToString() + "s";
        }
    }

    public void SubmitGuess(int decade)
    {
        string displayText = decade.ToString();

        int correctDecade = (currentData.yearBuilt / 10) * 10;
        if (decade == correctDecade)
        {
            displayText = String.Format("Correct! You win {0}", maxPrize);
        }
        else
        {
            displayText = String.Format("Sorry, this house was built in the {0}s. Better luck next time!", correctDecade);
        }

        Popup.instance.StartPopup(displayText);
    }
}