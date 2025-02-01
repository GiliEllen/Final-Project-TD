using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevelManager : MonoBehaviour
{
    private int _numberOfMonsters;
    [SerializeField] private int levelIndex;
    private const string BaseLevelSceneName = "Level";
    private const int VictorySceneIndex = 7;
    private const int GameOverSceneIndex = 8;
    [SerializeField] private float delayBetweenLevels;
    private bool _wasEndSceneLoaded;

    public event Action LevelCompleted = delegate { };

    private void Start()
    {
        Nightmare.NightmareDestroyed += OnMonsterDied;
        LoadLevel(levelIndex, 0);
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

    private void OnMonsterDied(float scareLevelToDecrease)
    {
        _numberOfMonsters--;
        CalculateNumberOfMonstersInLevel();
        if (_numberOfMonsters <= 0)
        {
            LevelCompleted();
            int nextLevelIndex = levelIndex + 1;
            AsyncOperation unloadLevelOperation = UnloadCurrentLevel();
            unloadLevelOperation.completed += (_) => LoadLevel(nextLevelIndex, delayBetweenLevels);
        }
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
        else loadLevelOperation.completed += (_) => CalculateNumberOfMonstersInLevel();
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
        _numberOfMonsters = FindObjectsByType<Nightmare>(FindObjectsSortMode.None).Length;
    }

    public void MoveToActiveScene(GameObject objectToMove)
    {
        SceneManager.MoveGameObjectToScene(objectToMove, SceneManager.GetActiveScene());
    }

    public void MoveToLevelScene(GameObject objectToMove)
    {
        SceneManager.MoveGameObjectToScene(objectToMove, SceneManager.GetSceneByName(GetLevelSceneName(levelIndex)));
    }
}
