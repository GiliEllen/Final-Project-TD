using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoseScreen : MonoBehaviour
{
    [SerializeField] private Baby baby;
    public event Action RestartGame = delegate { };

    private void Awake()
    {
        baby.BabyScared += () => ToggleActiveStatus(true);
        ToggleActiveStatus(false);
    }
    public void GoBack() {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReloadScene()
    {
        ToggleActiveStatus(false);
        RestartGame();
    }

    private void ToggleActiveStatus(bool status) {
        gameObject.SetActive(status);
    }
}
