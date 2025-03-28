using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolBehaviour : StateMachineBehaviour
{
    private static readonly int IsWalk = Animator.StringToHash("isWalk");
    private static readonly int IsRun = Animator.StringToHash("isRun");
    float timer;
    List<Transform> points = new List<Transform>();
    NavMeshAgent agent;
    
    Transform player;
    private float runRange = 5;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        Transform pointsObj = animator.gameObject.GetComponent<Enemy>().patrolRoute.transform;  //;   GameObject.FindGameObjectWithTag("Points").;
        foreach (Transform t in pointsObj)
            points.Add(t);
        agent = animator.GetComponent<NavMeshAgent>();

        agent.SetDestination(points[0].position);
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
            agent.SetDestination(points[Random.Range(0, points.Count)].position);
        
        timer +=Time.deltaTime;
        if (timer > 10)
            animator.SetBool(IsWalk, false);
        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance <= runRange) 
            animator.SetBool(IsRun, true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }

   
}
