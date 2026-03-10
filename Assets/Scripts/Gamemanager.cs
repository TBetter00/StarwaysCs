using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public float point = 0;
    private bool isGameOver;
    public float timeRunner;
    private float startTimer;
    [Header("Settings")]
    [SerializeField] private float difficultyBase = 1.1f; // How fast it scales (e.g., 1.05 to 1.2)
    [SerializeField] private float initialDecreaseRate = 1f;
    [SerializeField] private float exponentialDrop;
    [Header("UI")]
    [SerializeField] private Text maxTime;
    [SerializeField] private Text currentTime;
    [SerializeField] private Text score;

    void Update()
    {
        RefreshTime();
        if (isGameOver) return;
        timeRunner += Time.deltaTime;
        if (startTimer <= 10f)
        {
            startTimer += Time.deltaTime;
        }
        else
        {
            // Subtract exponentially based on time passed
            // Mathf.Pow(base, power) grows faster and faster
            exponentialDrop = initialDecreaseRate * Mathf.Pow(difficultyBase, timeRunner - 10f);
            point -= exponentialDrop * Time.deltaTime;
        }
        if (point < 0)
        {
            isGameOver = true;
            EndGame();
        }
    }
    void EndGame()
    {
        // // 1. Get the previous record (returns 0 if it's the first time playing)
        // float personalRecord = PlayerPrefs.GetFloat("BestTime", 0);

        // // 2. Check if this is a new record (faster time is smaller)
        // // We check if record is 0 for the very first game ever played
        // if (timeRunner > personalRecord || personalRecord == 0)
        // {
        //     Debug.Log("New Record! Saving: " + timeRunner);
        //     PlayerPrefs.SetFloat("BestTime", timeRunner);
        //     PlayerPrefs.Save(); // Forces Unity to write it to the disk immediately
        // }
        // else
        // {
        //     Debug.Log("You were slower. Record remains: " + personalRecord);
        // }

        QuitLogic();
    }

    void QuitLogic()
    {
        // Scene 0 is the same scene as the first scene that player will play so it will run over and over again and again
        SceneManager.LoadScene(0);
    }
    void RefreshTime()
    {
        maxTime.text = PlayerPrefs.GetFloat("BestTime", 0).ToString();
        currentTime.text = timeRunner.ToString("F0");
        score.text = point.ToString("F0");
    }
}
