using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string[] stages;
    public int stageIndex;

    public int potionCount;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this) 
            Destroy(this);
    }


    public void RestartStage()
    {
        SceneManager.LoadScene(stages[stageIndex]);
    }


    public void NextStage()
    {
        stageIndex++;
        if (stageIndex >= stages.Length) stageIndex = 0;

        SceneManager.LoadScene(stages[stageIndex]);
    }
}