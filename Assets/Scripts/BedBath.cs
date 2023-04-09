using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BedBath : BaseGame
{
    private int bedCount, bathCount;
    public GameObject bedPrefab, bathPrefab;
    public Transform bedSpawnPoint, bathSpawnPoint;
    public TextMeshProUGUI bedBathText;
    private List<GameObject> spawnedObjects;

    string[] bedBathInstructions = new string[] {
        "This is a fun game I like to call \"Bed Bath and Beyond!\"",
        "To play, simply click the buttons to select how many bedrooms and bathrooms are in the house!",
        "But beware, while you can increment the numbers of bedrooms and bathrooms, you cannot decrement them, so click carefully!",
        "Oh, and watch out for falling furniture. You signed a waiver, you can't sue us!"
    };

    AnimCategory[] bedBathAnims = new AnimCategory[] {
        AnimCategory.CorrectAnswer,
        AnimCategory.CorrectAnswer,
        AnimCategory.WrongAnswer,
        AnimCategory.Idle
    };

    private void Start()
    {
        //StartGame();
    }

    public override void StartGame()
    {
        base.PlayInstructions(bedBathInstructions, bedBathAnims, true);
        base.StartGame();
        bedCount = 0;
        bathCount = 0;
        HouseUI.instance.PopulateData(currentData, true, true, false, false, true, true, false, false);

        spawnedObjects = new List<GameObject>();
    }

    public void AddBed()
    {
        Quaternion randQuat = Quaternion.Euler(UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f));
        GameObject newBed = Instantiate(bedPrefab, bedSpawnPoint.position, randQuat, bedSpawnPoint);
        spawnedObjects.Add(newBed);
        bedCount++;
        bedBathText.text = String.Format("{0} bed, {1} bath", bedCount, bathCount);
    }

    public void AddBath()
    {
        Quaternion randQuat = Quaternion.Euler(UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f));
        GameObject newBath = Instantiate(bathPrefab, bathSpawnPoint.position, randQuat, bathSpawnPoint);
        spawnedObjects.Add(newBath);
        bathCount++;
        bedBathText.text = String.Format("{0} bed, {1} bath", bedCount, bathCount);
    }

    public void SubmitGuess()
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        string displayText;
        if (bedCount == 0 && bathCount == 0)
        {
            displayText = String.Format("0 beds and 0 baths?\nAre you joking?");
            dialogueList.Add(new Dialogue("Bestudo", displayText, base.dialogueSystem.bestudoCam, AnimCategory.WrongAnswer));
            AudioManager.instance.PlaySadSound();
        }
        if (bedCount == currentData.bedrooms && bathCount == currentData.bathrooms)
        {
            displayText = String.Format("Correct number of bedrooms and bathrooms! You win {0}!", maxPrize.ToString("C0"));
            dialogueList.Add(new Dialogue("Bestudo", displayText, base.dialogueSystem.bestudoCam, AnimCategory.CorrectAnswer));
            GameshowManager.instance.AddMoney(maxPrize);
            AudioManager.instance.PlayHappySound();
        }
        else
        {
            displayText = String.Format("Sorry, this house has {0} bedrooms and {1} bathrooms.\nBetter luck next time!", currentData.bedrooms, currentData.bathrooms);
            dialogueList.Add(new Dialogue("Bestudo", displayText, base.dialogueSystem.bestudoCam, AnimCategory.WrongAnswer));
            AudioManager.instance.PlaySadSound();
        }
        base.dialogueSystem.PlayDialogue(dialogueList, false, base.gameshowManager.StartNextGame, false);
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            Destroy(spawnedObjects[i]);
        }
        //Popup.instance.StartPopup(displayText);
        
    }
}