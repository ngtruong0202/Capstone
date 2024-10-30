using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyDetection : MonoBehaviour
{
    public SphereCollider sphereCollider;

    public List<Transform> detecedEnemies = new List<Transform>();
    public LayerMask enemyLayer;
    public Transform currentTarget;

    private int currentIndex = 0;

  
    private void Update()
    {
        if(Input.GetMouseButtonDown(2))
        {
            GetTransformEnemy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            detecedEnemies.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            detecedEnemies.Remove(other.transform);           
        }
    }    

    public Transform GetTransformEnemy()
    {
        detecedEnemies.RemoveAll(x => x == null);
        currentTarget = null;

        if (detecedEnemies.Count == 0) return null;
       
        Transform enemy = detecedEnemies[currentIndex];
        currentIndex = (currentIndex + 1) % detecedEnemies.Count;

        currentTarget = enemy;
        Debug.Log(currentTarget.name);

        return currentTarget;
    }

    public Transform GetClosestEnemy()
    {
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform enemy in detecedEnemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
                currentTarget = closestEnemy;
            }
        }
        Debug.Log(closestEnemy.name);
        return closestEnemy;
    }

    private void ChangeTarget(Transform newTarget)
    {
        if (newTarget == null) return;

        currentTarget = newTarget;
    }

}
