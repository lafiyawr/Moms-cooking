using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.XR.WSA;

public class GameScene : Scene
{
//    private GameObject 
    // Start is called before the first frame update
    private GameObject _threshold;
    [SerializeField]public float _roundTimeInSeconds;
    private GameObject _player;
    private Camera _cam;
    private int _currentRound = 1;
    private const float ROUND_TIME = 60;

    private FSM<GameScene> _gameStates;

    void Start()
    {
        _gameStates = new FSM<GameScene>(this);
        _gameStates.TransitionTo<RoundState>();
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
            //play cutscene sequence.
            //oncomplete, TransitionTo<RoundState>
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
    
    private class RoundState : EmptyGameState
    {
        private float _timeLeftInRound;
        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void Update()
        {
            base.Update();
            Context.CountdownRoundTime();
            Services.EnemyManager.EmitWave(Context._currentRound);
            if (_timeLeftInRound <= 0)
            {
                TransitionTo<GetReadyForNextRoundState>();
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

    
    
    private class GetReadyForNextRoundState : FSM<GameScene>.State
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Context._currentRound++;
            if (Context._currentRound == 1)
            {
                Debug.LogError("WRONG STATE FRIEND!");
                TransitionTo<RoundStartState>();
            }
            if (Context._currentRound == 2)
            {
                //play RAT cutscene
            } else if (Context._currentRound == 3)
            {
                //play cockroach cutscene
            } else if (Context._currentRound == 4)
            {
                //play apocalypse cutscene
            }

            //play cutscene here
            //on cutscene complete: transition to RoundState again
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
}
