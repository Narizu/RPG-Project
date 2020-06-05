using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Text moveText;
    public Text attackText;

    void Start()
    {
        moveText.enabled = true;
        attackText.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (gameObject.name == "Move")
                moveText.enabled = false;

            if (gameObject.name == "Attack")
                attackText.enabled = true;
        }
    }
}