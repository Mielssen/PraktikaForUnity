using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelMenuManager : MonoBehaviour
{
    [System.Serializable]
    public class LevelButtonInfo
    {
        public Button button; 
        public GameObject lockIcon;
        public int levelNumber; 
        public string sceneName; 
    }

    [Header("Level Buttons Setup")]

    [SerializeField] private LevelButtonInfo[] levelButtons;

    [Header("Locked Visuals")]

    [SerializeField] private Color lockedColor = new Color(0.3f, 0.3f, 0.3f, 0.9f);

    void Start()
    {

        int reachedLevel = PlayerPrefs.GetInt("ReachedLevel", 1);

        foreach (var info in levelButtons)
        {
            SetupButton(info, reachedLevel);
        }
    }

    void SetupButton(LevelButtonInfo info, int reachedLevel)
    {
        if (info.button == null) return;

        bool isUnlocked = info.levelNumber <= reachedLevel;

        info.button.interactable = isUnlocked;

        if (isUnlocked)
        {

            if (info.lockIcon != null) info.lockIcon.SetActive(false);
            info.button.image.color = Color.white;

            var text = info.button.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null) text.alpha = 1f;
        }
        else
        {

            if (info.lockIcon != null) info.lockIcon.SetActive(true);
            info.button.image.color = lockedColor;

            var text = info.button.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null) text.alpha = 0.3f;
        }
    }
    public void OpenLevelByInfo(int buttonIndex)
    {

        if (buttonIndex >= 0 && buttonIndex < levelButtons.Length)
        {
            string sceneToLoad = levelButtons[buttonIndex].sceneName;

            if (levelButtons[buttonIndex].button.interactable)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}