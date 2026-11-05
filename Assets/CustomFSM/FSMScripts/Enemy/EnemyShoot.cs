using Unity.VisualScripting;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    [Header("STATES")]
    [SerializeField] private PatrolState patrol;

    [SerializeField] private CombatState combat;

    private StateMachine stateMachine;

    private void Start()
    {
        //Crear y guardar una máquina de estados
        stateMachine = new StateMachine();
        //Empezar en un estado
        stateMachine.ChangeState(patrol);
    }

    private void Update()
    {
        //Se llama a la función Update de la máquina de estados
        stateMachine.Update();
    }
}
