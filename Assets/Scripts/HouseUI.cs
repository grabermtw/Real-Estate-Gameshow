using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HouseUI : MonoBehaviour
{
    public static HouseUI instance;

    public TextMeshProUGUI addressText;
    public TextMeshProUGUI cityStateText;
    public TextMeshProUGUI bedBathText; 
    public TextMeshProUGUI sqFeetText;
    public TextMeshProUGUI yearText;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI propertyTaxText;
    public TextMeshProUGUI taxAssessmentText;
    public Image houseImage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            Debug.Log("Only 1 PropertyManager can be loaded at once");
        }
    }

    public void PopulateData(HouseData data, bool useAddress, bool useCityState, bool useBedBath,
                             bool useSqFeet, bool useYear, bool usePrice, bool usePropertyTax, bool useTaxAssessment)
    {
        houseImage.sprite = Sprite.Create(data.image, new Rect(0, 0, data.image.width, data.image.height),
                                          new Vector2(data.image.width / 2, data.image.height / 2));

        addressText.text = data.address;
        cityStateText.text = string.Format("{0}, {1}", data.city, data.state);
        bedBathText.text = string.Format("{0} bed, {1} bath", data.bedrooms, data.bathrooms);
        sqFeetText.text = string.Format("{0} sq feet", data.squareFeet);
        yearText.text = string.Format("Built in {0}", data.yearBuilt);
        priceText.text = string.Format("Listed at {0}", data.price.ToString("C0"));
        propertyTaxText.text = string.Format("Property tax {0}", data.propertyTax.ToString("C0"));
        taxAssessmentText.text = string.Format("Tax assessment {0}", data.taxAssessment.ToString("C0"));

        addressText.gameObject.SetActive(useAddress);
        cityStateText.gameObject.SetActive(useCityState);
        bedBathText.gameObject.SetActive(useBedBath);
        sqFeetText.gameObject.SetActive(useSqFeet);
        yearText.gameObject.SetActive(useYear);
        priceText.gameObject.SetActive(usePrice);
        propertyTaxText.gameObject.SetActive(usePropertyTax);
        taxAssessmentText.gameObject.SetActive(useTaxAssessment);
    }
}