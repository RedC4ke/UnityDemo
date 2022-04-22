using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public void SpawnEnemy(string enemyTag)
    {
        GameObject enemyObject = ObjectPooler.Instance.SpawnFromPool(enemyTag, gameObject.transform.position, Quaternion.identity);
    }
}
