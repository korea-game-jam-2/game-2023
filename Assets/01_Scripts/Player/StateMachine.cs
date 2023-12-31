using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine<T> where T : class
{
    private List<IState<T>> _states = new List<IState<T>>();

    public IState<T> CurrentState { get => _currentState; }

    private IState<T> _currentState;
    private IState<T> _prevState;

    
    public StateMachine(IState<T> initState, T context)
    {
        initState.Initialize(context);
        _currentState = initState;
        _states.Add(initState);
    }

    public void AddState(IState<T> state, T context) {
        state.Initialize(context);
        _states.Add(state);
    }
    public void ChangeState<R>() {
        IState<T> state = _states.FirstOrDefault(state => state is R);
        if (state == null) return;
        _prevState = _currentState;
        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();

        
    }
    public void Execute() {
        _currentState.Execute();
    }
    public void Redo()
    {
        if (_prevState != null) {
            _currentState.Exit();
            _currentState = _prevState;
            _currentState.Enter();
        }
    }
}

public interface IState<T> where T : class
{

    void Enter();
    void Execute();
    void Exit();
    void Initialize(T context);
}
