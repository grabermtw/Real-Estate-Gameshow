using UnityEngine;

[System.Serializable]
public class BaseGame : MonoBehaviour
{
    public GameObject screensaver;
    protected HouseData currentData;
    public int maxPrize = 100000;

    public virtual void StartGame()
    {
        screensaver.SetActive(false);
        currentData = PropertyManager.instance.GetRandomProperty();
    }

    public virtual void EndGame()
    {

    }
}