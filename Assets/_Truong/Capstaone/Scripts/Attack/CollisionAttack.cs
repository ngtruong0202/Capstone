using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAttack : MonoBehaviour
{
    public float time;

    private void OnEnable()
    {
        Invoke(nameof(DisableEffects), time);
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
