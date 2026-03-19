using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
}