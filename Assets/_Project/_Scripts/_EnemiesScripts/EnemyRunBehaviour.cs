using UnityEngine;
using UnityEngine.AI;

public class EnemyRunBehaviour : StateMachineBehaviour
{
    private static readonly int IsAttacking = Animator.StringToHash("isAttack");
  
    private static readonly int IsRun = Animator.StringToHash("isRun");
    NavMeshAgent agent;

    private Transform player;

    private float attackRange = 2f;

    private float runRange = 10f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 4;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.position);
        float distance = Vector3.Distance(animator.transform.position, player.position);
        
        if (distance < attackRange)
            animator.SetBool(IsAttacking, true);
        if (distance > runRange)
            animator.SetBool(IsRun, false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
        agent.speed = 2;
    }


}
