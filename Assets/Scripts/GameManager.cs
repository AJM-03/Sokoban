using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string[] mainStages;
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
            Destroy(gameObject);
    }


    public void RestartStage()
    {
        SceneManager.LoadScene(mainStages[stageIndex]);
    }


    public void NextStage()
    {
        stageIndex = GridManager.Instance.exitDestinationStage;
        if (stageIndex >= mainStages.Length) stageIndex = 0;

        SceneManager.LoadScene(mainStages[stageIndex]);
    }

    public void SecretStage(string scene)
    {
        int i = 0;
        foreach (string stage in mainStages)
        {
            if (stage == scene) stageIndex = i;
            i++;
        }

        SceneManager.LoadScene(scene);
    }
}


// TODO

// Ice tile where things keep sliding in one direction
// Maze of spikes and inverted spikes, a button has to be pushed but it changes the layout
// Rusted blocks on each edge show location of secret exit
// Multiple button support
// UI Scaling