using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YearGuess : BaseGame
{
    public GameObject buttonPrefab;
    public Transform buttonParent;
    string[] yearGuessInstructions = new string[] {
        "Are you good with history? Because it's time to play \"Guess that Decade!\"",
        "It's very simple: guess the decade that the house was built based on the information given!",
        "You can win up to $100,000! That's a lot of money!",
        "But be warned: if you guess incorrectly, you won't even earn a single penny!"
    };
    AnimCategory[] yearGuessAnims = new AnimCategory[] { AnimCategory.CorrectAnswer, AnimCategory.CorrectAnswer, AnimCategory.CorrectAnswer, AnimCategory.WrongAnswer };

    private void Start()
    {
        //StartGame();
    }

    public override void StartGame()
    {
        base.PlayInstructions(yearGuessInstructions, yearGuessAnims);
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
        List<Dialogue> dialogueList = new List<Dialogue>();
        int correctDecade = (currentData.yearBuilt / 10) * 10;
        if (decade == correctDecade)
        {
            displayText = String.Format("Correct! You win {0}", maxPrize);
            dialogueList.Add(new Dialogue("Bestudo", displayText, base.dialogueSystem.bestudoCam, AnimCategory.CorrectAnswer));
        }
        else
        {
            displayText = String.Format("Sorry, this house was built in the {0}s. Better luck next time!", correctDecade);
            dialogueList.Add(new Dialogue("Bestudo", displayText, base.dialogueSystem.bestudoCam, AnimCategory.WrongAnswer));
        }
        base.dialogueSystem.PlayDialogue(dialogueList, false, base.gameshowManager.StartNextGame, false);
        //Popup.instance.StartPopup(displayText);
    }
}