using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]private List<Enemy> _wave;

    private const int NUM_ENEMIES_IN_WAVE = 20;
    private const int MAX_TIME_BEFORE_ENEMY_SPAWN = 5;
    private const int MIN_TIME_BEFORE_ENEMY_SPAWN = 1;
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

    public void EmitWave(float time)
    {
        _spawnTime -= Time.deltaTime;
        if (_spawnTime <= 0)
        {
            GameObject g = Services.GameManager.CreateGameObject(Services.PrefabDatabase.Actors[1]);
            _wave.Add(g.GetComponent<Enemy>());
//            g.transform.position = new Vector3(-10f, Random.Range(-5f, 5f), 0);
            _spawnTime = Random.Range(MIN_TIME_BEFORE_ENEMY_SPAWN, MAX_TIME_BEFORE_ENEMY_SPAWN);
        }
    }

}
