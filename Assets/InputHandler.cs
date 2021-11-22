using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private TMP_Text _stylusAvailableText;
    [SerializeField] private TMP_Text _stylusUsedText;
    
    [Space]
    [SerializeField] private Color _color;

    private Color _originalColor;
    private const string STYLUS_AVAILABLE = "Stylus Touch Supported:";
    private const string STYLUS_USED = "Stylus used:";

    private void Start()
    {
        _originalColor = _camera.backgroundColor;
        _stylusAvailableText.text = $"{STYLUS_AVAILABLE} {Input.stylusTouchSupported}";
        _stylusUsedText.text = $"{STYLUS_USED} {false}";
    }

    public void OnStylusUsed()
    {
        _camera.backgroundColor = _color;
        _stylusUsedText.text = $"{STYLUS_USED} {true}";
    }

    public void OnResetButtonClicked()
    {
        _camera.backgroundColor = _originalColor;
        _stylusUsedText.text = $"{STYLUS_USED} {false}";
    }
}
