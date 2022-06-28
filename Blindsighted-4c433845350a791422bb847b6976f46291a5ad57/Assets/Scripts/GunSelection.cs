using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelection : MonoBehaviour
{
    [SerializeField] private Settings settings;
    private Transform[] gunButtons;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gunButtons = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            gunButtons[i] = transform.GetChild(i);
            gunButtons[i].gameObject.SetActive(i - 1 <= settings.loop);
        }
    }

    public void SetGun(Weapon weapon)
    {
        settings.weapon = weapon;
    }

    public void Loop()
    {
        Debug.Log("asdasd");
        settings.loop++;
        LevelLoader.instance.LoadLevel(0, 1);
    }
}
