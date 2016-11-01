using UnityEngine;
using System.Collections;
using System;

public class EnemySpawner : MonoBehaviour {
    public ArrayList spawners = new ArrayList();
    public GameObject Enemy;
   
	// Use this for initialization
	void Start () {
        foreach (GameObject spawner in GameObject.FindGameObjectsWithTag("EnemySpawner")) {
            if (!spawners.Contains(spawner)) {
                spawners.Add(spawner);
            }
        }
        SpawnEnemies();
	}

    private void SpawnEnemies()
    {
        foreach (GameObject spawner in spawners) {
            Enemy.gameObject.transform.position = spawner.transform.position;
            Instantiate(Enemy);
        }   
    }
}
