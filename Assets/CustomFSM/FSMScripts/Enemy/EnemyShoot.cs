using Unity.VisualScripting;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    [Header("STATES")]
    [SerializeField] private PatrolState patrol;

    [SerializeField] private CombatState combat;

    [SerializeField] private AlertState alert;

    private StateMachine stateMachine;

    private void Start()
    {
        //Crear y guardar una máquina de estados
        stateMachine = new StateMachine();

        //Empezar en un estado
        stateMachine.ChangeState(patrol);

        //Asignar los estados a cambiar cuando se cumplan condiciones
        patrol.Setup(stateMachine, alert);
        alert.Setup(stateMachine, combat);
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
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, alert.hearingRange);
    }
}
