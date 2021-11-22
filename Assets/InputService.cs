using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TouchPhase = UnityEngine.TouchPhase;

public class InputService : MonoBehaviour
{
    [SerializeField] private UnityEvent StylusClicked;
    [SerializeField] private UnityEvent MouseClicked;
    [SerializeField] private UnityEvent SingleTouchTapped;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _buttonText;

    public bool IsNewInputSystem;

    private const string NEW_INPUT_SYSTEM = "New Input System:";
    private const int DISTANCE_FOR_MOVE = 10;
    private bool _isOneTouchClicked;

    private void Awake()
    {
        UpdateButtonText();

        _button.onClick.AddListener(() =>
        {
            IsNewInputSystem = !IsNewInputSystem;
            UpdateButtonText();
        });
    }

    private void UpdateButtonText()
    {
        string status = IsNewInputSystem ? "on" : "off";
        _buttonText.text = $"{NEW_INPUT_SYSTEM} {status}";
    }

    private void Update()
    {
        if (IsNewInputSystem)
        {
            if (Pen.current != null)
            {
                if (Pen.current.tip.isPressed)
                {
                    Debug.Log("New Input System: tip");
                    HandleMouseClick();
                
                }
                else if(Pen.current.press.isPressed)
                {
                    Debug.Log("New Input System: press");
                    HandleMouseClick();
                }
            }
            else
            {
                Debug.LogWarning("New Input System: Stylus not available");
            }

            if (Pointer.current.press.isPressed)
            {
                Debug.Log("New Input System: Pointer is pressed");
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && Input.touchCount == 0)
            {
                Debug.Log("Stylus clicked");
                HandleMouseClick();
            }
            else if (Input.touchCount > 0)
            {
                Debug.Log("Touch tapped");
                HandleTouch();
            }
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

        bool isThresholdPassed = touch.deltaPosition.sqrMagnitude > DISTANCE_FOR_MOVE * DISTANCE_FOR_MOVE;
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