using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinScreen : MonoBehaviour
{
    public void GoBack() {
        SceneManager.LoadScene("MainMenu");
    }

    public void ToggleActiveStatus(bool status) {
        gameObject.SetActive(status);
    }
}
