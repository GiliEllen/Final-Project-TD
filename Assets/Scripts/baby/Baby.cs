using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Baby : MonoBehaviour
{
    [SerializeField] private float _currentScareLevel;
    [SerializeField] private NewLevelManager levelManager;
    public TMP_Text scareText;
    [SerializeField] private float maxScareLevel;
    public event Action BabyScared = delegate { };
    [SerializeField] private bool _shouldCheckScare = true;

    private void Start()
    {
        levelManager.LevelLost += OnPlayerLost;
        levelManager.LevelStarted += () => _shouldCheckScare = true;
    }

    private void OnPlayerLost()
    {
        _currentScareLevel = 0f;
    }

    public void AdjustScare(float amount) 
    {
        if (!_shouldCheckScare) return;

        _currentScareLevel += amount;
        if (_currentScareLevel < 0)
            _currentScareLevel = 0;
        else CheckScareLevel();
        scareText.text = " " + _currentScareLevel + " ";
    }

    private void CheckScareLevel() {
        if (_currentScareLevel >= maxScareLevel) {
            BabyScared();
            _shouldCheckScare = false;
        }
    }
}
