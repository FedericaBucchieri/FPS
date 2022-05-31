using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerationPoint : MonoBehaviour
{
    [Tooltip("Enemy prefab to instantiate")]
    public GameObject enemyPrefab;
    [Tooltip("time interval between one enemy generation to the next one")]
    public float generationInterval;

    float idleTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        idleTime += Time.deltaTime;

        if(idleTime >= generationInterval)
        {
            idleTime = 0f;
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
    }

}
