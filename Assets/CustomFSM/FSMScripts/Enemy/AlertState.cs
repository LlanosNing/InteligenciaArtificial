using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class AlertState : State
{
    //Rango del OverlapSphere
    public float hearingRange = 6;

    //Ángulo del cono de visión
    public float visionAngle = 90;

    public LayerMask detectionLayers;
    public NavMeshAgent agent;

    //Referencia al jugador
    private Transform player;
    public override void OnEnter()
    {
        //Va a la última posición en la que vio la jugador (la posición en la que esté en ese momento)
        player = GameObject.FindWithTag("Player").transform;
        agent.SetDestination(player.position);
    }

    public override TransitionType OnUpdate()
    {
        //Primero comprueba alrededor con un Overlap
        Collider[] targets = Physics.OverlapSphere(agent.transform.position, hearingRange);
        if (targets.Length > 0)
        {
            //Calcula la dirección hacia el objeto detectado (player)
            Vector3 dirToTarget = player.position - agent.transform.position;

            dirToTarget.y = 0;

            //Calcular al producto escalar entre la dirección en la que mira y donde está el objetivo
            float dotProduct = Vector3.Dot(agent.transform.forward, dirToTarget.normalized);

            //Comprobar si está en su rango de visión
            if (dotProduct > Mathf.Cos(visionAngle * .5f * Mathf.Deg2Rad))
            {
                //Si detecta al objetivo, cambia de estado
                return TransitionType.PlayerDetected;
            }
        }

        //Si llega a la posición donde  vio al objeto, pero no lo detecta, 
        if (agent.remainingDistance < agent.stoppingDistance + .1f)
            return TransitionType.PlayerLost; //Devuelve una transición de tipo JUGADOR PERDIDO

        return TransitionType.None;
    }
}
