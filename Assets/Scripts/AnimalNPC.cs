using UnityEngine;
using UnityEngine.AI;

public class AnimalNPC : MonoBehaviour
{
    //Rango de tiempo que dura el Idle (aleatorio)
    public Vector2 idleTimeRange = new Vector2(2, 5);

    public NavMeshAgent agent;

    //Cómo de lejos puede moverse
    public float moveDistance = 6;

    //Cuánto tiempo se queda cagando
    public Vector2 poopTimeRange = new Vector2(6, 8);

    //Rango para detectar cosas peligrosas
    public float detectionRange = 4;

    //Capas que considera como objetos peligrosos
    public LayerMask dangerLayers;

    //Distancia a la que huye cuando escapa de un peligro
    public float runAwayDistance = 6;

    //Ángulo con el que modificará la dirección de huida (aleatoriamente)
    [Range(0, 110)] //Esto sirve para que en el editor se vea como un slider
    public float runAwayAngle = 45;

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

    //Función para detectar objetos peligrosos a su alrededor
    public Transform DetectDanger()
    {
        //Guardar todos los colliders detectados por el Overlap
        Collider[] detectedObjects = Physics.OverlapSphere(transform.position, detectionRange, dangerLayers);

        if(detectedObjects.Length > 0)
        {
            //Devuelve el primer objeto que haya detectado, sea el que sea
            return detectedObjects[0].transform;
        }

        //Si no detecta nada, devuelde NULL
        return null;
    }

    private void OnDrawGizmos()
    {
        //Rango de movimiento
        Gizmos.color = Color.cornflowerBlue;
        Gizmos.DrawWireSphere(transform.position, moveDistance);

        //Rango de detección de objetos peligrosos
        Gizmos.color = Color.yellowNice;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
