using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : Scene
{
//    private GameObject 
    // Start is called before the first frame update
    private GameObject _threshold;
    private GameObject _enemy;

    void Start()
    {
        Services.GameManager.Player = Services.GameManager.CreateGameObject(Services.PrefabDatabase.Actors[0]);
        Services.EnemyManager = FindObjectOfType<EnemyManager>();
    }

    // Update is called once per frame
}
