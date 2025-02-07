using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class Baby : MonoBehaviour
{
    [SerializeField] private float _currentScareLevel;
    [SerializeField] private NewLevelManager levelManager;
    public TMP_Text scareText;
    [SerializeField] private float maxScareLevel;
    public event Action BabyScared = delegate { };
    [SerializeField] private bool _shouldCheckScare = true;
    [SerializeField] private Slider scareLevelSlider;
    [SerializeField] private float smoothSpeed = 5f;
    private float targetSliderValue;

    private void Start()
    {
        levelManager.LevelLost += OnPlayerLost;
        levelManager.LevelStarted += () => _shouldCheckScare = true;
        targetSliderValue = _currentScareLevel / maxScareLevel;
        Nightmare.IncreaseScareLevel += AdjustScare;
    }

    private void OnPlayerLost()
    {
        _currentScareLevel = 0f;
    }

    private void Update()
    {
        if (scareLevelSlider != null)
        {
            targetSliderValue = Mathf.Clamp(_currentScareLevel / maxScareLevel, 0, 1);
            scareLevelSlider.value = Mathf.Lerp(scareLevelSlider.value, targetSliderValue, 
                smoothSpeed * Time.deltaTime);
            scareText.text = " " + _currentScareLevel + " / " + maxScareLevel; 
        }
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
