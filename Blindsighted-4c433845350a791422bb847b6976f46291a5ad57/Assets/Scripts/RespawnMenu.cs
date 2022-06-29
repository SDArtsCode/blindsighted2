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
        LevelLoader.instance.LoadLevel(0, 0);
        Debug.Log("hit");
    }

    public void Respawn()
    {
        LevelLoader.instance.LoadLevel(0, -1);
        Debug.Log("hit");
    }

    public void ChangeWeapon()
    {
        Debug.Log("hit");
        LevelLoader.instance.LoadLevel(0, 7);
    }
}
