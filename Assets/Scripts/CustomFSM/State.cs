using UnityEngine;

//abstract sirve para indicar que esta clase solo sirve para heredar de ella
public abstract class State 
{
    public string name = "State";

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
