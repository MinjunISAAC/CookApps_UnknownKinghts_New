// ----- C#
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Util.ForStateMachine;
using InGame.ForState;

public class Game_StateMachine : SimpleStateMachine<EGameState>
{
    // --------------------------------------------------
    // Singleton
    // --------------------------------------------------
    // ----- Constructor
    private Game_StateMachine() { }

    // ----- Static Variables
    private static Game_StateMachine _instance = null;

    // ----- Property
    public static Game_StateMachine Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = new Game_StateMachine();
                _instance._InitSingleton();
            }

            return _instance;
        }
    }

    // --------------------------------------------------
    // Functions - Nomal
    // --------------------------------------------------
    private class CoroutineExecoutor : MonoBehaviour { }
    private void _InitSingleton()
    {
        if (null == _coroutineExecutor)
        {
            GameObject executorGameObject = new GameObject("CoroutineExecutor");

            _coroutineExecutor = executorGameObject.AddComponent<CoroutineExecoutor>();
            if (null == _coroutineExecutor)
            {
                Debug.LogError("[Game_StateMachine._InitSingleton] Coroutine 실행자가 생성되지 않았습니다.");
                return;
            }
            UnityEngine.Object.DontDestroyOnLoad(executorGameObject);
        }

        OnInit
        (
            new Dictionary<EGameState, SimpleState<EGameState>>()
            {
                { EGameState.Village      , new State_Village()       },
                { EGameState.ChapterSelect, new State_ChapterSelect() },
                /*
                    { EStateType.BuildDeck,   new State_BuildDeck()   },
                    { EStateType.Battle,      new State_Battle()      },
                    { EStateType.Result,      new State_Result()      },
                */
            },
            _coroutineExecutor,
            null
        );
    }
}