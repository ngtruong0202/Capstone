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
        Debug.Log(gameObject.transform.position + "1");
        Debug.Log(destination.position + "destina");
        rb.MovePosition(destination.position);
        //gameObject.transform.position = destination.position;
        Debug.Log(gameObject.transform.position + "2");

    }

}
