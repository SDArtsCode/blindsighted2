using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenuConfirm;
    [SerializeField] GameObject weaponConfirm;
    [SerializeField] GameObject main;
    [SerializeField] GameObject options;
    [SerializeField] GameObject controls;

    public void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        main.SetActive(true);
    }

    public void Return()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        main.SetActive(true);
        AudioManager.instance.Play("MenuBad");
    }

    public void MainMenuConfirmation()
    {
        mainMenuConfirm.SetActive(true);
        main.SetActive(false);
        AudioManager.instance.Play("MenuAffirmative");
    }
    public void MainMenu()
    {
        Debug.Log("MainMenu");
        AudioManager.instance.Play("MenuGood");
        LevelLoader.instance.LoadLevel(1, 0);
    }
    public void WeaponSelectionConfirmation()
    {
        weaponConfirm.SetActive(true);
        main.SetActive(false);
        AudioManager.instance.Play("MenuAffirmative");
    }

    public void WeaponSelection()
    {
        LevelLoader.instance.LoadLevel(0, 6);
        AudioManager.instance.Play("MenuGood");
    }

    public void Controls()
    {
        controls.SetActive(true);
        main.SetActive(false);
        AudioManager.instance.Play("MenuAffirmative");
    }

    public void Options()
    {
        options.SetActive(true);
        main.SetActive(false);
        AudioManager.instance.Play("MenuAffirmative");
    }

    public void QuitGame()
    {
        AudioManager.instance.Play("MenuBad");
        Application.Quit();
    }

}
