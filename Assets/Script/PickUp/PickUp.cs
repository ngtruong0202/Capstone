using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    PickUpManager pickUpManager;

    void Start()
    {
        pickUpManager = FindAnyObjectByType<PickUpManager>();
    }


}
