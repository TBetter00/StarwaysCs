using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float score;
    [SerializeField] private float limitScore = 10;
    public static ScoreManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (score >= limitScore)
        {
            limitScore = math.pow(limitScore, 2);
            SceneManager.LoadSceneAsync(0);
        }
    }

    public void addScore(float a)
    {
        score += a;
    }

    public float getScore()
    {
        return this.score;
    }

}
