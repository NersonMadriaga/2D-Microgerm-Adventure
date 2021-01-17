using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject greenEnemy;
    [SerializeField] private GameObject brownEnemy;
    [SerializeField] private GameObject zombieEnemy;

    private Vector2 enemyPosition;

    private void Start()
    {
        enemyPosition = gameObject.transform.position;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, enemyPosition, Quaternion.identity);
    }

    public void SpawnGreenSlime()
    {
        SpawnEnemy(greenEnemy);
    }

    public void SpawnBrownSlime()
    {
        SpawnEnemy(brownEnemy);
    }

    public void ZombieEnemy()
    {
        SpawnEnemy(zombieEnemy);
    }
}
