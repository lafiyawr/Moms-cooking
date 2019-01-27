using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using ProBuilder2.Common;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GgjSceneManager : MonoBehaviour
{
    private FSM<GgjSceneManager> _fsm;

    [SerializeField]private List<GameObject> _scenes = new List<GameObject>();

    private GameObject _titleScene;
    private GameObject _gameScene;
    private GameObject _endScene;

    public GameObject GameScene => _gameScene;
    public GameObject TitleScene => _titleScene;
    public GameObject EndScene => _endScene;

    private GameObject _currentScene;
    // Start is called before the first frame update
    void Start()
    {
        
        _fsm = new FSM<GgjSceneManager>(this);
        _fsm.TransitionTo<TitleState>();
        for (int i = 0; i < Services.PrefabDatabase.Scenes.Length; i++)
        {
            GameObject g = Services.GameManager.CreateGameObject(Services.PrefabDatabase.Scenes[i]);
            _scenes.Add(g);
        }

        _titleScene = _scenes[0];
        _gameScene = _scenes[1];
        _endScene = _scenes[2];
    }

    // Update is called once per frame 
    void Update()
    {
        _fsm.Update();
    }

    private void SetOnlyCurrentSceneActive(List<GameObject> scenes)
    {
        if (scenes.Count > 0)
        {
            foreach (var scene in scenes)
            {
                if (scene != _currentScene)
                {
                    scene.SetActive(false);
                }
                else
                {                        
                    scene.SetActive(true);
                }

            }
        }
    }

    private class NeutralState : FSM<GgjSceneManager>.State
    {
        
    }

    private class TitleState : NeutralState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Title state!");
            Context._currentScene = Context._scenes[0];
            Context.SetOnlyCurrentSceneActive(Context._scenes);
        }

        public override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Return))
            {
                TransitionTo<GameplayState>();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }

    private class GameplayState : NeutralState
    {
        private GameScene _gameScene;
        public override void OnEnter()
        {
            base.OnEnter();
            Context._currentScene = Context._scenes[1];
            _gameScene = Context._currentScene.GetComponent<GameScene>();
            Context.SetOnlyCurrentSceneActive(Context._scenes);
            Debug.Log("Gameplay State!");
        }

        public override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene("SampleScene");
            if (_gameScene.IsPlayerVictorious)
                TransitionTo<EndState>();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }

    private class EndState : NeutralState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Context._currentScene = Context._scenes[2];
            Context.SetOnlyCurrentSceneActive(Context._scenes);
            Debug.Log("End State!");
        }

        public override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
