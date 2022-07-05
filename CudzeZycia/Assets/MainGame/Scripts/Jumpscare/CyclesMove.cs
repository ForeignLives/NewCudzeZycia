using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class CyclesMove : MonoBehaviour
{
    public NavMeshAgent _NavMeshAgent;
    public Animator _anim;
    public GameObject StartPoint, EndPoint;
    public float waittime;
    private float waittimesave; 
    private int point = 0;
    public bool StartJumpscare = false;
    Vector3 _PointStart, _PointEnd;
    void Start()
    {
        waittimesave = waittime;
        _NavMeshAgent.isStopped = false;
        _PointStart = StartPoint.transform.position;
        _PointEnd = EndPoint.transform.position;
    }
    private void Update()
    {



        if (_NavMeshAgent.isStopped == true)
        {
            if (StartJumpscare == true)
            {
                _anim.SetTrigger("Jumpscare");
                StartJumpscare = false;
            }
            else
                _anim.ResetTrigger("Jumpscare");

        }
        else
        {
            if (point == 0)
            {
                _NavMeshAgent.destination = _PointStart;
                if (_NavMeshAgent.remainingDistance < _NavMeshAgent.stoppingDistance)
                {
                    waittime -= Time.deltaTime;
                    if (waittime <= 0)
                    {
                        point = 1;
                        waittime = waittimesave;
                    }
                    _anim.SetBool("Walk", false);
                }
                else
                {
                    _anim.SetBool("Walk", true);
                }

            }
            else
            {
                _NavMeshAgent.destination = _PointEnd;
                if (_NavMeshAgent.remainingDistance < _NavMeshAgent.stoppingDistance)
                {
                    waittime -= Time.deltaTime;
                    if (waittime <= 0)
                    {
                        point = 0;
                        waittime = waittimesave;
                    }
                    _anim.SetBool("Walk", false);
                }
                else
                {
                    _anim.SetBool("Walk", true);
                }
            }
        }
    }


}
