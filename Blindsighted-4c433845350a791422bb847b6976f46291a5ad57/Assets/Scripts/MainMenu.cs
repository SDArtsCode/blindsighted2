using UnityEngine;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject options;
    [SerializeField] GameObject mainMenu;

    public void StartGame()
    {
        LevelLoader.instance.LoadLevel(1);
        AudioManager.instance.Play("MenuGood");
    }

    public void Menu()
    {
        options.SetActive(false);
        mainMenu.SetActive(true);
        AudioManager.instance.Play("MenuAffirmative");

    }

    public void Options()
    {
        options.SetActive(true);
        mainMenu.SetActive(false);
        AudioManager.instance.Play("MenuAffirmative");
    }

    public void Quit()
    {
        AudioManager.instance.Play("MenuAffirmative");
        Application.Quit();
    }
}
