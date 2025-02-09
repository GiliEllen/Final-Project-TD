using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingToysMovement : MonoBehaviour
{
    [SerializeField] private float moveDistance = 20f;
    [SerializeField] private float moveDuration = 2f;
    // Start is called before the first frame update
    void Start()
    {
        StartMovement();
    }

    public void StartMovement()
    {
        //movement from side to side
        transform.DOLocalMoveX(transform.localPosition.x + moveDistance, moveDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        transform.DOLocalMoveY(transform.localPosition.y + moveDistance / 2, moveDuration)
           .SetEase(Ease.InOutSine)
           .SetLoops(-1, LoopType.Yoyo);
    }

}