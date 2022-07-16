using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnManager : MonoBehaviour
{
    public enum PlayerTurnState { Intro, PickDice, PickEnemy, Attack, ShuffleBag, Inactive};
    public PlayerTurnState playerTurnState = PlayerTurnState.Intro;

    float curEnergy, maxEnergy = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnUpdate()
    {
        switch (playerTurnState)
        {
            case PlayerTurnState.Intro:
                IntroUpdate();
                break;
            case PlayerTurnState.PickDice:
                PickDiceUpdate();
                break;
            case PlayerTurnState.PickEnemy:
                PickEnemyUpdate();
                break;
            case PlayerTurnState.Attack:
                AttackUpdate();
                break;
            case PlayerTurnState.ShuffleBag:
                ShuffleBagUpdate();
                break;
        }
    }

    public void StartPlayerTurn()
    {
        Debug.Log("START PLAYER TURN");
        IntroEnter();
    }

    public bool IsDone()
    {
        return playerTurnState == PlayerTurnState.Inactive;
    }

    public void OnDiceEndRoll(int result)
    {
        GameManager.instance.enemyManager.DamageTargetedEnemy(0, result);
        AttackExit();
    }

    // ***********************************
    // INTRO FUNCTIONS
    // ***********************************
    void IntroEnter()
    {
        curEnergy = maxEnergy;
        IntroExit();
    }

    void IntroUpdate()
    {

    }

    void IntroExit()
    {
        PickDiceEnter();
    }

    // ***********************************
    // PICK DICE FUNCTIONS
    // ***********************************
    void PickDiceEnter()
    {
        playerTurnState = PlayerTurnState.PickDice;
    }

    void PickDiceUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PickDiceExit();
        }
    }

    void PickDiceExit()
    {
        PickEnemyEnter();;
    }

    // ***********************************
    // PICK ENEMY FUNCTIONS
    // ***********************************
    void PickEnemyEnter()
    {
        playerTurnState = PlayerTurnState.PickEnemy;
        PickEnemyExit();
    }

    void PickEnemyUpdate()
    {
        
    }

    void PickEnemyExit()
    {
        AttackEnter();
    }

    // ***********************************
    // ATTACK FUNCTIONS
    // ***********************************
    void AttackEnter()
    {
        Debug.Log("ATTACK!");
        playerTurnState = PlayerTurnState.Attack;
        GameManager.instance.diceManager.Roll(OnDiceEndRoll);
    }

    void AttackUpdate()
    {
        //TODO: Roll a dice.
    }

    void AttackExit()
    {
        curEnergy--;
        if (curEnergy > 0)
        {
            PickDiceEnter();
        }
        else
        {
            EnterInactive();
        }
        Debug.Log("ENERGY LEFT: " + curEnergy);
    }

    // ***********************************
    // ATTACK FUNCTIONS
    // ***********************************
    void ShuffleBagEnter()
    {

    }

    void ShuffleBagUpdate()
    {

    }

    void ShuffleBagExit()
    {

    }

    void EnterInactive()
    {
        playerTurnState = PlayerTurnState.Inactive;
    }
}
