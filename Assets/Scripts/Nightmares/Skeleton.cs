using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Nightmare
{

    protected virtual void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
        Hoop hoop = collidedObject.GetComponent<Hoop>();
        if (hoop != null)
        {
            KnockBack(hoop.GetKnockBackForceMagnitude());
        }
        else
        {
            Ball ball = collidedObject.GetComponent<Ball>();
            if (ball != null)
                KnockBack(ball.GetKnockBackForce());
        }
    }

    private void KnockBack(float knockBackMagnitude)
    {
        isMoving = false;
        transform.DOMoveZ(transform.position.z + knockBackMagnitude, 0.4f).OnComplete(() => isMoving = true);
    }
}