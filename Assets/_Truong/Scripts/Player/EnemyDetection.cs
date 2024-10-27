using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyDetection : MonoBehaviour
{
    public SphereCollider sphereCollider;

    //public float detectionRadius = 15f;
    public LayerMask enemyLayer;

    public List<Transform> detecedEnemies = new List<Transform>();
    private Transform currentTarget;
    private int currentIndex = 0;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();

    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(2))
        {
            GetRandomEnemy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            detecedEnemies.Add(other.transform);
            if(currentTarget == null)
            {
                ChangeTarget(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            detecedEnemies.Remove(other.transform);
            if(currentTarget == other.transform)
            {
                currentTarget = null;
                ChangeTarget(null);
            }
        }
    }

    private void ChangeTarget(Transform newTarget)
    {
        if(newTarget == null) return;

        currentTarget = newTarget;
    }

    public Transform GetRandomEnemy()
    {
        if(detecedEnemies == null) return null;

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

        foreach(Transform enemy in detecedEnemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.position);
            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }
}
