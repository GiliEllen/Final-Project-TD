using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinScreen : MonoBehaviour
{
    [SerializeField] private NewLevelManager levelManager;
    public event Action RestartPressed = delegate { };

    private void Awake()
    {
        levelManager.GameWon += () => ToggleActiveStatus(true);
        ToggleActiveStatus(false);
    }

    public void OnRestartPressed()
    {
        ToggleActiveStatus(false);
        RestartPressed();
    }

    private void ToggleActiveStatus(bool status) 
    {
        gameObject.SetActive(status);
    }

}
