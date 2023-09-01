using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(MeshRenderer))]
public class Coin : MonoBehaviour, ICollectable, IBoosterAffectable
{
    [SerializeField]
    float _lerpTime;

    private int _coinValue = 1;
    private AudioSource _audioSource;
    private MeshRenderer _meshRenderer;
    public bool IsCollected { get; set; } = false;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    public void OnCollected()
    {
        if (!IsCollected)
        {
            _audioSource.Play();
            Bank.Instance.AddPoints(_coinValue, this);
            _meshRenderer.enabled = false;
            enabled = false;
        }
        IsCollected = true;
    }
    public void TakeBoosterAffect(Transform transform)
    {
        StartCoroutine(MoveToPosition(transform));
    }
    private IEnumerator MoveToPosition(Transform newTransform)
    {
        float elapsedTime = 0f;
        Vector3 startingPos = transform.position;
        while (elapsedTime < _lerpTime)
        {
            //Debug.Log(name + (newTransform.position - transform.position).magnitude);
            transform.position = Vector3.Lerp(startingPos, newTransform.position, elapsedTime / _lerpTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        OnCollected();
    }
}
