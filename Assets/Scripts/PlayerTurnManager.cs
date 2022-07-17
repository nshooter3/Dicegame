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
    private int curAttack, curDefense;
    private bool attackWasCrit;
    private Enemy targetedEnemy;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        curAttack = initAttack;
        curDefense = initDefense;
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
        StartCoroutine(DamageEnemyCoroutine(result));
    }

    private IEnumerator DamageEnemyCoroutine(int result)
    {
        if (result == 1)
        {
            BuffRandomStat();
            yield return new WaitForSeconds(1f);
        }
        if (attackWasCrit)
        {
            //TODO: crit presentation.
            result *= 2;
        }
        GameManager.instance.enemyManager.DamageTargetedEnemy(targetedEnemy, result + curAttack);
        AttackExit();
    }

    public void BuffRandomStat()
    {
        if (Random.Range(0, 2) == 1)
        {
            BuffAttack(1);
        }
        else
        {
            BuffDefense(1);
        }
    }

    public void BuffAttack(int amount)
    {
        curAttack += amount;
        playerUI.SetAttack(curAttack);
    }

    public void BuffDefense(int amount)
    {
        curDefense += amount;
        playerUI.SetDefense(curDefense);
    }

    public void TakeDamage(int damage)
    {
        damage = Mathf.Max(damage - curDefense, 0);
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
        PickDiceExit();
    }

    void PickDiceUpdate()
    {
        
    }

    void PickDiceExit()
    {
        PickEnemyEnter();
    }

    // ***********************************
    // PICK ENEMY FUNCTIONS
    // ***********************************
    void PickEnemyEnter()
    {
        playerTurnState = PlayerTurnState.PickEnemy;
        playerUI.EnemySelectorSetActive();
    }

    void PickEnemyUpdate()
    {
        targetedEnemy = null;
        if (Input.GetMouseButtonDown(0) && GameManager.instance.clickManager.CheckForSelectedEnemy(out targetedEnemy))
        {
            playerUI.EnemySelectorSelectTarget();
            PickEnemyExit();
        }
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
        curEnergy--;
        playerUI.SetEnergy(curEnergy, maxEnergy);
        playerTurnState = PlayerTurnState.Attack;
        GameManager.instance.diceManager.Roll(OnDiceEndRoll, GameManager.instance.diceManager.GetRandomDieType(), out attackWasCrit);
    }

    void AttackUpdate()
    {
        //TODO: Roll a dice.
    }

    void AttackExit()
    {
        if (curEnergy > 0)
        {
            PickDiceEnter();
        }
        else
        {
            EnterInactive();
        }
    }

    // ***********************************
    // INACTIVE FUNCTIONS
    // ***********************************

    void EnterInactive()
    {
        playerTurnState = PlayerTurnState.Inactive;
    }
}
