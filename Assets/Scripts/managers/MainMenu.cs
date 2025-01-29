using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject shopScreen;
    public GameObject achievementsScreen;
    public GameObject galleryScreen;
    public LevelSelectionMenuManager allLevelsScreen;
    public Player player;
    
    public void ActivateScreen(GameObject screen) {
        screen.SetActive(true);
        gameObject.SetActive(false);
    }

    public void GoBack(GameObject screen) {
        screen.SetActive(false);
        gameObject.SetActive(true);
    }

    public void GoBackLevelManager() {
        allLevelsScreen.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
    public void ActivateLevelManager() {
        allLevelsScreen.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Play() {
        // player.UnlockedLevel ;
        SceneManager.LoadScene("Gili-test");
    }
}
