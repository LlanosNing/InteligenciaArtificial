using UnityEngine;

public class StateMachine
{
    public State currentState;

    public void ChangeState(State newState)
    {
        if(currentState != null)
        {
            //Salir del estado en el que se encuentre
            currentState.OnExit();
            return;
        }

        //Asignar el nuevo estado
        currentState = newState;

        //Llamar al OnEnter del nuevo estado actual
        currentState.OnEnter();
    }
    
    public void Update()
    {
        Debug.Log($"Current state: {currentState.name}");
        currentState.OnUpdate();
    }
}
