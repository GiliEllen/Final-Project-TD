using UnityEngine;
using DG.Tweening;

public class Duck : Toy
{
    public float moveDuration = 2f; // Base duration for movement
    private float minX = -4f;
    private float maxX = 4f;
    private float minYRotation = 40f;
    private float maxYRotation = 150f;
    private float zSpeed = 20f; // Speed in the Z direction 
    private float activeTimer = 0f;
    
    private void Start()
    {
        timeActive = 20f;
        isMovable = true;
        isMoving = true;
        timeActive = 20f;
        MoveDuck();
    }
    
    private void MoveDuck()
    {
        float targetZ = transform.position.z + zSpeed; // Move forward constantly
        transform.DOMoveZ(targetZ, timeActive).SetEase(Ease.Linear);
        
        float startX = transform.position.x;
        float firstTargetX = startX < (minX + maxX) / 2 ? maxX : minX;
        float firstMinR = startX < (minX + maxX) / 2 ? maxYRotation : minYRotation;
        
        Sequence movementSequence = DOTween.Sequence();
        movementSequence.Append(transform.DOMoveX(firstTargetX, moveDuration).SetEase(Ease.InOutSine))
                         .Append(transform.DOMoveX(startX < (minX + maxX) / 2 ? minX : maxX, moveDuration).SetEase(Ease.InOutSine))
                         .SetLoops(-1, LoopType.Yoyo);

        //  Sequence rotatingSequence = DOTween.Sequence();
        //  rotatingSequence.Append(transform.DORotate(startX < (minX + maxX) / 2 ? (new Vector3(0f, maxYRotation , 0f)): (new Vector3(0f, minYRotation , 0f)), 0.2f)).SetEase(Ease.InOutSine);
                        // .Append(transform.DORotate()
                        // .SetEase(Ease.InOutSine);
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
