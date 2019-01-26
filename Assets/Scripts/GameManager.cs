using System.Collections;
using System.Collections.Generic;
using ProBuilder2.Common;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject _playerGameObject;
    private Player _player;
    private GameObject _titleScene;
    private GameObject _gameScene;
    private GameObject _endScene;
    private GameObject[] _scenes;
    public GameObject[] Scenes => _scenes;
    private GameObject _currentScene;

    public GameObject CurrentScene
    {
        get { return _currentScene; }
        set { _currentScene = value; }
    }

    private FSM<GameManager> _sceneFsm;
    
       
    void Awake(){
//        Services.Prefabs = Resources.Load<PrefabDB>("Prefabs/PrefabDB");
        Services.GameManager = this;
        Services.PrefabDatabase = Resources.Load<PrefabDatabase>("Prefabs/PrefabDatabase");
    }

    void Start()
    {
        _playerGameObject = CreateGameObject(Services.PrefabDatabase.Actors[0]);
//        _gameScene.SetActive(true);
//        _endScene.SetActive(false);
    }

    public GameObject CreateGameObject(GameObject someGameObject)
    {
        GameObject g = Instantiate(someGameObject, Vector3.zero, Quaternion.identity);
        return g;
    }

    // Update is called once per frame
    void Update()
    {
    }

}
