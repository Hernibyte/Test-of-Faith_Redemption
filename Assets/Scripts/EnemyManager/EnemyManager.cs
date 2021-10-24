﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] List<GameObject> enemiesPrefab;
    [SerializeField] UnityEvent switchGatesOpen;
    [SerializeField] GameObject enemyGenerator;
    [SerializeField] float enemiesSpawnOffSet;
    [SerializeField] int minEnemiesPerRoom;
    [SerializeField] int maxEnemiesPerRoom;
    [HideInInspector] public int enemiesAmount;
    GameManager gameManager;

    void Awake() 
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void GenerateEnemy(GameObject prefab, Vector2 position)
    {
        GameObject obj = Instantiate(prefab, position, Quaternion.identity, transform);
        EnemyAttack enemyAttack = obj.GetComponent<EnemyAttack>();
        enemyAttack.playerTransform = gameManager.playerPrefab.transform;
    }

    public void GenerateEnemies()
    {
        BoxCollider2D enemyGeneratorCollider = enemyGenerator.GetComponent<BoxCollider2D>();
        //
        switchGatesOpen?.Invoke();
        //
        int rand = Random.Range(minEnemiesPerRoom, maxEnemiesPerRoom + 1);
        //
        for(int i = 0; i < rand; i++)
        {
            Vector2 spawnPosition = new Vector2();
            //
            spawnPosition.x = Random.Range(enemyGenerator.transform.position.x - enemyGeneratorCollider.size.x / 2, 
                                        enemyGenerator.transform.position.x + enemyGeneratorCollider.size.x / 2);
            //
            spawnPosition.y = Random.Range(enemyGenerator.transform.position.y - enemyGeneratorCollider.size.y / 2, 
                                        enemyGenerator.transform.position.y + enemyGeneratorCollider.size.y / 2);
            //
            switch(Random.Range(0, 2))
            {
                case 0:
                    GenerateEnemy(enemiesPrefab[0], spawnPosition);
                break;
                case 1:
                    GenerateEnemy(enemiesPrefab[1], spawnPosition);
                break;
            }
            enemiesAmount++;
        }
    }

    public void IfEnemyDie()
    {
        enemiesAmount--;
        if(enemiesAmount <= 0)
            switchGatesOpen?.Invoke();
    }
}