using UnityEngine;
using UnityEngine.AI;
public class MoveToObject : MonoBehaviour
{
    public NavMeshAgent _NavMeshAgent;
    public string FindPoint;
    private GameObject GoToPoint;
    void Start()
    {
        GoToPoint = GameObject.Find(FindPoint);
        Vector3 _Point = GoToPoint.transform.position;
        _NavMeshAgent.destination = _Point;
        _NavMeshAgent.isStopped = false;
    }
}
