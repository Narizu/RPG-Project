using UnityEngine;

public class TitleAnimator : MonoBehaviour
{
    public RectTransform rpgImage;
    public RectTransform projectImage;
    public GameObject controlsPanel;
    public GameObject contactPanel;

    float acceleration;
    bool first;

    void Start()
    {
        first = true;
    }

    void Update()
    {
        if (first)
            acceleration += Time.deltaTime * 0.1f;

        if (projectImage.position.y > 800)
        {
            first = false;
            acceleration -= Time.deltaTime * 0.1f;
        }
        else if (projectImage.position.y < 760)
        {
            first = false;
            acceleration += Time.deltaTime * 0.1f;
        }
        
        projectImage.position += new Vector3(0f, acceleration, 0f);
        rpgImage.Rotate(0f, 0f, acceleration * 0.1f);
    }

    public void SetControlsPanel()
    {
        controlsPanel.SetActive(!controlsPanel.activeSelf);
    }

    public void SetContactPanel()
    {
        contactPanel.SetActive(!contactPanel.activeSelf);
    }
}