using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public int levelNum;
    public List<Nightmare> nightmares = new List<Nightmare>();
    private float elapsedTime = 0f; 
    private int currentIndex = 0;
    private int defeatedNightmareCount = 0;
    public Baby baby;
    public float totalPointsToWin;
    public GameWinScreen gameWinScreen;
    public GameLoseScreen gameLoseScreen;

    public void isGameOver() {
        //TODO: popup logic
         Debug.Log ("game OVER!!!!!!!");
         gameLoseScreen.ToggleActiveStatus(true);
         Time.timeScale = 0;  
    }
    public void isGameWin() {
        //TODO: popup logic
        Debug.Log ("game WIN!!!!!!!");
        gameWinScreen.ToggleActiveStatus(true);
        Time.timeScale = 0;  
    }

    private void Start()
    {
        nightmares.Sort((a, b) => a.timeToInitialize.CompareTo(b.timeToInitialize));
        InvokeRepeating("UpdateScareLevel", 0f, 1f);
    }

    public void SendNightMare() 
    {
        if (currentIndex < nightmares.Count && elapsedTime >= nightmares[currentIndex].timeToInitialize)
        {
            nightmares[currentIndex].gameObject.SetActive(true);
            baby.AdjustScare(5);
            currentIndex++;
        }
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        SendNightMare();
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        foreach (var nightmare in nightmares)
        {
            if (nightmare.hp > 0)
            {
                return; 
            }
        }

        isGameWin();
    }
    private void UpdateScareLevel()
    {
        foreach (var nightmare in nightmares)
        {
            if (nightmare.hp > 0)
            {
                baby.AdjustScare(1);
            }
        }

    }
}
