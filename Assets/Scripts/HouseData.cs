using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct HouseData
{
    public int price;
    public int bedrooms;
    public int bathrooms;
    public int squareFeet;
    public int propertyTax;
    public int taxAssessment;
    public string city;
    public string state;
    public string address;
    public int yearBuilt;
    public Image image;

    public HouseData(int price, int bedrooms, int bathrooms, int squareFeet, int propertyTax,
                     int taxAssessment, string city, string state, string address, int yearBuilt, Image image)
    {
        this.price = price;
        this.bedrooms = bedrooms;
        this.bathrooms = bathrooms;
        this.squareFeet = squareFeet;
        this.propertyTax = propertyTax;
        this.taxAssessment = taxAssessment;
        this.city = city;
        this.state = state;
        this.address = address;
        this.yearBuilt = yearBuilt;
        this.image = image;
    }

    public override string ToString()
    {
        return price + " " + bedrooms + " " + bathrooms + " " + squareFeet + " " + propertyTax + " " +
               taxAssessment + " " + city + " " + state + " " + address + " " + yearBuilt;
    }
}
