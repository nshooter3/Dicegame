using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EnemyManager enemyManager;
    public PlayerTurnManager playerTurnManager;
    public DiceManager diceManager;

    public enum BattleState { Intro, PlayerTurn, EnemyTurn, Victory, Defeat };
    public BattleState battleState = BattleState.Intro;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        IntroEnter();
    }

    // Update is called once per frame
    void Update()
    {
        switch (battleState)
        {
            case BattleState.Intro:
                IntroUpdate();
                break;
            case BattleState.PlayerTurn:
                PlayerTurnUpdate();
                break;
            case BattleState.EnemyTurn:
                EnemyTurnUpdate();
                break;
            case BattleState.Victory:
                VictoryUpdate();
                break;
            case BattleState.Defeat:
                DefeatUpdate();
                break;
        }
    }

    // ***********************************
    // INTRO FUNCTIONS
    // ***********************************
    void IntroEnter()
    {
        IntroExit();
    }

    void IntroUpdate()
    {

    }

    void IntroExit()
    {
        PlayerTurnEnter();
    }

    // ***********************************
    // PLAYER TURN FUNCTIONS
    // ***********************************
    void PlayerTurnEnter()
    {
        battleState = BattleState.PlayerTurn;
        playerTurnManager.StartPlayerTurn();
    }

    void PlayerTurnUpdate()
    {
        playerTurnManager.OnUpdate();
        if (playerTurnManager.IsDone())
        {
            PlayerTurnExit();
        }
    }

    void PlayerTurnExit()
    {
        EnemyTurnEnter();
    }

    // ***********************************
    // ENEMY TURN FUNCTIONS
    // ***********************************
    void EnemyTurnEnter()
    {
        Debug.Log("ENEMY ATTACK");
        battleState = BattleState.EnemyTurn;
        EnemyTurnExit();
    }

    void EnemyTurnUpdate()
    {

    }

    void EnemyTurnExit()
    {
        PlayerTurnEnter();
    }

    // ***********************************
    // VICTORY FUNCTIONS
    // ***********************************
    void VictoryEnter()
    {

    }

    void VictoryUpdate()
    {

    }

    void VictoryExit()
    {

    }

    // ***********************************
    // DEFEAT FUNCTIONS
    // ***********************************
    void DefeatEnter()
    {

    }

    void DefeatUpdate()
    {

    }

    void DefeatExit()
    {

    }
}
