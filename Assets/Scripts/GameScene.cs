using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Timers;
using UnityEngine;
using UnityEngine.XR.WSA;
using DG.Tweening;
using Vector3 = UnityEngine.Vector3;

public class GameScene : Scene
{
//    private GameObject 
    // Start is called before the first frame update
    private FSM<GameScene> _gameStates;
    [SerializeField]private Threshold _threshold;
    private GameObject _player;
    private Camera _cam;
    [SerializeField] private GameObject _winMsg;
    [SerializeField] private GameObject _loseMsg;
    [SerializeField] private List<CutScene> _cutScenes;
    private float _roundTimeInSeconds;
    private int _currentRound = 1;
    public int CurrentRound => _currentRound;
    private const float ROUND_TIME = 1;
    private bool _isPlayerVictorious;
    private bool _isPlayerDead;
    public bool IsPlayerVictorious => _isPlayerVictorious;


    void Start()
    {
        _gameStates = new FSM<GameScene>(this);
        _gameStates.TransitionTo<CutsceneState>();
        _cam = Camera.main;
        _player = Services.GameManager.CreateGameObject(Services.PrefabDatabase.Actors[0]);
        Services.GameManager.Player = _player;
        _player.transform.position = _cam.ScreenToWorldPoint(new Vector3(_cam.pixelWidth * 0.90f, Random.Range(0, Screen.height-10), _cam.nearClipPlane));
        Services.EnemyManager = FindObjectOfType<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _gameStates.Update();
    }

    public float CountdownRoundTime()
    {
        _roundTimeInSeconds -= Time.deltaTime;
        return _roundTimeInSeconds;
    }

    private class EmptyGameState : FSM<GameScene>.State
    {
    }
    
    private class RoundStartState : EmptyGameState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Context._background.SetActive(false);
            Context._cutScenes[0].gameObject.SetActive(true);
            //play cutscene sequence.
            //oncomplete, TransitionTo<RoundState>
        }

        public override void Update()
        {
            base.Update();
            if (Context._cutScenes[0].IsCutSceneComplete)
            {
                TransitionTo<RoundState>();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            Context._cutScenes[0].gameObject.SetActive(false);
        }
    }
    
    private class RoundState : EmptyGameState
    {
        private float _timeLeftInRound;
        public override void OnEnter()
        {
            base.OnEnter();
            Context._background.SetActive(true);
            Context._player.SetActive(true);
            Context._threshold.gameObject.SetActive(true);
            Context._roundTimeInSeconds = ROUND_TIME;
            Debug.Log("You are in round " + Context._currentRound);
        }

        public override void Update()
        {
            base.Update();
            _timeLeftInRound = Context.CountdownRoundTime();
            Services.EnemyManager.EmitWave(Context._currentRound);
            if (Context._currentRound < Context._cutScenes.Count)
            {
                if (_timeLeftInRound <= 0)
                {
                    TransitionTo<CutsceneState>();
                } 
            }
            else
            {
                if (_timeLeftInRound <= 0)
                {
                    TransitionTo<WinState>();
                } 
            }

            if (Context._threshold.HitReceived)
            {
                TransitionTo<LoseState>();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            Context._threshold.gameObject.SetActive(false);
            Services.EnemyManager.ClearWave();
            if(Context._currentRound<Context._cutScenes.Count)
                Context._currentRound++;
//            Services.EnemyManager.
        }
    }
 
    private class CutsceneState : FSM<GameScene>.State
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("cutscene state");
//            Services.GameManager.Player.SetActive(false);
            Context._player.SetActive(false);
            Context._background.SetActive(false);
            Context._cutScenes[Context._currentRound-1].gameObject.SetActive(true);
        }

        public override void Update()
        {
            base.Update();
            if (Context._cutScenes[Context._currentRound - 1].IsCutSceneComplete)
                TransitionTo<RoundState>();
        }

        public override void OnExit()
        {
            base.OnExit();
            Context._cutScenes[Context._currentRound - 1].gameObject.SetActive(false);
        }
    }
    
    private class LoseState : EmptyGameState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            //play some cutscene here; then OnComplete=>TransitionTo<RoundState>
            Context._loseMsg.SetActive(true);
        }

        public override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Return))
                TransitionTo<RoundState>();
        }

        public override void OnExit()
        {
            base.OnExit();
            Context._loseMsg.SetActive(false);
        }
    }
    
    private class WinState : EmptyGameState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("We are in win stateEEEEEEEE!");
            Context._winMsg.SetActive(true);
            Context._background.SetActive(false);
        }

        public override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Context._isPlayerVictorious = true;
                Context._player.SetActive(false);
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }

    private class TestState : EmptyGameState
    {
    }
}
