using TMPro;
using UnityEngine;

public class WidgetCoins : Widget
{
    private TextMeshProUGUI _textMeshPro;
    private const string RELATIVE_PATH = "/bankData.json";

    private void Awake()
    {
        _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
    }
    public override void Enable()
    {
        base.Enable();
        Bank.Instance.OnCoinsAmountChanged += Display;
        Display(PersistentDataHandler.Instance.GetDataObject<BankData>(RELATIVE_PATH).CoinsAmount);
    }
    
    private void Display(int value)
    {
        _textMeshPro.text = value.ToString();
    }
    public override void Disable()
    {
        base.Disable();
        Bank.Instance.OnCoinsAmountChanged -= Display;
    }
}
