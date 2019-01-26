using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : Scene
{
//    private GameObject 
    // Start is called before the first frame update
    private GameObject _threshold;

    void Start()
    {
        Services.GameManager.Player = Services.GameManager.CreateGameObject(Services.PrefabDatabase.Actors[0]);
        Services.GameManager.Player.transform.position = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
