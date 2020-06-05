using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeleportManager : MonoBehaviour
{
    public static int maxSceneLevel;

    public GameObject teleportPanel;
    public Button[] levelButtons;
    public QuestManager questManager;

    string[] sceneName;
    int sceneLevel;
    GameObject npcQuest;
    GameObject[] items;

    void Start()
    {
        npcQuest = GameObject.FindGameObjectWithTag("Quest");
        items = GameObject.FindGameObjectsWithTag("Item");

        sceneName = SceneManager.GetActiveScene().name.Split('l');

        if (SceneManager.GetActiveScene().name.Contains("Dungeon"))
        {
            sceneName = sceneName[1].Split('D');
            sceneLevel = int.Parse(sceneName[0]);
        }
        else if (SceneManager.GetActiveScene().name.Contains("Tutorial"))
        {
            sceneName = sceneName[1].Split('T');
            sceneLevel = int.Parse(sceneName[0]);
        }
        else
            sceneLevel = int.Parse(sceneName[1]);

        if (QuestManager.questsDone >= sceneLevel && SceneManager.GetActiveScene().name != "Level10Dungeon")
        {
            Destroy(npcQuest);

            foreach (GameObject item in items)
                Destroy(item);
        }

        foreach (Button button in levelButtons)
            button.interactable = false;

        if (maxSceneLevel < sceneLevel)
            maxSceneLevel = sceneLevel;
        
        for (int i = 0; i < maxSceneLevel; i++)
            levelButtons[i].interactable = true;

        questManager.desactivateQuest();
    }

    void Update()
    {
        if (QuestManager.questsDone >= 10)
            return;

        int i = QuestManager.questsDone;

        while (i >= 0)
        {
            levelButtons[i].interactable = true;
            i--;
        }
    }

    public void CloseTeleportPanel()
    {
        teleportPanel.SetActive(!teleportPanel.activeSelf);
    }
}