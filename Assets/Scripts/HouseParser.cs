using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseParser : MonoBehaviour
{
    public string pathToCSV = "";
    public string pathToImages = "";
    public const int numFieldsInRow = 11;

    public void DebugParse()
    {
        foreach (HouseData hd in ParseCSV())
        {
            Debug.Log(hd.ToString());
        }
    }

    public List<HouseData> ParseCSV()
    {
        
        List<HouseData> exportList = new List<HouseData>();
        try
        {
            Debug.Log(Application.persistentDataPath);
            string fullCSVPath = Path.Combine(Application.streamingAssetsPath, pathToCSV);
            using (StreamReader sr = new StreamReader(fullCSVPath))
            {
                // skip header line
                string line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    // parse line
                    string[] toks = line.Split(',');
                    if (toks.Length == numFieldsInRow)
                    {
                        int price = Int32.Parse(toks[0]);
                        int bedrooms = Int32.Parse(toks[2]);
                        int bathrooms = Int32.Parse(toks[2]);
                        int squareFeet = Int32.Parse(toks[3]);
                        int propertyTax = Int32.Parse(toks[4]);
                        int taxAssessment = Int32.Parse(toks[5]);
                        string city = toks[6];
                        string state = toks[7];
                        string address = toks[8];
                        int yearBuilt = Int32.Parse(toks[9]);
                        //Debug.Log(Path.Combine(pathToImages, toks[10].Replace(".png", "")));
                        string pathToImage = Path.Combine(Application.streamingAssetsPath, "Images", toks[10]);
                        byte[] pngBytes = System.IO.File.ReadAllBytes(pathToImage);
                        Texture2D image = new Texture2D(2, 2);
                        Debug.Log(pathToImage);
                        if (!image.LoadImage(pngBytes))
                        {
                            Debug.Log("Failed to load bytes");
                        }
                        else
                        {
                            Debug.Log("LOADED IT!!!");
                        }
                        if (image == null)
                        {
                            Debug.Log("Failed to load image " + Path.Combine(pathToImages, toks[10].Replace(".png", "")));
                        }
                        
                        HouseData newData = new HouseData(price, bedrooms, bathrooms, squareFeet, propertyTax,
                                                          taxAssessment, city, state, address, yearBuilt, image);
                        exportList.Add(newData);
                    }
                    else
                    {
                        Debug.Log(String.Format("wrong number of tokens- got {0} expected {1}\nfor line {2}", toks.Length, numFieldsInRow, line));
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.StackTrace);
        }
        return exportList;
    }
}