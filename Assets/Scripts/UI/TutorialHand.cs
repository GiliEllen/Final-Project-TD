using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialHand : MonoBehaviour
{
    [SerializeField] private Transform endTransfrom;
    private Vector3 _handLastLocation;
    private PlacementSystem _placementSystem;
    private Tween _movementTween;

    private void Start()
    {
        _handLastLocation = endTransfrom.position;
        Destroy(endTransfrom.gameObject);
        _placementSystem = FindAnyObjectByType<PlacementSystem>();
        _placementSystem.DraggingToy += RemoveHand;
        TutorialHandMovement();
    }

    private void TutorialHandMovement()
    {
        _movementTween = transform.DOMove(_handLastLocation, 2f, false)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Restart);
    }

    private void RemoveHand()
    {
        _placementSystem.DraggingToy -= RemoveHand;
        _movementTween.Kill();
        Destroy(gameObject);
    }

}
