using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class ControlsButton : MonoBehaviour
{
    private const float ControlsMenuHeight = 450f;
    [SerializeField] private Button _controlsButton;
    [SerializeField] private Transform _panel;

    private bool isOpen = false;
    void Start()
    {
        _controlsButton.onClick.AddListener(Move);
    }

    private void Move()
    {
        if (isOpen)
        {
            _panel.position = new Vector3(_panel.position.x, _panel.position.y - ControlsMenuHeight, _panel.position.z);
        }
        else
        {
            _panel.position = new Vector3(_panel.position.x, _panel.position.y + ControlsMenuHeight, _panel.position.z);
        }
        isOpen = !isOpen;
    }
}
