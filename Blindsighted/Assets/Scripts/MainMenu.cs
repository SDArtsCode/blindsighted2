using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject options;
    [SerializeField] GameObject mainMenu;

    public void StartGame()
    {

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
