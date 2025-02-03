using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera sceneCamera;

    private Vector3 lastPosition = Vector3.zero;

    [SerializeField]
    private LayerMask placementLayermask;
    [SerializeField] private NewLevelManager levelManager;

    public event Action OnDrag, OnDrop;

    private bool isTouching;

    private void Start()
    {
        levelManager.LevelStarted += () => SetInputStatus(true);
        levelManager.LevelCompleted += () => SetInputStatus(false);
        levelManager.LevelLost += () => SetInputStatus(false);
    }

    private void SetInputStatus(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
    private void Update()
    {
#if UNITY_EDITOR
        HandleMouseInput();
#else
        HandleTouchInput();
#endif
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButton(0)) // Left mouse button is held
        {
            isTouching = true;
            OnDrag?.Invoke();
        }

        if (Input.GetMouseButtonUp(0)) // Left mouse button is released
        {
            if (isTouching)
            {
                OnDrop?.Invoke();
            }
            isTouching = false;
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                case TouchPhase.Moved:
                    isTouching = true;
                    OnDrag?.Invoke();
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (isTouching)
                    {
                        OnDrop?.Invoke();
                    }
                    isTouching = false;
                    break;
            }
        }
    }

    public Vector3 GetSelectedMapPosition()
    {
#if UNITY_EDITOR
        // Mouse position in the editor
        Vector3 mousePos = Input.mousePosition;
#else
        // Touch position for mobile
        Vector3 mousePos = Input.touchCount > 0 ? Input.GetTouch(0).position : Vector3.zero;
#endif

        mousePos.z = sceneCamera.nearClipPlane;

        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, placementLayermask))
        {
            lastPosition = hit.point;
        }

        return lastPosition;
    }
}
