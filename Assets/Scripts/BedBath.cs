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

    private void Start()
    {
        //StartGame();
    }

    public override void StartGame()
    {
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
        string displayText;
        if (bedCount == currentData.bedrooms && bathCount == currentData.bathrooms)
        {
            displayText = String.Format("Correct number of bedrooms and bathrooms! You win 10,000!");
        }
        else
        {
            displayText = String.Format("Sorry, this house has {0} bedrooms and {1} bathrooms.\nBetter luck next time!", currentData.bedrooms, currentData.bathrooms);
        }
        Popup.instance.StartPopup(displayText);
    }
}