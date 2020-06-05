using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalManager : MonoBehaviour
{
    public GameObject finalPanel;
    public Text code;

    bool first;

    void Start()
    {
        first = true;
        code.text = "" + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level10Dungeon")
            if (!GameObject.FindGameObjectWithTag("EnemyBoss"))
                if (first)
                    Close();
    }

    public void Close()
    {
        finalPanel.SetActive(!finalPanel.activeSelf);
        first = false;
    }
}