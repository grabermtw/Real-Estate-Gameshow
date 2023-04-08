using System.Collections.Generic;
using UnityEngine;

public class PropertyManager : MonoBehaviour
{
    public static PropertyManager instance;
    
    private List<HouseData> properties;
    private HouseParser parser;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
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
            return;
        }

        parser = GetComponent<HouseParser>();
        properties = parser.ParseCSV();
    }

    public HouseData GetRandomProperty()
    {
        int idx = Random.Range(0, properties.Count);
        return properties[idx];
    }
}