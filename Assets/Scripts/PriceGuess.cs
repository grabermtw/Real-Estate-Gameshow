using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PriceGuess : MonoBehaviour
{
    public const int maxPrize = 100000;

    public TextMeshProUGUI AddressText, CityStateText, BedBathText, SqFeetText, YearText;
    public Image houseImage;
    public TMP_InputField guessInput;

    public GameObject popup;
    public TextMeshProUGUI popupWinText, popupAnyKeyText;

    private HouseData currentData;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        popup.SetActive(false);
        PopulateData(PropertyManager.instance.GetRandomProperty());
    }

    public void PopulateData(HouseData data)
    {
        currentData = data;
        houseImage.sprite = Sprite.Create(data.image, new Rect(0, 0, data.image.width, data.image.height),
                                          new Vector2(data.image.width / 2, data.image.height / 2));

        AddressText.text = data.address;
        CityStateText.text = string.Format("{0}, {1}", data.city, data.state);
        BedBathText.text = string.Format("{0} bed, {1} bath", data.bedrooms, data.bathrooms);
        SqFeetText.text = string.Format("{0} sq feet", data.squareFeet);
        YearText.text = string.Format("Built in {0}", data.yearBuilt);
    }

    public void ConvertValueToMoney()
    {
        int caretAdjust = 0;
        if (guessInput.text == "")
        {
            caretAdjust = 1;
        }
        if (Int32.TryParse(guessInput.text, out int value))
        {
            guessInput.text = value.ToString("C0");
        }
        if (guessInput.text == "$")
        {
            guessInput.text = "";
            caretAdjust = -1;
        }
        guessInput.caretPosition += caretAdjust;
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
        popupWinText.text = String.Format("The actual listing is valued at {0}.\nYou won {1}!", currentData.price.ToString("C0"), prize.ToString("C0"));
        StartCoroutine(PopupCoroutine());
    }

    public IEnumerator PopupCoroutine()
    {
        popup.SetActive(true);
        popupAnyKeyText.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        popupAnyKeyText.gameObject.SetActive(true);
        yield return new WaitUntil( () => Input.anyKey );
        popup.SetActive(false);
        PopulateData(PropertyManager.instance.GetRandomProperty());
    }
}