using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ProBuilder2.Common;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private FSM<SceneManager> _fsm;

    [SerializeField]private List<GameObject> _scenes = new List<GameObject>();

    private GameObject _currentScene;
    // Start is called before the first frame update
    void Start()
    {
        
        _fsm = new FSM<SceneManager>(this);
        _fsm.TransitionTo<TitleState>();
        for (int i = 0; i < Services.PrefabDatabase.Scenes.Length; i++)
        {
            GameObject g = Services.GameManager.CreateGameObject(Services.PrefabDatabase.Scenes[i]);
            _scenes.Add(g);
        }
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

    private class NeutralState : FSM<SceneManager>.State
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
        public override void OnEnter()
        {
            base.OnEnter();
            Context._currentScene = Context._scenes[1];
            Context.SetOnlyCurrentSceneActive(Context._scenes);
            Debug.Log("Gameplay State!");
      
        }

        public override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Return))
            {
                TransitionTo<EndState>();
            }
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
                TransitionTo<TitleState>();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
