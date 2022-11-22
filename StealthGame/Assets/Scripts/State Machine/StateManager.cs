using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public State currentState;

    public void SwitchState(State state)
    {
        currentState = state;
        state.EnterState(this);
    }

   
}
