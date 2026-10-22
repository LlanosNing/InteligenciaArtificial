using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class AB_RunAway : StateMachineBehaviour
{
    private AnimalNPC npc;

    private Vector3 destination;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        npc = animator.GetComponent<AnimalNPC>();

        if(Random.Range(1, 11) <= 2) //Probabilidad de que se cague
        {
            animator.SetBool("isPlantandoUnPino", true);
            return;
        }

        //Huir en cuanto entra en este estado
        destination = GetRunawayPosition(animator);
        npc.agent.SetDestination(GetRunawayPosition(animator));
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Calcular dirección al destino
        Vector3 direction = destination - animator.transform.position; //Usamos la posición del animator porque estamos usando el StateMachine de la animación
        //Quitar la altura de la dirección
        direction.y = 0;
        //Comprobar si ha llegado al destino
        if (direction.sqrMagnitude <= npc.agent.stoppingDistance + .5f)
        {
            //Cuando llega a su destino, comprueba si tiene algún peligro cerca, si no lo tiene, va al estado IDLE
            if(npc.DetectDanger() == null)
            {
                animator.SetBool("isIdle", true);
            }
            else //Si cuando huye sigue teniendo un objeto peligroso cerca, vuelve a huir 
            {
                destination = GetRunawayPosition(animator);
                npc.agent.SetDestination(destination);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isRunningAway", false);
        animator.SetBool("isIdle", true);
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

    private Vector3 GetRunawayPosition(Animator anim)
    {
        //Calcular la dirección contraria a la que se encuentra el objeto peligroso
        Vector3 direction = anim.transform.position - npc.DetectDanger().position;
        //Calcular rotación aleatoria //RoundToInt es para redondear ángulos
        int randomRotation = Random.Range(0, Mathf.RoundToInt(npc.runAwayAngle * .5f));
        //Aplicar una rotación a la dirección de huida. IMPORTANTE: Mantener este orden de los elementos
        //porque me paso las matemáticas por el culo y aquí el orden de los factores SÍ altera el productos
        direction = Quaternion.Euler(0, randomRotation, 0) * direction.normalized;
        //Calcular y devolver la posición sumando la nueva dirección a la posición actual
        return anim.transform.position + direction.normalized * npc.runAwayDistance;

    }
}