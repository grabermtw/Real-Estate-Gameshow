using TMPro;
using UnityEngine;

public class OneOffDigit : MonoBehaviour
{
    public int initialValue;
    public int currentValue;
    public TextMeshProUGUI text;
    
    public void IncreaseDigit()
    {
        currentValue = (initialValue + 1) % 10;
        text.text = currentValue.ToString();   
    }

    public void DecreaseDigit()
    {
        currentValue = (initialValue + 9) % 10;
        text.text = currentValue.ToString();   
    }
}