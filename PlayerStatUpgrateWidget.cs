using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUpgrateWidget : DisplayTextWidget
{
    [SerializeField]
    private protected TextMeshProUGUI _priceTextUGUI;
    [SerializeField]
    private protected float _incrementValue = 0.2f;
    [SerializeField]
    private Button _button;
    [SerializeField]
    private Image _secondaryImage;
    [SerializeField]
    private UpgradeParticlePlayer _upgradeParticlePlayer;

    private protected PlayerUpgradesData _playerUpgradesData;
    private protected Bank _bank;
    public PlayerUpgradesData PlayerUpgradesData { set 
            { _playerUpgradesData = value; 
               Debug.Log($"<color=red>{_playerUpgradesData == null}</color> ");
               Debug.Log($"<color=red>{_playerUpgradesData.ResultMultiplier} {_playerUpgradesData.SpeedMultiplier} {_playerUpgradesData.LaunchForceMultiplier}</color> ");
            }
        }
    private protected const string PLAYERSTATS_FILE_PATH = "/playerUpgradesData.json";
    private protected float _statValue;
    private protected int _costValue;
    public override void Enable()
    {
        base.Enable();
        _bank = Bank.Instance;
        _button.onClick.AddListener(UpdateStatValue);
        SetStringValue();   
    }
    public override void DisplayText()
    {
        _priceTextUGUI.text = _costValue.ToString();
        _textMeshProUGUI.text = _statValue.ToString("f1");
    }
    public virtual void UpdateStatValue()
    {
        SetStringValue();
        _upgradeParticlePlayer.PlayUpgradeEffect(_secondaryImage.color);
    }
    public virtual void SetStringValue()
    {
        DisplayText();
    }
    private protected virtual void UpdateStatCost(int currentUpdatePrice)
    {
        int newUpdatePrice = Mathf.RoundToInt(currentUpdatePrice * 5 / 100);
        _costValue += newUpdatePrice;
    }
}
