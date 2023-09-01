using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FinisherState : LevelEndCallerStateBase
{
    private EndPhaseTimer _endPhaseTimer;
    private CameraSetup _cameraSetup;
    private Finisher _finisher;

    private float _finisherRoutineTime = 5f;
    private WaitForSeconds _delay;
    public FinisherState(
        EndCallerInterfaceContainer endCallerInterfaceContainer,
        List<Widget> allWidgets,
        Finisher finisher,
        CameraSetup cameraSetup,
        EndPhaseTimer endPhaseTimer,
        InputReader inputReader, 
        ILevelStateSwitcher levelStateSwitcher)
    : base(allWidgets,endCallerInterfaceContainer,inputReader, levelStateSwitcher) 
    {
        _endPhaseTimer = endPhaseTimer;
        _cameraSetup = cameraSetup;
        _finisher = finisher;
    }
    public override void Start()
    {
        base.Start();
        FillWidgetList();
        TurnOnWidgets();
        _finisher.OnFinalValueSet += TurnOffFinisherWidget;

        _delay = new WaitForSeconds(_finisherRoutineTime * .5f);
        CoroutineRunner.Instance.StartCoroutine(FinisherTimeRoutine());
    }
    private IEnumerator FinisherTimeRoutine()
    {
        _finisher.StartFinisherRoutine();
        Time.timeScale *= .5f;
        yield return _delay;
        Time.timeScale *= 2f;
        _cameraSetup.SetLerpable(new ListLerpableBehaviour(_endPhaseTimer, _cameraSetup, _cameraSetup.GetComponent<Camera>()));
    }
    private void TurnOffFinisherWidget(float f)
    {
        ToggleSpecificWidget(typeof(FinisherWidget), false);
    }
    private void SwitchState()
    {
        _levelStateSwitcher.SwitchState<EndedState>();
    }
    public override void Stop()
    {
        base.Stop();
        TurnOffWidgets();
        ClearWidgetList();
    }

    public override void HandleLevelEnding()
    {
        SwitchState();
    }

    private protected override void FillWidgetTypes()
    {
        _types.Add(typeof(FinisherWidget));
        _types.Add(typeof(WidgetCoins));
        _types.Add(typeof(WidgetPoints));
    }
}
