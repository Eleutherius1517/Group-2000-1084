using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPosition;

    public GameObject bombPrefab;
    public Transform spawnPositionBomb;

    private bool _isSpawnEnemies;
    [SerializeField] Enemyes _enemyes;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            this.TakeDownBomb();
        }
        _enemyes = FindObjectOfType<Enemyes>();
        if (_enemyes == null)
        {
            _isSpawnEnemies = true;
        }
    }

    private void TakeDownBomb()
    {
        var bombObj = Instantiate(bombPrefab, spawnPositionBomb.position, spawnPositionBomb.rotation);
    }

    void FixedUpdate()
    {


        if (_isSpawnEnemies)
        {
            _isSpawnEnemies = false;
            SpawnEnemies();
        }
        
    }

    private void SpawnEnemies()
    {
        var enemyObj = Instantiate(enemyPrefab, spawnPosition.position, spawnPosition.rotation);
        var enemy = enemyObj.GetComponent<Enemyes>();
        enemy.Init(20);
    }
}
