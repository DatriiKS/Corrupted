using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputInitializerWidgetButton : Widget
{
    [SerializeField]
    private Button _button;
    [SerializeField]
    private InputReader _inputReader;
    public override void Enable()
    {
        base.Enable();
        _button.onClick.AddListener(InitializeInput);
    }
    private void InitializeInput()
    {
        _inputReader.InitializeInput();
    }
    public override void Disable()
    {
        base.Enable();
        _button.onClick.RemoveListener(InitializeInput);
    }
}
