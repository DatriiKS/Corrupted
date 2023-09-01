using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(EndCallerInterfaceContainer))]
public class LevelBehavior : MonoBehaviour, ILevelStateSwitcher
{
    private EndCallerInterfaceContainer _endCallerInterfaceContainer;
    [SerializeField]
    private InputReader _inputReader;
    [SerializeField]
    private PlayerMover _playerMover;
    [SerializeField]
    private Finisher _finisher;
    [SerializeField]
    private Booster _booster;
    [SerializeField]
    private Man _man;
    [SerializeField]
    private PalletTrigger _palletTrigger;
    [SerializeField]
    private Canvas _mainCanvas;
    [SerializeField]
    private EndPhaseSpawner _endPhaseSpawner;
    [SerializeField]
    private EndPhaseTimer _endPhaseTimer;
    [SerializeField]
    private CameraSetup _cameraSetup;

    private BaseState _currentState;
    private List<BaseState> _allStates;
    private List<Widget> _widgets;

    private void Start()
    {
        _endCallerInterfaceContainer = GetComponent<EndCallerInterfaceContainer>();
        _widgets = _mainCanvas.GetComponentsInChildren<Widget>(true).ToList();

        _allStates = new List<BaseState>
        {
            new WaitingForStartState(_widgets,_inputReader,this),
            new PlayModeState(_endCallerInterfaceContainer, _endPhaseSpawner,_palletTrigger,_widgets,_man,_booster,_inputReader,_playerMover,this),
            new FinisherState(_endCallerInterfaceContainer, _widgets,_finisher,_cameraSetup,_endPhaseTimer,_inputReader,this),
            new EndedState(_widgets,_inputReader,this)
        };
        _currentState = _allStates[0];
        _currentState.Start();
    }

    public void SwitchState<T>() where T : BaseState
    {
        BaseState state = _allStates.FirstOrDefault(a => a is T);
        _currentState.Stop();
        state.Start();
        _currentState = state;
    }
}
