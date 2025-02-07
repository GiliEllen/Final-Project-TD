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
        levelManager.GameWon += () => ToggleActiveStatus(true);
        ToggleActiveStatus(false);
    }

    public void OnContinuePressed()
    {
        ToggleActiveStatus(false);
        ContinuePressed();
    }

    private void ToggleActiveStatus(bool status) {
        gameObject.SetActive(status);
    }

}
