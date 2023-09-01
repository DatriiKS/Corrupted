using System;
using System.Collections;
using UnityEngine;

public class Bank : Singleton<Bank>
{
    public delegate void PointsAdded(int points);
    public event PointsAdded OnPointsAdded;
    public delegate void CoinsAdded(int coins);
    public event CoinsAdded OnCoinsAmountChanged;
    public event Action OnFinalLevelValueSet;

    private const string RELATIVE_PATH = "/bankData.json";

    public float LevelPointsMulptiplier { get; set; } = 1f;
    public int PointsOnLevel { get ; private set; }

    private float _lerpingValueDuration = 2f;

    private BankData _bankData;
    private void OnEnable()
    {
        _bankData = PersistentDataHandler.Instance.GetDataObject<BankData>(RELATIVE_PATH);
        Debug.Log($"BankDataLoaded {_bankData}");
    }
    public void AddPoints(int value, object sender)
    {
        switch (sender)
        {
            case Plank:
                PointsOnLevel += value;
                UpdatePoints();
                break;
            case Coin:
                _bankData.CoinsAmount += value;
                OnCoinsAmountChanged?.Invoke(_bankData.CoinsAmount);
                break;
        }
    }
    public bool Purchase(int currencyAmount)
    {
        if (_bankData.CoinsAmount >= currencyAmount)
        {
            _bankData.CoinsAmount -= currencyAmount;
            OnCoinsAmountChanged(_bankData.CoinsAmount);
            return true;
        }
        return false;
    }
    private void UpdatePoints()
    {
        OnPointsAdded?.Invoke(PointsOnLevel);
    }
    public void CallLevelValueUpdateRoutine()
    {
        StartCoroutine(MultiplyRoutine());
    }
    private IEnumerator MultiplyRoutine()
    {
        float timeElapsed = 0f;
        float startingValue = PointsOnLevel;
        while (timeElapsed < _lerpingValueDuration)
        {
            timeElapsed += Time.deltaTime;
            float lerpingPoints = Mathf.Lerp(PointsOnLevel, startingValue * LevelPointsMulptiplier, timeElapsed / _lerpingValueDuration);
            PointsOnLevel = Mathf.CeilToInt(lerpingPoints);
            UpdatePoints();
            yield return null;
        }
        _bankData.CoinsAmount += PointsOnLevel;
        OnCoinsAmountChanged?.Invoke(_bankData.CoinsAmount);
        CheckForNewHighScore(PointsOnLevel);
        OnFinalLevelValueSet?.Invoke();
    }

    private void CheckForNewHighScore(int endValue)
    {
        if (endValue > _bankData.BestResult)
        {
            _bankData.BestResult = endValue;
            PersistentDataHandler.Instance.SaveDataObject(_bankData, RELATIVE_PATH);
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        PointsOnLevel = 0;
    }
    protected override void OnApplicationQuit()
    {
        base.OnApplicationQuit();
        PersistentDataHandler.Instance.SaveDataObject(_bankData, RELATIVE_PATH);
        Debug.Log($"BankDataSaved {_bankData}");
    }
}
