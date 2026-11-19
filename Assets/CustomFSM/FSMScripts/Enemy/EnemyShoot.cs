using Unity.VisualScripting;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    [Header("STATES")]
    [SerializeField] private PatrolState patrol;

    [SerializeField] private CombatState combat;

    [SerializeField] private AlertState alert;

    [SerializeField] private FindCoverState findCover;

    private StateMachine stateMachine;

    private void Start()
    {
        //Crear y guardar una máquina de estados
        stateMachine = new StateMachine();

        //Empezar en un estado
        stateMachine.ChangeState(findCover);

        //Configurar las transiciones entre estados
        stateMachine.AddTransition(patrol, alert, TransitionType.PlayerDetected); //PATROL --> ALERT
        
        stateMachine.AddTransition(alert, combat, TransitionType.PlayerDetected); //ALERT --> COMBAT
                                                                                  
        stateMachine.AddTransition(alert, patrol, TransitionType.PlayerDetected); //ALERT --> PATROL

    }

    private void Update()
    {
        //Se llama a la función Update de la máquina de estados
        stateMachine.Update();
    }

    //Dibujar los rangos de efecto de combat, patrol, alert
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, combat.attackRange);
        Gizmos.color = Color.navajoWhite;
        Gizmos.DrawWireSphere(transform.position, patrol.detectionRange);
        Gizmos.color = Color.greenYellow;
        Gizmos.DrawWireSphere(transform.position, alert.hearingRange);
    }
}
