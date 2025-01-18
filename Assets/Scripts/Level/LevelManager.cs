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

    public void isGameOver() {}
    public void isGameWin() {
        Debug.Log ("game WIN!!!!!!!");
    }

    private void Start()
    {
        nightmares.Sort((a, b) => a.timeToInitialize.CompareTo(b.timeToInitialize));
    }

    public int GetNightmareCount()
    {
        return nightmares.Count;
    }

    public void SendNightMare() 
    {
        if (currentIndex < nightmares.Count && elapsedTime >= nightmares[currentIndex].timeToInitialize)
        {
            nightmares[currentIndex].gameObject.SetActive(true);
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
}
