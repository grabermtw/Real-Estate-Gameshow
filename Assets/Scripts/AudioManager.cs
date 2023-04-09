using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource audioSource;
    public AudioClip happyClip, sadClip, applauseClip;

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

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        PlayApplauseSound();
    }

    public void PlayHappySound()
    {
        audioSource.PlayOneShot(happyClip);
    }

    public void PlaySadSound()
    {
        audioSource.PlayOneShot(sadClip);
    }

    public void PlayApplauseSound()
    {
        audioSource.clip = applauseClip;
        audioSource.PlayDelayed(0.125f);
    }
}