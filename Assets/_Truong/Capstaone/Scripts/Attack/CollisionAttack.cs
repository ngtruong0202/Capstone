using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAttack : MonoBehaviour
{
    

    private void OnEnable()
    {
        Invoke(nameof(DisableEffects), 1.5f);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(DisableEffects));
    }

    public void DisableEffects()
    {
        gameObject.SetActive(false);
    }
}
