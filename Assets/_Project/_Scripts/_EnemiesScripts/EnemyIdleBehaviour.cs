using UnityEngine;

public class EnemyIdleBehaviour : StateMachineBehaviour
{
    private static readonly int IsWalk = Animator.StringToHash("isWalk");
    private static readonly int IsRun = Animator.StringToHash("isRun");

    private float timer;
    Transform player;
    private float runRange = 10;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > 5)
            animator.SetBool(IsWalk, true);
        
        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance < runRange)
            animator.SetBool(IsRun, true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

   
}
