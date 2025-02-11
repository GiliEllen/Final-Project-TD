using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinScreen : MonoBehaviour
{
    [SerializeField] private NewLevelManager levelManager;
    private Player player;
    public event Action RestartPressed = delegate { };

    private void Awake()
    {
        levelManager.GameWon += () => ToggleActiveStatus(true);
        ToggleActiveStatus(false);
    }

    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");

        if (playerObject != null)
        {
            player = playerObject.GetComponent<Player>();
        }
        else
        {
            Debug.LogError("Player GameObject not found!");
        }
    }

      public void PausePlayerAudio()
    {
        if (player != null)
        {
            player.PauseAudio();
        }
    }

    public void ResumePlayerAudio()
    {
        if (player != null)
        {
            player.ResumeAudio();
        }
    }

    public void GoBack() {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnRestartPressed()
    {
        ToggleActiveStatus(false);
        RestartPressed();
    }

    private void ToggleActiveStatus(bool status) {
        gameObject.SetActive(status);
        if (status) {
             Time.timeScale = 0f;
             PausePlayerAudio();
        } else {
            Time.timeScale = 1f;
            ResumePlayerAudio();      
        }
    }

}
