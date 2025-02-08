using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    [SerializeField] private TMP_Text tutorialText;
    [SerializeField] private Sprite darkOverlay;
    [SerializeField] private float textSpeed = 1000;
    private GameObject scareLevel;
    private bool tutorialSkipped = false;

    private readonly string[] tutorialLines =
    {
        "The baby’s nightmares are coming to life!",
        "Use the magical toys to stop the nightmares before they reach the baby.",
        "Don’t let the scare level fill up. Too many nightmares will wake the baby!",
        "Drag and place toys wisely to stop the enemies before it’s too late."
    };

    async void Start()
    {

    }


   

}
