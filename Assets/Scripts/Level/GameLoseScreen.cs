using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoseScreen : MonoBehaviour
{
    public void GoBack() {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void ToggleActiveStatus(bool status) {
        gameObject.SetActive(status);
    }
}
