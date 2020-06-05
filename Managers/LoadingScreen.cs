using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance { get; private set; }

    public static bool tutorial = false;
    public static Vector3 tutPlaPos;

    public Image loadingImage;
    public Text loadingText;

    float speed;
    float timer;
    float timeBetweenTextAnimations;

    void Awake()
    {
        speed = 0.01f;
        timeBetweenTextAnimations = 0.1f;
        DefineSingleton();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeBetweenTextAnimations)
            TextAnimation();

        if (tutorial && SceneManager.GetActiveScene().name == "Level01")
        {
            tutorial = false;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = tutPlaPos;
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            camera.transform.position = tutPlaPos;
        }
    }

    void DefineSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            loadingImage.gameObject.SetActive(false);
            loadingText.gameObject.SetActive(false);
        }
        else
            Destroy(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(ShowLoadingScreen(sceneName));
    }

    IEnumerator ShowLoadingScreen(string sceneName)
    {
        loadingImage.gameObject.SetActive(true);
        loadingText.gameObject.SetActive(true);
        Color ci = loadingImage.color;
        Color ct = loadingText.color;
        ci.a = 0f;
        ct.a = 0f;

        while (ci.a < 1)
        {
            loadingImage.color = ci;
            loadingText.color = ct;
            ci.a += speed;
            ct.a += speed;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);

        while (!sceneName.Equals(SceneManager.GetActiveScene().name))
            yield return null;

        while (ci.a > 0)
        {
            loadingImage.color = ci;
            loadingText.color = ct;
            ci.a -= speed;
            ct.a -= speed;
            yield return null;
        }

        loadingImage.gameObject.SetActive(false);
        loadingText.gameObject.SetActive(false);
    }

    void TextAnimation()
    {
        timer = 0f;

        if (loadingText.text == "Loading...")
            loadingText.text = "Loading";

        else if (loadingText.text == "Loading")
            loadingText.text = "Loading.";

        else if (loadingText.text == "Loading.")
            loadingText.text = "Loading..";

        else if (loadingText.text == "Loading..")
            loadingText.text = "Loading...";
    }
}