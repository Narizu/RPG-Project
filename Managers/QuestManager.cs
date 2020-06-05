using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    public static int questsDone = 0;

    public GameObject questPanel;                               // Referencia a la quest.
    
    bool firstTime;                                             // Cuando es la primera vez.
    string[] sceneName;
    int sceneLevel;

    void Start()
    {
        firstTime = true;                                       // Fijar la primera vez a verdadero.
    }

    void Update()
    {
        selectQuest();
    }

    public void showQuest()
    {
        gameObject.SetActive(true);                             // Activar la quest.
    }

    public void hideQuest()
    {
        gameObject.SetActive(false);                            // Desactivar la quest.
        
        if (firstTime)                                          // Si es la primera vez...
            activateQuest();                                    // ...activar la quest.

        if (SceneManager.GetActiveScene().name.Contains("Tutorial"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            LoadingScreen.tutPlaPos = player.transform.position;
            LoadingScreen.tutorial = true;
            SceneManager.LoadScene("Level01");
        }

        else if (SceneManager.GetActiveScene().name == "Level10Dungeon")
        {
            GameObject narizuNPC = GameObject.FindGameObjectWithTag("Quest");
            GameObject narizuEnemy = GameObject.FindGameObjectWithTag("EnemyBoss");
            narizuEnemy.GetComponent<NavMeshAgent>().enabled = false;
            narizuEnemy.transform.position = narizuNPC.transform.position;
            Destroy(narizuNPC);
            narizuEnemy.GetComponent<NavMeshAgent>().enabled = true;

        }
    }

    public void activateQuest()
    {
        questPanel.SetActive(true);                             // Activar la quest.
        firstTime = false;                                      // Fijar la primera vez a falso.
    }

    public void selectQuest()
    {
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

        switch (sceneLevel)
        {
            case 1:
                Quest01();
                break;
            case 2:
                Quest02();
                break;
            case 3:
                Quest03();
                break;
            case 4:
                Quest04();
                break;
            case 5:
                Quest05();
                break;
            case 6:
                Quest06();
                break;
            case 7:
                Quest07();
                break;
            case 8:
                Quest08();
                break;
            case 9:
                Quest09();
                break;
            case 10:
                Quest10();
                break;
        }
    }

    public void desactivateQuest()
    {
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

        if (questsDone != sceneLevel - 1)
        {
            questPanel.SetActive(false);
            return;
        }
    }

    void Quest01()
    {
        ScoreManager.questText = "Enemy";
        ScoreManager.questNumber = 1;
        
        if (ScoreManager.score >= ScoreManager.questNumber)
            questsDone = 1;
    }

    void Quest02()
    {
        ScoreManager.questText = "Level";
        ScoreManager.questNumber = 3;

        if (PlayerExperience.level >= ScoreManager.questNumber)
            questsDone = 2;
    }

    void Quest03()
    {
        ScoreManager.questText = "Item";
        ScoreManager.questNumber = 3;

        if (ScoreManager.items >= ScoreManager.questNumber)
            questsDone = 3;
    }

    void Quest04()
    {
        ScoreManager.questText = "Enemy";
        ScoreManager.questNumber = 4;

        if (ScoreManager.score >= ScoreManager.questNumber)
            questsDone = 4;
    }

    void Quest05()
    {
        ScoreManager.questText = "Item";
        ScoreManager.questNumber = 1;

        if (ScoreManager.items >= ScoreManager.questNumber)
            questsDone = 5;
    }

    void Quest06()
    {
        ScoreManager.questText = "Enemy";
        ScoreManager.questNumber = 6;

        if (ScoreManager.score >= ScoreManager.questNumber)
            questsDone = 6;
    }

    void Quest07()
    {
        ScoreManager.questText = "Item";
        ScoreManager.questNumber = 4;

        if (ScoreManager.items >= ScoreManager.questNumber)
            questsDone = 7;
    }

    void Quest08()
    {
        ScoreManager.questText = "Enemy";
        ScoreManager.questNumber = 8;

        if (ScoreManager.score >= ScoreManager.questNumber)
            questsDone = 8;
    }

    void Quest09()
    {
        ScoreManager.questText = "Item";
        ScoreManager.questNumber = 5;

        if (ScoreManager.items >= ScoreManager.questNumber)
            questsDone = 9;
    }

    void Quest10()
    {
        ScoreManager.questText = "Enemy";
        ScoreManager.questNumber = 1;

        if (ScoreManager.score >= ScoreManager.questNumber)
            questsDone = 10;
    }
}