using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class GameScene : Scene
{
//    private GameObject 
    // Start is called before the first frame update
    private GameObject _threshold;
    [SerializeField]public float _roundTimeInSeconds;
    private GameObject _player;
    private Camera _cam;

    void Start()
    {
        _cam = Camera.main;
        _player = Services.GameManager.CreateGameObject(Services.PrefabDatabase.Actors[0]);
        _player.transform.position = _cam.ScreenToWorldPoint(new Vector3(_cam.pixelWidth * 0.90f, Random.Range(0, Screen.height-10), _cam.nearClipPlane));
        Services.EnemyManager = FindObjectOfType<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Services.EnemyManager.EmitWave(CountdownRoundTime());
    }

    public float CountdownRoundTime()
    {
        _roundTimeInSeconds -= Time.deltaTime;
        return _roundTimeInSeconds;
    }
}
