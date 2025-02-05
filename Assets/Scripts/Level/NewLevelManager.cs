using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevelManager : MonoBehaviour
{
    [SerializeField] private int _numberOfMonsters;
    [SerializeField] private int levelIndex;
    private const string BaseLevelSceneName = "Level";
    private const int VictorySceneIndex = 7;
    private const int GameOverSceneIndex = 8;
    [SerializeField] private float delayBetweenLevels;
    private bool _wasEndSceneLoaded;
    [SerializeField] private GameWinScreen gameWinScreen;
    [SerializeField] private GameLoseScreen gameLoseScreen;
    [SerializeField] private Baby baby;

    public event Action LevelStarted = delegate { };
    public event Action LevelCompleted = delegate { };
    public event Action LevelLost = delegate { };

    private void Start()
    {
        gameWinScreen.ContinuePressed += () => LoadNextLevel(levelIndex + 1);
        Nightmare.NightmareDestroyed += OnMonsterDied;
        baby.BabyScared += OnLost;
        gameLoseScreen.RestartGame += () => LoadNextLevel(1);
        LoadLevel(levelIndex, 0);
        InvokeRepeating("UpdateScareLevel", 0f, 1f);
    }

    private void OnDestroy()
    {
        Nightmare.NightmareDestroyed -= OnMonsterDied;
    }

    private AsyncOperation UnloadCurrentLevel()
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
            await Task.Delay(TimeSpan.FromSeconds(delay));

        bool isNextSceneEndScene = IsNextSceneEndScene(nextLevelIndex);
        if (isNextSceneEndScene && _wasEndSceneLoaded)
            return;
        levelIndex = nextLevelIndex;
        AsyncOperation loadLevelOperation =
            SceneManager.LoadSceneAsync(GetLevelSceneName(levelIndex), LoadSceneMode.Additive);
        if (isNextSceneEndScene)
        {
            _wasEndSceneLoaded = true;
            //loadLevelOperation.completed += (_) => OnEndSceneLoaded();
        }
        else loadLevelOperation.completed += OnLevelLoaded;
    }

    private void OnLevelLoaded(AsyncOperation op)
    {
        LevelStarted();
        CalculateNumberOfMonstersInLevel();
    }

    private bool IsNextSceneEndScene(int nextLevelIndex)
    {
        return nextLevelIndex == GameOverSceneIndex || nextLevelIndex == VictorySceneIndex;
    }

    /*private void OnEndSceneLoaded()
    {
        RetryButton retryButton = FindFirstObjectByType<RetryButton>();
        retryButton.GameRestarted += () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }*/

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
}
