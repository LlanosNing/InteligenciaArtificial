using UnityEngine;
using UnityEngine.AI;

public class AnimalNPC : MonoBehaviour
{
    //Rango de tiempo que dura el Idle (aleatorio)
    public Vector2 idleTimeRange = new Vector2(2, 5);

    public NavMeshAgent agent;
    //Cómo de lejos puede moverse
    public float moveDistance = 6;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //Calcula una posición aleatoria dentro del NavMesh
    public Vector3 GetRandomPosition()
    {
        //Generar una posición aleatoria en base a la distancia de movimiento
        Vector3 position = new Vector3(Random.Range(-moveDistance, moveDistance), 0, Random.Range(-moveDistance, moveDistance));

        //Hay que sumarle la posición del objeto o si no, será siempre respecto al origen del mundo (0, 0, 0)
        position += transform.position;

        //Con esta función se puede comprobar si el punto está dentro de la NavMesh
        //El valor de maxDistance siempre es el doble de la altura de la agente
        //Las áreas son como las Layers (ma o menos)
        if (NavMesh.SamplePosition(position, out NavMeshHit hit, agent.height * 2, NavMesh.AllAreas))
        {
            //Devuelve una posición válida dentro del NavMesh
            return hit.position;    
        }

        return Vector3.zero;
    }
    private void OnDrawGizmos()
    {
        //azulico
        Gizmos.color = Color.cornflowerBlue;
        Gizmos.DrawWireSphere(transform.position, moveDistance);
    }
}
