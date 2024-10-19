using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawner;
    public Transform playerPositon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(playerPositon.position, transform.position) < 20f)
        {
            spawner.gameObject.SetActive(true);
        }
    }
}
