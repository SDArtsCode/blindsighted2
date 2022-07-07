using UnityEngine.SceneManagement;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider))]
public class LevelExit : MonoBehaviour
{
    [SerializeField] Settings settings;
    public static Action levelCompleted;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            settings.level = SceneManager.GetActiveScene().buildIndex - 2;
            levelCompleted();
            LevelLoader.instance.LoadLevel(1, 8);
        }
    }
}
