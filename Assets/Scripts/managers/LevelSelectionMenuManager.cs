using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionMenuManager : MonoBehaviour
{
    public static int currLevel;
    public void OnClickBack() {
        gameObject.SetActive(false);
    }

    public void OnClickLevel(int levelNum) {
        currLevel = levelNum;
        SceneManager.LoadScene("Gili-test");
    }
 
}
