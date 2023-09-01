using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class BestResultWidget : Widget
{
    [SerializeField]
    private TextMeshProUGUI _bestResultTMPro;
    [SerializeField]
    private float _scaleMultiplier;
    [SerializeField]
    private float _bounceDuration;

    private const string RELATIVE_PATH = "/bankData.json";
    public override void Enable()
    {
        base.Enable();
        BankData bankData = PersistentDataHandler.Instance.GetDataObject<BankData>(RELATIVE_PATH);
        _bestResultTMPro.text = $"Best Result: {bankData.BestResult:f0}";
        transform.DOScale(transform.localScale * _scaleMultiplier, _bounceDuration).SetLoops(-1,LoopType.Yoyo);
    }
}
