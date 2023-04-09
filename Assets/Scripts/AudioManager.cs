using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource audioSource;
    public AudioClip happyClip, sadClip;

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
            Debug.Log("Only 1 AUdioManager can be loaded at once");
            return;
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHappySound()
    {
        audioSource.PlayOneShot(happyClip);
    }

    public void PlaySadSound()
    {
        audioSource.PlayOneShot(sadClip);
    }
}