using UnityEngine;

public class KillNPCAfterExit : MonoBehaviour
{
    private bool isvisible = false;
    private bool MustBeDead = false;


    private void Update()
    {
        if (MustBeDead == true && isvisible == false)
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameVisible()
    {
        isvisible = true;
    }

    private void OnBecameInvisible()
    {
        isvisible = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Spawner")
        MustBeDead = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Spawner")
            MustBeDead = false;
    }
}
