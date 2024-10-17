using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    BaseState currentState;
    public void Start()
    {
        currentState = GetInitialState();
        if (currentState != null){
            currentState.Enter();
        }
    }

   

    // Update is called once per frame
    public void Update(){       
        if(currentState != null){
            currentState.UpdateLogic();
        }
     }

    public void LateUpdate()
    {
        if(currentState != null)
        {
            currentState.UpdatePhysic();
        }
    }

    public void changeState(BaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }


    protected virtual BaseState GetInitialState()
    {
        return null;
    }

   



}
