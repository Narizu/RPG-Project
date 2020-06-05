using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using BayatGames.SaveGameFree;

public class LoadLevel : MonoBehaviour
{
    public void OpenSceneMenu()
    {
        LoadingScreen.Instance.LoadScene("Menu");
    }

    public void OpenSceneCharacter()
    {
        LoadingScreen.Instance.LoadScene("CharacterClasses");
    }

    public void OpenSceneTest()
    {
        LoadingScreen.Instance.LoadScene("Test");
    }

    public void OpenSceneChooseYourCharacter()
    {
        LoadingScreen.Instance.LoadScene("ChooseYourCharacter");
    }

    public void OpenSceneLevel01()
    {
        LoadingScreen.Instance.LoadScene("Level01");
    }

    public void OpenSceneLevel02()
    {
        LoadingScreen.Instance.LoadScene("Level02");
    }

    public void OpenSceneLevel03()
    {
        LoadingScreen.Instance.LoadScene("Level03");
    }

    public void OpenSceneLevel04()
    {
        LoadingScreen.Instance.LoadScene("Level04");
    }

    public void OpenSceneLevel05()
    {
        LoadingScreen.Instance.LoadScene("Level05");
    }

    public void OpenSceneLevel06()
    {
        LoadingScreen.Instance.LoadScene("Level06");
    }

    public void OpenSceneLevel07()
    {
        LoadingScreen.Instance.LoadScene("Level07");
    }

    public void OpenSceneLevel08()
    {
        LoadingScreen.Instance.LoadScene("Level08");
    }

    public void OpenSceneLevel09()
    {
        LoadingScreen.Instance.LoadScene("Level09");
    }

    public void OpenSceneLevel10()
    {
        LoadingScreen.Instance.LoadScene("Level10");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;        // Salir del modo jugar en el editor.
#else
        Application.Quit();
#endif
    }

    public void Save()
    {
        SaveGame.Save("Character", PlayerStats.character);
        SaveGame.Save("Gender", PlayerStats.gender);
        SaveGame.Save("Level", PlayerExperience.level);
        SaveGame.Save("Experience", PlayerExperience.currentExp);
        SaveGame.Save("Quest", QuestManager.questsDone);
        SaveGame.Save("Scene", SceneManager.GetActiveScene().name);
    }

    public void Load()
    {
        PlayerStats.character = SaveGame.Load<string>("Character");
        PlayerStats.gender = SaveGame.Load<string>("Gender");
        PlayerExperience.level = SaveGame.Load<int>("Level");
        PlayerExperience.currentExp = SaveGame.Load<float>("Experience");
        QuestManager.questsDone = SaveGame.Load<int>("Quest");
        LoadingScreen.Instance.LoadScene(SaveGame.Load<string>("Scene"));
    }
}