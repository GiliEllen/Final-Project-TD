using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoseScreen : MonoBehaviour
{
    [SerializeField] private Baby baby;
    private Player player;
    public event Action RestartGame = delegate { };

    private void Awake()
    {
        baby.BabyScared += () => ToggleActiveStatus(true);
        ToggleActiveStatus(false);
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();

        if (player == null)
        {
            Debug.LogError("Player component not found in the scene!");
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

    public void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReloadScene()
    {
        ToggleActiveStatus(false);
        RestartGame();
    }

    private void ToggleActiveStatus(bool status)
    {
        gameObject.SetActive(status);

        if (status)
        {
            Time.timeScale = 0f;
            PausePlayerAudio();
            if (player != null) player.ToggleIsGamePaused(true);
        }
        else
        {
            Time.timeScale = 1f;
            ResumePlayerAudio();
            if (player != null) player.ToggleIsGamePaused(false);
        }
    }
}
