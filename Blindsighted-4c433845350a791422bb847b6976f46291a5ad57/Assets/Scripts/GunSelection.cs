using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunSelection : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tokenDisplay;
    [SerializeField] private Settings settings;
    private Transform[] gunButtons;
    [SerializeField] private int[] gunCosts;
    [SerializeField] private Weapon[] guns;

    [SerializeField] bool midRound;
    int currentGunIndex;
    [SerializeField] Transform[] buttons;
    public Sprite unlockedCard;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        for(int i = 0; i < guns.Length; i++)
        {
            if (guns[i].name.Equals(settings.weapon.name))
            {
                currentGunIndex = i;
                break;
            }
        }

        ResetUI();
    }

    public void SetGun(int index)
    {
        settings.weapon = guns[index];
        currentGunIndex = index;
        ResetUI();

        transform.GetChild(currentGunIndex).GetComponent<Animator>().SetTrigger("Selected");
        AudioManager.instance.Play("MenuGood");
    }

    public void Loop()
    {
        LevelLoader.instance.LoadLevel(0, settings.level + 2);
    }

    public void ResetUI()
    {
        for(int i = 0; i < buttons.Length; i++)
        {   
            if ((settings.unlocks[i] == 0))
            {
                buttons[i].GetChild(0).gameObject.SetActive(true);
                buttons[i].GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                transform.GetChild(i).GetComponent<Image>().overrideSprite = unlockedCard;


                buttons[i].GetChild(1).gameObject.SetActive(true);
                buttons[i].GetChild(0).gameObject.SetActive(false);

                if(i == currentGunIndex)
                {
                    buttons[i].GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "selected";
                }
                else
                {
                    buttons[i].GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "equip";
                }
            }
        }

        tokenDisplay.text = "" + settings.tokens;
    }

    public void PurchaseGun(int index)
    {
        if (settings.tokens > gunCosts[index])
        {
            settings.tokens -= gunCosts[index];
            tokenDisplay.text = "" + settings.tokens;
            settings.unlocks[index] = 1;    
            SetGun(index);
        }
        else
        {
            AudioManager.instance.Play("MenuBad");
        }
    }
}
