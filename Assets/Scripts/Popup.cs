using System.Collections;
using TMPro;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public static Popup instance;

    public TextMeshProUGUI popupText, popupAnyKeyText;
    public GameObject popupUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            popupUI.SetActive(false);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void StartPopup(string text)
    {
        popupText.text = text;
        StartCoroutine(PopupCoroutine());
    }

    public IEnumerator PopupCoroutine()
    {
        popupUI.SetActive(true);
        popupAnyKeyText.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        popupAnyKeyText.gameObject.SetActive(true);
        yield return new WaitUntil( () => Input.anyKey );
        popupUI.SetActive(false);
    }
}