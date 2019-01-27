using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.XR.WSA;
using DG.Tweening;

public class GameScene : Scene
{
//    private GameObject 
    // Start is called before the first frame update
    private GameObject _threshold;
    [SerializeField]public float _roundTimeInSeconds;
    private GameObject _player;
    private Camera _cam;
    private int _currentRound = 1;

    public int CurrentRound => _currentRound;

    private const float ROUND_TIME = 10;

    private FSM<GameScene> _gameStates;

    void Start()
    {
        _gameStates = new FSM<GameScene>(this);
        _gameStates.TransitionTo<RoundStartState>();
        _cam = Camera.main;
        _player = Services.GameManager.CreateGameObject(Services.PrefabDatabase.Actors[0]);
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
            Debug.Log("Playing ROUNDSTART cutscene!");
            //play cutscene sequence.
            //oncomplete, TransitionTo<RoundState>
        }

        public override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                TransitionTo<RoundState>();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
    
    private class RoundState : EmptyGameState
    {
        private float _timeLeftInRound;
        public override void OnEnter()
        {
            base.OnEnter();
            Context._roundTimeInSeconds = ROUND_TIME;
            Debug.Log("You are in round " + Context._currentRound);
        }

        public override void Update()
        {
            base.Update();
            _timeLeftInRound = Context.CountdownRoundTime();
            Services.EnemyManager.EmitWave(Context._currentRound);
            if (Context._currentRound < 5)
            {
                if (_timeLeftInRound <= 0)
                {
                    TransitionTo<GetReadyForNextRoundState>();
                } 
            }
            else
            {
                if (_timeLeftInRound <= 0)
                {
                    TransitionTo<WinState>();
                } 
            }

        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("Kill all animals!");
        }
    }
 
    private class GetReadyForNextRoundState : FSM<GameScene>.State
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("GET READY FOR NEXT ROUND STATE!");
            Context._currentRound++;
            if (Context._currentRound == 1)
            {
                Debug.LogError("WRONG STATE FRIEND!");
            }
            if (Context._currentRound == 2)
            {
                //play RAT cutscene
                Debug.Log("Playing RAT cutscene");

            } else if (Context._currentRound == 3)
            {
                //play cockroach cutscene
                Debug.Log("Playing COCKROACH cutscene");
            } else if (Context._currentRound == 4)
            {
                //play apocalypse cutscene
                Debug.Log("Playing APOCALYPSE cutscene");
            }
        }

        public override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                TransitionTo<RoundState>();
            }
        }

        public override void OnExit()
        {
            base.OnExit();

        }
    }
    
    private class LoseState : EmptyGameState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            //play some cutscene here; then OnComplete=>TransitionTo<RoundState>
        }

        public override void Update()
        {
            base.Update();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
    
    private class WinState : EmptyGameState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            
        }

        public override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("RESTART!");
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
