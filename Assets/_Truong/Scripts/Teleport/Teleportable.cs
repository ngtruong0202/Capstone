using UnityEngine;

public class Teleportable : MonoBehaviour
{
    public Rigidbody rb;
    public bool canTeleport = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void TeleportTo(Transform destination)
    {
        rb.MovePosition(destination.position);
    }

}
