using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FSMLite
{
    private Dictionary<string, State> _stateDict;
    private State _currentState;

    public FSMLite()
    {
        _stateDict = new Dictionary<string, State>();
        _currentState = new State();
    }

    public void Start(string stateName)
    {
        lock (_stateDict)
        {
            if (_stateDict.ContainsKey(stateName))
            {
                _currentState = _stateDict[stateName];
                _currentState.OnStateInEvent.Invoke();
            }
            else
            {
                Debug.LogError($"No state named : {stateName} exists!");
            }
        }
    }

    public void SetState(string stateName)
    {
        lock (_stateDict)
        {
            if (_currentState.Name.Equals(stateName))
            {
                //skip
            }
            else
            {
                if (_stateDict.ContainsKey(stateName))
                {
                    _currentState.OnStateOutEvent?.Invoke();
                    _currentState = _stateDict[stateName];
                    _currentState.OnStateInEvent.Invoke();
                }
                else
                {
                    Debug.LogError($"No state named : {stateName} exists!");
                }
            }
        }
    }

    public State GetState()
    {
        lock (_stateDict)
        {
            return _currentState;
        }
    }

    public State GetState(string stateName)
    {
        lock (_stateDict)
        {
            if (_stateDict.ContainsKey(stateName))
            {
                return _stateDict[stateName];
            }
        }

        return null;
    }

    public void RegisterState(State state)
    {
        lock (_stateDict)
        {
            if (_stateDict.ContainsKey(state.Name))
            {
                Debug.LogError($"Duplicate state name : {state.Name}");
            }
            else
            {
                _stateDict.Add(state.Name, state);
            }
        }
    }

    public void UnRegisterState(string stateName)
    {
        lock (_stateDict)
        {
            _stateDict.Remove(stateName);
        }
    }

    public void Clear()
    {
        lock (_stateDict)
        {
            foreach (var sp in _stateDict)
            {
                sp.Value.OnStateInEvent.RemoveAllListeners();
                sp.Value.OnStateOutEvent.RemoveAllListeners();
            }
            _stateDict.Clear();
        }
    }

    public class State
    {
        public string Name = "";
        public UnityEvent OnStateInEvent = new UnityEvent();
        public UnityEvent OnStateOutEvent = new UnityEvent();
    }
}