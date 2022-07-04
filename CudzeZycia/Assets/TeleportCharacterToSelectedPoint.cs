using UnityEngine;

public class TeleportCharacterToSelectedPoint : MonoBehaviour
{
    void TeleportPlayer(GameObject Point)
    {
        transform.position = Point.transform.position;
    }
}
