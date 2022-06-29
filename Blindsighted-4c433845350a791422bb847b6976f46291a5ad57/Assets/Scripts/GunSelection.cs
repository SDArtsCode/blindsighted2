using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelection : MonoBehaviour
{
    [SerializeField] private Settings settings;
    private Transform[] gunButtons;
    [SerializeField] bool midRound;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gunButtons = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            gunButtons[i] = transform.GetChild(i);

            if (midRound)
            {
                gunButtons[i].gameObject.SetActive(i - 1 < settings.loop);
            }
            else
            {
                gunButtons[i].gameObject.SetActive(i - 1 <= settings.loop);
            }
            
        }
    }

    public void SetGun(Weapon weapon)
    {
        settings.weapon = weapon;
        AudioManager.instance.Play("MenuBad");
    }

    public void Loop()
    {
        if (midRound)
        {
            LevelLoader.instance.LoadLevel(0, settings.lastSceneIndex);
        }
        else
        {
            settings.loop++;
            LevelLoader.instance.LoadLevel(0, 1);
        }        
    }
}
