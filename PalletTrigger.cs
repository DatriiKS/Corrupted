using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PalletTrigger : MonoBehaviour, ILevelEndCaller
{
    [SerializeField]
    private EndCallerInterfaceContainer _endCallerInterfaceContainer;
    [SerializeField]
    private Pile _pile;
    [SerializeField]
    private Booster _booster;

    private Collider _pileCollider;

    public event Action OnObstacleHit;
    public event Action OnLevelEndTriggerEnter;
    public event Action<bool> OnLevelEnded;

    private void Awake()
    {
        _pileCollider = GetComponent<Collider>();
        _booster.OnBoostValuePeaked += ScaleTrigger;
        _endCallerInterfaceContainer.InterfaceList.Add(this);
    }
    private void OnTriggerEnter(Collider other)
    {
        ProcessCollision(other);
    }
    private void ProcessCollision(Collider collider)
    {
        if (collider.TryGetComponent(out ICollectable collectable))
            Collect(collectable);
        else if (collider.GetComponent<Obstacle>())
        {
            OnObstacleHit?.Invoke();
            OnLevelEnded?.Invoke(false);
            _pile.HandleObstacleCollision(this);
            _pileCollider.enabled = false;
        }
        else if (collider.TryGetComponent(out LevelEndTrigger levelEndTrigger))
            OnLevelEndTriggerEnter?.Invoke();
    }

    private void Collect(ICollectable collectable)
    {
        if (collectable is Plank plank)
        {
            if (ColorValidator.CompareColors(gameObject, plank.gameObject))
            {
                _pile.AddPlankToPile(plank);
            }
            else
            {
                _pile.RemovePlankFromPile();
            }
        }
        else
        {
            collectable.OnCollected();
        }
    }

    private void ScaleTrigger(float boostTime)
    {
        StartCoroutine(ScaleTriggerRoutine(boostTime));
    }

    private IEnumerator ScaleTriggerRoutine(float boostTime)
    {
        Vector3 cashedLocalScale = transform.localScale;

        transform.localScale += Vector3.right * 2;
        yield return new WaitForSeconds(boostTime);
        transform.localScale = cashedLocalScale;
    }
    private void OnDisable()
    {
        _booster.OnBoostValuePeaked -= ScaleTrigger;
    }
}