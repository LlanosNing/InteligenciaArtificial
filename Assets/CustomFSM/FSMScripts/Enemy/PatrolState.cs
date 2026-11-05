using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class PatrolState : State
{
    public NavMeshAgent agent;

    public Vector2 idleTimeInterval = new Vector2(1, 2);

    public Transform[] waypoints;

    //Índice del punto de ruta actual
    private int waypointIndex;

    private float idleTimer;


    public override void OnEnter()
    {
        //Iniciar el recorrido desde el primer waypoint
        waypointIndex = 0;

        //Inicializar el temporizador de Idle
        idleTimer = Random.Range(idleTimeInterval.x, idleTimeInterval.y);

        agent.SetDestination(waypoints[waypointIndex].position);

        
    }

    public override void OnUpdate()
    {
        //Comprobar si llega a su destino y pasar al siguiente Waypoint
        //Se pone agent porque es un State así que sin el agent no se puede poner en un objeto
        Vector3 direction = waypoints[waypointIndex].position - agent.transform.position;
        direction.y = 0;
        if(direction.sqrMagnitude <= agent.stoppingDistance + 0.05)
        {
            //Timer para estarse quieto
            idleTimer -= Time.deltaTime;

            //Cuando acaba el timer, pasa al siguiente Waypoint
            if(idleTimer <= 0)
            {
                //Reiniciar el timer y pasar al siguiente punto
                idleTimer = Random.Range(idleTimeInterval.x, idleTimeInterval.y);

                waypointIndex++;

                //Si ha llegado al último punto, vuelve a empezar desde el primero
                if (waypointIndex >= waypoints.Length)
                {
                    waypointIndex = 0;
                }

                agent.SetDestination(waypoints[waypointIndex].position);
            }

            
        }
    }
}
