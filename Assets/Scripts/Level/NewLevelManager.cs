using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NewLevelManager : MonoBehaviour
{
     private CancellationTokenSource pauseTokenSource = new CancellationTokenSource();
    [SerializeField] private int _numberOfMonsters;
    [SerializeField] private int levelIndex;
    private const string BaseLevelSceneName = "Level";
    [SerializeField] private float delayBetweenLevels;
    [SerializeField] private GameWinScreen gameWinScreen;
    [SerializeField] private GameLoseScreen gameLoseScreen;
    [SerializeField] private Baby baby;

    public event Action LevelStarted = delegate { };
    public event Action LevelCompleted = delegate { };
    public event Action LevelLost = delegate { };
    public event Action GameWon = delegate { };

        // ClearAllCooldownTexts();
    private void Start()
    {
        Nightmare.NightmareDestroyed += OnMonsterDied;
        baby.BabyScared += OnLost;
        gameLoseScreen.RestartGame += RestartGame;
        gameWinScreen.RestartPressed += RestartGame;
        LoadLevel(levelIndex, 0);
    }

    private void OnDestroy()
    {
        Nightmare.NightmareDestroyed -= OnMonsterDied;
    }

    private void RestartGame() => LoadNextLevel(2);

    public AsyncOperation UnloadCurrentLevel()
    {
        AsyncOperation unloadLevelOperation = SceneManager.UnloadSceneAsync(GetLevelSceneName(levelIndex));
        return unloadLevelOperation;
    }

    private void OnLost()
    {
        LevelLost();
    }

    private void OnMonsterDied(float scareLevelToDecrease)
    {
        _numberOfMonsters--;
        if (_numberOfMonsters <= 0)
        {
            LevelCompleted();
            int nextLevelIndex = levelIndex + 1;
            if (IsThereNextLevel(nextLevelIndex))
                LoadNextLevel(nextLevelIndex);
            else GameWon();
        }
    }

    private void LoadNextLevel(int nextLevelIndex)
    {
        AsyncOperation unloadLevelOperation = UnloadCurrentLevel();
        levelIndex = nextLevelIndex;
        unloadLevelOperation.completed += (_) => LoadLevel(levelIndex, delayBetweenLevels);
    }


 private async void LoadLevel(int nextLevelIndex, float delay)
    {
        if (delay > 0)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(delay), pauseTokenSource.Token);
            }
            catch (TaskCanceledException)
            {
                return; 
            }
        }

        levelIndex = nextLevelIndex;
        AsyncOperation loadLevelOperation =
            SceneManager.LoadSceneAsync(GetLevelSceneName(levelIndex), LoadSceneMode.Additive);
        loadLevelOperation.completed += OnLevelLoaded;
    }

    private void OnLevelLoaded(AsyncOperation op)
    {
        LevelStarted();
        CalculateNumberOfMonstersInLevel();
    }

    private string GetLevelSceneName(int levelIndex)
    {
        return BaseLevelSceneName + levelIndex;
    }

    private void CalculateNumberOfMonstersInLevel()
    {
        _numberOfMonsters = FindObjectsByType<Nightmare>(FindObjectsInactive.Include, FindObjectsSortMode.None).Length;
    }

    public void MoveToActiveScene(GameObject objectToMove)
    {
        SceneManager.MoveGameObjectToScene(objectToMove, SceneManager.GetActiveScene());
    }

    public void MoveToLevelScene(GameObject objectToMove)
    {
        SceneManager.MoveGameObjectToScene(objectToMove, SceneManager.GetSceneByName(GetLevelSceneName(levelIndex)));
    }


    private void UpdateScareLevel()
    {
        baby.AdjustScare(1 * _numberOfMonsters);
    }

    private bool IsThereNextLevel(int nextLevelIndex)
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(nextLevelIndex + 1);
        int loadedSceneBuildIndex = SceneUtility.GetBuildIndexByScenePath(scenePath);
        return loadedSceneBuildIndex > 0;
    }

     // Pause and Resume Handling
    public void PauseGame()
    {
        pauseTokenSource.Cancel();
    }

    public void ResumeGame()
    {
        pauseTokenSource = new CancellationTokenSource(); 
    }

}
