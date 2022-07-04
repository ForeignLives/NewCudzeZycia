using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovementRandom : MonoBehaviour
{
    enum AIStates
    {
        Idle,
        Wandering
    }

    public float maxwait = 120.0f;
    public float minwait = 60.0f;
    public float RangeMovement = 100f;
    public Animator _anim;

    [SerializeField]
    private NavMeshAgent agent = null;
    [SerializeField]
    private LayerMask floorMask = 0;

    private AIStates curState = AIStates.Idle;
    private float waitTimer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if(agent.remainingDistance > agent.stoppingDistance)
        {
            _anim.SetBool("Walk", true);
        }else
        {
            _anim.SetBool("Walk", false);
        }

        switch (curState)
        {
            case AIStates.Idle:
                
                DoIdle();
                break;
            case AIStates.Wandering:
                DoWander();
                break;
            default:
                Debug.LogError("Should not be here, away with you! D:");
                break;
        }
    }

    private void DoIdle()
    {
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            return;
        }
        agent.SetDestination(RandomNavSphere(transform.position, RangeMovement, floorMask));
        curState = AIStates.Wandering;
    }

    private void DoWander()
    {
        if (agent.pathStatus != NavMeshPathStatus.PathComplete)
            return;

        waitTimer = Random.Range(minwait, maxwait);
        curState = AIStates.Idle;
    }

    Vector3 RandomNavSphere(Vector3 origin, float distance, LayerMask layerMask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layerMask);

        return navHit.position;
    }
}