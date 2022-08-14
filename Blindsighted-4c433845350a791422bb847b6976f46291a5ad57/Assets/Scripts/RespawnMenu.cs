using UnityEngine;

public class RespawnMenu : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            MainMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Respawn();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            ChangeWeapon();
        }
    }

    public void MainMenu()
    {
        AudioManager.instance.Stop("GameOver");
        LevelLoader.instance.LoadLevel(0, 0);
    }

    public void Respawn()
    {
        AudioManager.instance.Stop("GameOver");
        LevelLoader.instance.LoadLevel(0, -1);
    }

    public void ChangeWeapon()
    {
        AudioManager.instance.Stop("GameOver");
        LevelLoader.instance.LoadLevel(0, 6);
    }
}
