using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAttack : MonoBehaviour
{
    
    public float time;
    public Transform currentPos;
    private Transform originalChild;

    private void Awake()
    {
        currentPos = transform.parent;
        originalChild = currentPos.GetChild(currentPos.childCount -1);
    }

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
        
        transform.position = originalChild.position;
        gameObject.SetActive(false);
    }
}
