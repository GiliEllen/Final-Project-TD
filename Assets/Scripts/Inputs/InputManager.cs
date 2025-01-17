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

    public event Action OnDrag, OnDrop;

    private bool isTouching;

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
        if (Input.GetMouseButton(0))
        {
            isTouching = true;
            OnDrag?.Invoke();
        }

        if (Input.GetMouseButtonUp(0))
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
                case TouchPhase.Moved:
                    OnDrag?.Invoke();
                    break;

                case TouchPhase.Ended:
                    OnDrop?.Invoke();
                    break;
            }
        }
    }

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
