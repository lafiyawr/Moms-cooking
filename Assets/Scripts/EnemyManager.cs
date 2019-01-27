using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]private List<Enemy> _wave;

    private const int NUM_ENEMIES_IN_WAVE = 20;
    private const int MAX_TIME_BEFORE_ENEMY_SPAWN_RD1 = 5;
    private const int MIN_TIME_BEFORE_ENEMY_SPAWN_RD1 = 1;
    private const int MAX_TIME_BEFORE_ENEMY_SPAWN_RD2 = 3;
    private const int MIN_TIME_BEFORE_ENEMY_SPAWN_RD2 = 1;
    private const int MAX_TIME_BEFORE_ENEMY_SPAWN_RD3 = 2;
    private const int MIN_TIME_BEFORE_ENEMY_SPAWN_RD3 = 1;
    private float _spawnTime;
    
    // Start is called before the first frame update
    void Start()
    {
        _spawnTime = 5;
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void EmitWave(int round)
    {
        _spawnTime -= Time.deltaTime;
        if (round == 1)
        {
            if (_spawnTime <= 0)
            {
                GameObject g = Services.GameManager.CreateGameObject(Services.PrefabDatabase.Actors[1]);
                _wave.Add(g.GetComponent<Enemy>());
    //            g.transform.position = new Vector3(-10f, Random.Range(-5f, 5f), 0);
                _spawnTime = Random.Range(MIN_TIME_BEFORE_ENEMY_SPAWN_RD1, MAX_TIME_BEFORE_ENEMY_SPAWN_RD1);
            }
        }

        if (round == 2)
        {
            if (_spawnTime <= 0)
            {
                GameObject g = Services.GameManager.CreateGameObject(Services.PrefabDatabase.Actors[2]);
                _wave.Add(g.GetComponent<Enemy>());
                //            g.transform.position = new Vector3(-10f, Random.Range(-5f, 5f), 0);
                _spawnTime = Random.Range(MIN_TIME_BEFORE_ENEMY_SPAWN_RD2, MAX_TIME_BEFORE_ENEMY_SPAWN_RD2);
            }
        }

        if (round == 3)
        {
            if (_spawnTime <= 0)
            {
                GameObject g = Services.GameManager.CreateGameObject(Services.PrefabDatabase.Actors[3]);
                _wave.Add(g.GetComponent<Enemy>());
                //            g.transform.position = new Vector3(-10f, Random.Range(-5f, 5f), 0);
                _spawnTime = Random.Range(MIN_TIME_BEFORE_ENEMY_SPAWN_RD3, MAX_TIME_BEFORE_ENEMY_SPAWN_RD3);
            }
        }

        if (round == 4)
        {
            if (_spawnTime <= 0)
            {
                GameObject g = Services.GameManager.CreateGameObject(Services.PrefabDatabase.Actors[Random.Range(1,3)]);
                _wave.Add(g.GetComponent<Enemy>());
                //            g.transform.position = new Vector3(-10f, Random.Range(-5f, 5f), 0);
                _spawnTime = Random.Range(MIN_TIME_BEFORE_ENEMY_SPAWN_RD3, MAX_TIME_BEFORE_ENEMY_SPAWN_RD3);
            }
        }

    }

    public void ClearWave()
    {
        foreach (var enemy in _wave)
        {
            enemy.gameObject.SetActive(false);
        }
        _wave.Clear();
        Debug.Log("Wave count: " + _wave.Count);
    }

}
