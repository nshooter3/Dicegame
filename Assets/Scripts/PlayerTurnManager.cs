using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnManager : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int initAttack = 0, initDefense = 0;
    [SerializeField]
    private PlayerUI playerUI;

    public enum PlayerTurnState { Intro, PickDice, PickEnemy, Attack, Inactive};
    public PlayerTurnState playerTurnState = PlayerTurnState.Intro;

    private int curEnergy, maxEnergy = 3;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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
        }
    }

    // ***********************************
    // UTIL FUNCTIONS
    // ***********************************

    public void StartPlayerTurn()
    {
        Debug.Log("START PLAYER TURN");
        IntroEnter();
        playerUI.SetEnergy(maxEnergy, maxEnergy);
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

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        playerUI.SetHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //TODO: Die already scum
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
        playerUI.SetEnergy(curEnergy, maxEnergy);
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
    // INACTIVE FUNCTIONS
    // ***********************************

    void EnterInactive()
    {
        playerTurnState = PlayerTurnState.Inactive;
    }
}
