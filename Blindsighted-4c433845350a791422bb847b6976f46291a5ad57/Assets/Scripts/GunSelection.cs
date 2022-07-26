using UnityEngine;
using UnityEngine.UI;

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

            Debug.Log(i);
            if (settings.unlocks[i] == 0)
            {
                gunButtons[i].GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 1);
                gunButtons[i].GetComponent<Button>().interactable = false;
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
