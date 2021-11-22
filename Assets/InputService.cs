using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputService : MonoBehaviour
{
    [SerializeField] private UnityEvent StylusClicked;
    [SerializeField] private UnityEvent MouseClicked;
    [SerializeField] private UnityEvent SingleTouchTapped;

    private int _distanceForMove = 10;
    private bool _isOneTouchClicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.touchCount == 0)
        {
            HandleMouseClick();
        }
        else if (Input.touchCount > 0)
        {
            HandleTouch();
        }
    }

    private void HandleMouseClick()
    {
        StylusClicked?.Invoke();
        MouseClicked?.Invoke();
    }

    private void HandleTouch()
    {
        if (Input.touchCount != 1)
        {
            _isOneTouchClicked = false;
            return;
        }

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            _isOneTouchClicked = true;
        }

        bool isThresholdPassed = touch.deltaPosition.sqrMagnitude > _distanceForMove * _distanceForMove;
        if (touch.phase == TouchPhase.Moved && isThresholdPassed)
        {
            _isOneTouchClicked = false;
        }

        if (touch.phase != TouchPhase.Ended || !_isOneTouchClicked)
        {
            return;
        }

        SingleTouchTapped?.Invoke();
    }
}