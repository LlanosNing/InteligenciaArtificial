using UnityEngine;

//abstract sirve para indicar que esta clase solo sirve para heredar de ella
[System.Serializable]
public abstract class State 
{
    public string name = "Default state";

    //El estado al que debe cambiar cuando se cumpla la condición
    public State stateToChange;

    //La máquina de estados con la que debe cambiar de uno a otro
    protected StateMachine sm;

    public void Setup(StateMachine stateMachine, State state)
    {
        sm = stateMachine;  
        stateToChange = state;
    }

   public virtual void OnEnter()
    {

    } 

   
    public virtual void OnUpdate()
    {

    }


    public virtual void OnExit() 
    { 
    
    }
}
