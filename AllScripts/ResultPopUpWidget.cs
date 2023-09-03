using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class ResultPopUpWidget : PopUpWidget
{
    [SerializeField]
    private TextMeshProUGUI _levelScoreTMPro;
    [SerializeField]
    private TextMeshProUGUI _addedLevelScoreTMPro;
    [SerializeField]
    private Button _sceneChangerButton;
    [SerializeField]
    private float _scoreAppearenceDuration;
    [SerializeField]
    private float _startingAnimatedScoreSize;
    public override void Enable()
    {
        base.Enable();
        string levelPoitsAmount = Bank.Instance.PointsOnLevel.ToString("f0");
        _levelScoreTMPro.text = levelPoitsAmount;
        _addedLevelScoreTMPro.text = $"+{levelPoitsAmount}";
        _addedLevelScoreTMPro.transform.localScale = Vector3.one * _startingAnimatedScoreSize;
        _addedLevelScoreTMPro.alpha = 0f;
        _sceneChangerButton.enabled = false;
        _sceneChangerButton.onClick.AddListener(SceneLoader.LoadNextScene);
    }

    private protected override void OnPopupAnimFinished()
    {
        _addedLevelScoreTMPro.DOFade(_fadeEndValue, _scoreAppearenceDuration);
        _addedLevelScoreTMPro.transform.DOScale(1f, _scoreAppearenceDuration);
        _sceneChangerButton.enabled = true;
    }
    public override void Disable()
    {
        base.Disable();
        _sceneChangerButton.onClick.RemoveListener(SceneLoader.LoadNextScene);
    }
}
