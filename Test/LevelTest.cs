using UnityEngine;

public class LevelTest : MonoBehaviour
{
    public int newLevel;

    void Start()
    {
        PlayerExperience.level = newLevel;
    }
}