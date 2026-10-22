using UnityEngine;

public class AB_Patrol : StateMachineBehaviour
{
    private AnimalNPC npc;

    private Vector3 destination;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Esto hay que hacerlo en todos los StateMachine porque es muuuy divertido (no se puede hacer de otra forma)
        npc = animator.GetComponent<AnimalNPC>();
        //Mueve al agente a una posición aleatoria
        destination = npc.GetRandomPosition(); //Awuí guardamos variable
        npc.agent.SetDestination(destination); //Aquí la usamos
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Calcular dirección al destino
        Vector3 direction = destination - animator.transform.position; //Usamos la posición del animator porque estamos usando el StateMachine de la animación
        //Quitar la altura de la dirección
        direction.y = 0;
        //Comprobar si ha llegado al destino
        if (direction.sqrMagnitude <= npc.agent.stoppingDistance + .5f)
        {
            ChooseAction(animator); //Le pasamos el animator pa que funsione
        }

        //Si detecta un objeto pegriloso en algún momento, pasa al estado de RUN AWAY
        if (npc.DetectDanger() != null)
        {
            animator.SetBool("isRunningAway", true);
        }
    }

    void ChooseAction(Animator animator) //Le pasamos el animator pa que funsione
    {
        int choice = Random.Range(1, 11);

        //70% de probabilidad de cagar, 30% de volver a Idle
        if(choice <= 7)
        {
            Debug.Log("ME CAGO");
            //Pasar al estado de Cacota
            animator.SetBool("isPlantandoUnPino", true);
        }
        else
        {
            Debug.Log("Stoy Idleando");
            //Pasar al estado Idle
            animator.SetBool("isIdle", false);

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Cuando salga de este estado por la razón que sea, cambia el bool de isPatrolling a false
        animator.SetBool("isPatrolling", false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
