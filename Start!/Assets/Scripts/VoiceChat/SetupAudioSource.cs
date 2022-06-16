using UnityEngine;

public class SetupAudioSource : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = AudioClip.Create("test", 8000, 1, 8000, false);
        audioSource.Play();
    }
}