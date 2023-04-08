using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PriceGuess : MonoBehaviour
{
    public TextMeshProUGUI AddressText, CityStateText, BedBathText, SqFeetText, YearText;
    public Image houseImage;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        PopulateData(PropertyManager.instance.GetRandomProperty());
    }

    public void PopulateData(HouseData data)
    {
        houseImage.sprite = Sprite.Create(data.image, new Rect(0, 0, data.image.width, data.image.height),
                                          new Vector2(data.image.width / 2, data.image.height / 2));

        AddressText.text = data.address;
        CityStateText.text = string.Format("{0}, {1}", data.city, data.state);
        BedBathText.text = string.Format("{0} bed, {1} bath", data.bedrooms, data.bathrooms);
        SqFeetText.text = string.Format("{0} sq feet", data.squareFeet);
        YearText.text = string.Format("Built in {0}", data.yearBuilt);
    }
}