using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   
    public void TeleportTo(Transform destination)
    {
        transform.position = destination.position;
    }
}
