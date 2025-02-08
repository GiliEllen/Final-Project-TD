using UnityEngine;
using DG.Tweening;

public class Duck : Toy
{
    public float moveDuration = 1.3f; // Base duration for movement
    private float minX = -4f;
    private float maxX = 4f;
    private float minYRotation = 30f;
    private float maxYRotation = 160f;
    private float zSpeed = 20f; // Speed in the Z direction 
    private float activeTimer = 0f;
    
    private void Start()
    {
        timeActive = 10f;
        isMovable = true;
        isMoving = true;
        timeActive = 20f;
        MoveDuck();
    }
    
    private void MoveDuck()
    {
        float targetZ = transform.position.z + zSpeed; // Move forward constantly
        // transform.DOMoveZ(targetZ, timeActive).SetEase(Ease.Linear);
        
        float startX = transform.position.x;
        float firstTargetX = startX < (minX + maxX) / 2 ? maxX : minX;
        float firstMinR = startX < (minX + maxX) / 2 ? maxYRotation : minYRotation;
        
        Sequence movementSequence = DOTween.Sequence();

        movementSequence
            .Append(transform.DORotate(startX < (minX + maxX) / 2 ? 
                new Vector3(0f, maxYRotation, 0f) : new Vector3(0f, minYRotation, 0f), 0.1f))
            .SetEase(Ease.InOutSine)

            .Append(transform.DOMoveZ(transform.position.z + 1f, moveDuration)
                .SetEase(Ease.InOutSine))
            .Join(transform.DOMoveX(firstTargetX, moveDuration)
                .SetEase(Ease.InOutSine))

            .Append(transform.DORotate(startX < (minX + maxX) / 2 ? 
                new Vector3(0f, minYRotation, 0f) : new Vector3(0f, maxYRotation, 0f), 0.8f))
            .SetEase(Ease.InOutSine)

            .Append(transform.DOMoveZ(transform.position.z + 3f, moveDuration)
                .SetEase(Ease.InOutSine))
            .Join(transform.DOMoveX(startX < (minX + maxX) / 2 ? minX : maxX, moveDuration)
                .SetEase(Ease.InOutSine))
                .Append(transform.DORotate(startX < (minX + maxX) / 2 ? 
                new Vector3(0f, maxYRotation, 0f) : new Vector3(0f, minYRotation, 0f), 0.1f))
            .SetEase(Ease.InOutSine)

            .Append(transform.DOMoveZ(transform.position.z + 6f, moveDuration)
                .SetEase(Ease.InOutSine))
            .Join(transform.DOMoveX(firstTargetX, moveDuration)
                .SetEase(Ease.InOutSine))

            .Append(transform.DORotate(startX < (minX + maxX) / 2 ? 
                new Vector3(0f, minYRotation, 0f) : new Vector3(0f, maxYRotation, 0f), 0.8f))
            .SetEase(Ease.InOutSine)

            .Append(transform.DOMoveZ(transform.position.z + 9f, moveDuration)
                .SetEase(Ease.InOutSine))
            .Join(transform.DOMoveX(startX < (minX + maxX) / 2 ? minX : maxX, moveDuration)
                .SetEase(Ease.InOutSine))
                .Append(transform.DORotate(startX < (minX + maxX) / 2 ? 
                new Vector3(0f, maxYRotation, 0f) : new Vector3(0f, minYRotation, 0f), 0.1f))
            .SetEase(Ease.InOutSine)

            .Append(transform.DOMoveZ(transform.position.z + 12f, moveDuration)
                .SetEase(Ease.InOutSine))
            .Join(transform.DOMoveX(firstTargetX, moveDuration)
                .SetEase(Ease.InOutSine))

            .Append(transform.DORotate(startX < (minX + maxX) / 2 ? 
                new Vector3(0f, minYRotation, 0f) : new Vector3(0f, maxYRotation, 0f), 0.8f))
            .SetEase(Ease.InOutSine)

            .Append(transform.DOMoveZ(transform.position.z + 15f, moveDuration)
                .SetEase(Ease.InOutSine))
            .Join(transform.DOMoveX(startX < (minX + maxX) / 2 ? minX : maxX, moveDuration)
                .SetEase(Ease.InOutSine));
    }

     private void OnCollisionEnter(Collision collision)
    {
        Nightmare enemy = collision.gameObject.GetComponent<Nightmare>();
        if (enemy != null)
        {
            if (!enemy.isInvisible) {     
                enemy.TakeDamage(enemy.hp);
            } else {
                return;
            }
        } 
    }

        private void Deactivate()
    {
        gameObject.SetActive(false);
        DestroyToy();
    }

        private void Update()
    {
        activeTimer += Time.deltaTime;
        if (activeTimer >= timeActive)
        {
           Deactivate();
        }
        
    }
}
