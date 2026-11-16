using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class FindCoverState : State
{
    public float searchRange = 20;
    public LayerMask coverLayers;
    public float speedModifier = 3;
    public NavMeshAgent agent;

    public override void OnEnter()
    {
        FindCover();
    }

    public override TransitionType OnUpdate()
    {
        return TransitionType.None;
    }

    void FindCover()
    {
        //Busca todas las posibles coberturas en su rango
        Collider[] covers = Physics.OverlapSphere(agent.transform.position, searchRange, coverLayers);

        if (covers.Length <= 0)
            return;

        //Buscar la posición dentro del NavMesh más cercana a la posición de la cobertura
        if (NavMesh.SamplePosition(covers[0].transform.position, out NavMeshHit hit, agent.height * 2, agent.areaMask))
        {
            //Guardar la posición de la cobertura ya dentro del NavMesh
            Vector3 coverPos = hit.position;

            //Buscar la arista de la NavMesh más cercana a la posición de la cobertura
            //La arista guarda la normal de la superficie, que es lo que nos interesa
            if (NavMesh.FindClosestEdge(covers[0].transform.position, out hit, agent.areaMask))
            {
                agent.SetDestination(hit.position);
            }
        }
    }

    public override void DrawGizmos()
    {
        Gizmos.color = Color.hotPink;
        Gizmos.DrawWireSphere(agent.transform.position, searchRange);
    }
}
