using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(SurfaceDetector))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField]
    private EndCallerInterfaceContainer _endCallerInterfaceContainer;
    [SerializeField]
    private LevelEndTrigger _levelEndTrigger;
    [SerializeField]
    private Pile _pile;
    [SerializeField]
    private Booster _booster;
    [SerializeField]
    private PlayerModelAnimController _playerModelAnimController;

    private CharacterController _characterController;
    private SurfaceDetector _surfaceDetector;
    private InputReader _inputReader;

    private const float GRAVITY = 9.81f;
    private const float GROUNDED_GRAVITY = 1f;
    private const string RELATIVE_PATH = "/playerUpgradesData.json";
    private float _baseSpeed = 5f;
    private float _speed;
    private float _maxRunningSpeed;
    private float _maxBoostedSpeed;

    private Transform _charControllerTf;
    private Vector2 _nonBoostedSpeedRange;
    private bool _isBoosted = false;

    public float Speed
    {
        get => _speed;
        private set { _speed = value; _playerModelAnimController.SetMovementBlendTreeValue(_nonBoostedSpeedRange, _speed); }
    }
    public float BoostedSpeed { get => _maxBoostedSpeed; }

    private void OnEnable()
    {
        _pile.OnPlankAdded += IncreaseSpeed;
        _pile.OnPlankRemoved += SetBaseSpeed;
        _inputReader.OnInputInitialized += SetBaseSpeed;
        _booster.OnBoostValuePeaked += EnterBoostedMode;
        _levelEndTrigger.OnEndTriggerEntered += DisableMovement;

        _endCallerInterfaceContainer.OnLevelEnded += DisableMovement;
    }
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _surfaceDetector = GetComponent<SurfaceDetector>();
        _inputReader = GetComponent<InputReader>();

        _charControllerTf = _characterController.transform;
    }
    public void SetUpSpeedValues()
    {
        PlayerUpgradesData playerUpgradesData = JsonDataService<PlayerUpgradesData>.LoadData(RELATIVE_PATH);
        _maxRunningSpeed = _baseSpeed + playerUpgradesData.SpeedMultiplier;
        _maxBoostedSpeed = _maxRunningSpeed * 1.5f;
        _nonBoostedSpeedRange = new Vector2(0, _maxRunningSpeed);
        SetBaseSpeed();
    }

    private void EnterBoostedMode(float boostTime)
    {
        StartCoroutine(BoostRoutine(boostTime));
    }

    private IEnumerator BoostRoutine(float boostTime)
    {
        float cashedSpeed = Speed;
        Debug.LogWarning("Cashed speed " + Speed);
        _isBoosted = true;

        Speed = _maxBoostedSpeed;
        Debug.LogWarning("Boosted speed " + Speed);
        yield return new WaitForSeconds(boostTime);
        _isBoosted = false;

        Speed = cashedSpeed;
        Debug.LogWarning("Boost Ended " + Speed);
    }

    private void SetBaseSpeed()
    {
        Speed = _baseSpeed;
    }

    private void IncreaseSpeed(float plankScore)
    {
        if (!_isBoosted)
        {
            Debug.LogWarning("Current speed " + Speed);
            float clampedSpeed = Mathf.Clamp(Speed += plankScore, _baseSpeed, _maxRunningSpeed);
            Speed = clampedSpeed;
        }
    }

    public void FixedUpdate()
    {
        (float, float)? limit = _surfaceDetector.CalculateClamp();
        if (limit.HasValue)
        {
            Move(limit);
        }
        else
        {
            Debug.Log($"<color=red> No surface underneath player</color>");
            Move((-float.MaxValue, float.MaxValue));
        }
    }
    private void Move((float, float)? limit)
    {
        float xValue = _inputReader.CurrentSideDelta;
        float yValue = GetGravityValue();
        Vector3 direction = _charControllerTf.forward;
        Vector3 projectedDirection = _surfaceDetector.Project(direction).normal return new Color(c1.r, c1.g, c1.b, a);
        }


        public static bool Compare(this Vector3 v1, Vector3 v2, int accuracy)
        {
            bool x = (int)(v1.x * accuracy) == (int)(v2.x * accuracy);
            bool y = (int)(v1.y * accuracy) == (int)(v2.y * accuracy);
            bool z = (int)(v1.z * accuracy) == (int)(v2.z * accuracy);

            return x && y && z;
        }

        public static bool Compare(this Quaternion q1, Quaternion q2, int accuracy)
        {
            bool x = (int)(q1.x * accuracy) == (int)(q2.x * accuracy);
            bool y = (int)(q1.y * accuracy) == (int)(q2.y * accuracy);
            bool z = (int)(q1.z * accuracy) == (int)(q2.z * accuracy);
            bool w = (int)(q1.w * accuracy) == (int)(q2.w * accuracy);

            return x && y && z && w;
        }

        //public static void AddElementAtIndex<T>(this T[] array, int writeIndex, T item)
        //{
        //    if (writeIndex >= array.Length)
        //        System.Array.Resize(ref array, Mathf.NextPowerOfTwo(writeIndex + 1));

        //    array[writeIndex] = item;
        //}

        /// <summary>
        /// Insert item into array at index.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /