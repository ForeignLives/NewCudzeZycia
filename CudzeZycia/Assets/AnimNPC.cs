using UnityEngine.AI;
using UnityEngine;

public class AnimNPC : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator _anim;
    void Update()
    {
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            _anim.SetBool("Walk", true);
        }
        else
        {
            _anim.SetBool("Walk", false);
        }
    }
}
