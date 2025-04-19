using UnityEngine;

public class Audio : MonoBehaviour
{
    
    void Start()
    {
        AudioSource audioSource=GetComponent<AudioSource>();
        audioSource.Play();
    }
}
