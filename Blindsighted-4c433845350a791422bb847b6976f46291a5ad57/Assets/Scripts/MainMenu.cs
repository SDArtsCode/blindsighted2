using UnityEngine;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject options;
    [SerializeField] GameObject mainMenu;

    public void StartGame()
    {
        LevelLoader.instance.LoadLevel(0);
    }

    public void Menu()
    {
        options.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void Options()
    {
        options.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
