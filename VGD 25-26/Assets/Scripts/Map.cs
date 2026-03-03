using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    //References
    [SerializeField] private Button cityButton;
    [SerializeField] private GameObject cityLockBackground, cityLockIcon;

    [SerializeField] private Button dojoButton;
    [SerializeField] private GameObject dojoLockBackground, dojoLockIcon;

    void Awake()
    {
        Debug.Log("levels unlocked:" + PlayerPrefs.GetInt("levelsUnlocked", 0));
        switch (PlayerPrefs.GetInt("levelsUnlocked", 0))
        {
            case 0:
                break;
            case 1:
                UnlockLevel(cityButton, cityLockBackground, cityLockIcon);
                break;
            case 2:
                UnlockLevel(cityButton, cityLockBackground, cityLockIcon);
                UnlockLevel(dojoButton, dojoLockBackground, dojoLockIcon);
                break;
            default:
                break;
        }

        PlayerPrefs.SetInt("levelsUnlocked", PlayerPrefs.GetInt("levelsUnlocked")+1);
    }

    void UnlockLevel(Button b, GameObject lb, GameObject li)
    {
        b.interactable = true;
        lb.SetActive(false);
        li.SetActive(false);
    }
}
