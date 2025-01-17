using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera sceneCamera;

    private Vector3 lastPosition;
    [SerializeField]
    private LayerMask placementLayermask;

    public event Action OnLongTouchStart, OnDrag, OnDrop, OnCancelPlacement;

    private float longTouchThreshold = 0.5f; 
    private float touchStartTime;
    private bool isLongTouch;
    private bool isTouching;
    private Vector2 touchStartPosition;

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
        if (Input.GetMouseButtonDown(0))
        {
            touchStartTime = Time.time;
            touchStartPosition = Input.mousePosition;
            isTouching = true;
            isLongTouch = false;
        }

        if (Input.GetMouseButton(0) && isTouching)
        {
            if (Time.time - touchStartTime > longTouchThreshold && !isLongTouch)
            {
                isLongTouch = true;
                OnLongTouchStart?.Invoke(); 
            }

            if (isLongTouch)
            {
                OnDrag?.Invoke(); 
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isLongTouch)
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
                    touchStartTime = Time.time;
                    touchStartPosition = touch.position;
                    isTouching = true;
                    isLongTouch = false;
                    break;

                case TouchPhase.Stationary:
                case TouchPhase.Moved:
                    if (isTouching && Time.time - touchStartTime > longTouchThreshold && !isLongTouch)
                    {
                        isLongTouch = true;
                        OnLongTouchStart?.Invoke(); 
                    }

                    if (isLongTouch)
                    {
                        OnDrag?.Invoke(); 
                    }
                    break;

                case TouchPhase.Ended:
                    if (isLongTouch)
                    {
                        OnDrop?.Invoke();
                    }
                    isTouching = false;
                    break;
            }
        }
    }

    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, placementLayermask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }
}
