using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinScreen : MonoBehaviour
{
    [SerializeField] private NewLevelManager levelManager;
    public event Action ContinuePressed = delegate { };

    private void Awake()
    {
        levelManager.LevelCompleted += () => ToggleActiveStatus(true);
        ToggleActiveStatus(false);
    }

    public void OnContinuePressed()
    {
        ToggleActiveStatus(false);
        ContinuePressed();
    }

    public void GoBack() {
        SceneManager.LoadScene("MainMenu");
    }

    public void ToggleActiveStatus(bool status) {
        gameObject.SetActive(status);
    }

}
