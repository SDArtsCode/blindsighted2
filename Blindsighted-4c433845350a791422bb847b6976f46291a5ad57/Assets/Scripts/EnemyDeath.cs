using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip[] clips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = clips[Random.Range(0, clips.Length)];
        audioSource.Play();
    }

    private void LateUpdate()
    {
        if(audioSource.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }
}
