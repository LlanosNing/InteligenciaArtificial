using UnityEngine;

public class AB_Cacota : StateMachineBehaviour
{
    private AnimalNPC npc;

    private float timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //como no es MonoBehaviour NO puede hacer GetCompoenent. Se tiene que hacer a través del Animator
        npc = animator.GetComponent<AnimalNPC>();

        //Iniciar el timer con un tiempo aleatorio
        timer = Random.Range(npc.poopTimeRange.x, npc.idleTimeRange.y);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            //cambiar al estado IDLE usando la propia transición del Animators
            animator.SetBool("isIdle", true);
        }
        //Mientras está en este estado, da vueltas sobre sí mismo
        animator.transform.Rotate(0, 1500 * Time.deltaTime, 0);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isPlantandoUnPino", false);
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
