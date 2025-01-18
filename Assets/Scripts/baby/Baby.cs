using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Baby : MonoBehaviour
{
    public float scareLevel = 0;
    public LevelManager levelManager;
    public TMP_Text scareText;

    public void AdjustScare(float amount) {
        scareLevel += amount;
        CheckScareLevel();
        scareText.text = " " + scareLevel + " ";
    }

    private void CheckScareLevel() {
        if (scareLevel >= 100) {
            levelManager.isGameOver();
        }
    }
}
