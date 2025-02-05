using UnityEngine;
using DG.Tweening;

public class EnemyPortal : MonoBehaviour
{
    private void Start()
    {
        transform.localScale = Vector3.one * 0.01f;

        transform.DOScale(0.6f, 0.5f)
            .OnComplete(() =>
            {
                DOVirtual.DelayedCall(1f, () =>
                {
                    transform.DOScale(0.01f, 0.5f).OnComplete(() =>
                    {
                        Destroy(gameObject);
                    });
                });
            });
    }
}
