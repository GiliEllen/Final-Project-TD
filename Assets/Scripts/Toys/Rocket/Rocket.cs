using UnityEngine;
using DG.Tweening;

public class Rocket : Toy
{
    public float speed = 5f;  
    private Rigidbody rb;

    [SerializeField] float arcHeight = 5f;
    [SerializeField] float duration = 1f;
    [SerializeField] Vector3 startPoint;

    public Rocket()
    {
        hp = 5;
        isMovable = true;
        isMoving = true;
        canBulletBounce = false;
        shotDegree = 0;
        shotAmount = 0;
        manaCost = 5;
        gridWidth = 1;
        gridHeight = 1;
        timeActive = 5;
        type = "rocket";
    }

    private void Start()
    {
        AnimateRocket();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Nightmare enemy = collision.gameObject.GetComponent<Nightmare>();
        Debug.Log("heeeeeeeeeeeeeeeeeeeey");
        if (enemy != null)
        {
            if (!enemy.isInvisible) {    
                enemy.TakeDamage(20);
                TakeDamage(hp);
            }
        }  
        Transform root = transform.root; 
        root.gameObject.SetActive(false);
    }

    private void AnimateRocket()
    {
        InputManager inputManager = FindObjectOfType<InputManager>();

        Vector3 endPoint = inputManager.GetSelectedMapPosition();
        Vector3 controlPoint = (startPoint + endPoint) / 2 + Vector3.right * arcHeight;

        Vector3[] path = new Vector3[]
        {
            startPoint,
            controlPoint,
            endPoint,
        };
        transform.position = path[0];
        transform.DOPath(path, duration, PathType.CatmullRom)
            .SetEase(Ease.InOutSine)
            .SetLookAt(0.01f)
            .OnComplete(() => OnRocketReachedTarget());
    }

    private void OnRocketReachedTarget()
    {
        //here explosion animation
        Debug.Log("Rocket reached target");
        DestroyToy();
    }

    public override void DestroyToy() {
    isAlive = false;
    GameObject smoke = Instantiate(Resources.Load("Smoke"), transform.position, Quaternion.identity) as GameObject;
    
    Transform root = transform.root;     
    Destroy(root.gameObject);
    }

}
