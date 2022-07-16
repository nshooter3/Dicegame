using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int initAttack = 0, initDefense = 0;
    [SerializeField]
    private List<EnemyAction> enemyActions;
    [SerializeField]
    private HealthBar healthBar;
    [SerializeField]
    private EnemyIntentManager intent;

    private int curHealth;
    public bool dead;
    private int actionIndex = 0;
    private int curAttack, curDefense;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        curAttack = initAttack;
        curDefense = initDefense;
        healthBar.SetHealth(curHealth, maxHealth, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIntent()
    {
        switch (enemyActions[actionIndex].enemyActionType)
        {
            case EnemyAction.EnemyActionType.Attack:
                intent.SetAttack(enemyActions[actionIndex].amount, enemyActions[actionIndex].numTimes);
                break;
            case EnemyAction.EnemyActionType.StrengthBuff:
                intent.SetStrengthBuff(enemyActions[actionIndex].amount, enemyActions[actionIndex].enemyBuffType == EnemyAction.EnemyBuffType.All);
                break;
            case EnemyAction.EnemyActionType.DefenseBuff:
                intent.SetDefenseBuff(enemyActions[actionIndex].amount, enemyActions[actionIndex].enemyBuffType == EnemyAction.EnemyBuffType.All);
                break;
        }
    }

    public IEnumerator PerformAction()
    {
        switch (enemyActions[actionIndex].enemyActionType)
        {
            case EnemyAction.EnemyActionType.Attack:
                yield return StartCoroutine(Attack(enemyActions[actionIndex].amount, enemyActions[actionIndex].numTimes));
                break;
            case EnemyAction.EnemyActionType.StrengthBuff:
                yield return StartCoroutine(StrengthBuff(enemyActions[actionIndex].amount, enemyActions[actionIndex].enemyBuffType == EnemyAction.EnemyBuffType.All));
                break;
            case EnemyAction.EnemyActionType.DefenseBuff:
                yield return StartCoroutine(DefenseBuff(enemyActions[actionIndex].amount, enemyActions[actionIndex].enemyBuffType == EnemyAction.EnemyBuffType.All));
                break;
        }
        intent.HideAll();
        actionIndex = (actionIndex + 1) % enemyActions.Count;
    }

    private IEnumerator Attack(int damage, int numTimes, float betweenAttackDelay = 0.3f, float postAttackDelay = 1f)
    {
        for (int i = 0; i < numTimes; i++)
        {
            GameManager.instance.playerTurnManager.TakeDamage(damage);
            yield return new WaitForSeconds(betweenAttackDelay);
        }
        yield return new WaitForSeconds(postAttackDelay);
    }

    public IEnumerator StrengthBuff(int amount, bool isAll, float postBuffDelay = 1f)
    {
        if (!isAll)
        {
            curAttack += amount;
        }
        else
        {
            curAttack += amount;
            //TODO: BuffAllEnemies
        }
        yield return new WaitForSeconds(postBuffDelay);
    }

    public IEnumerator DefenseBuff(int amount, bool isAll, float postBuffDelay = 1f)
    {
        if (!isAll)
        {
            curDefense += amount;
        }
        else
        {
            curDefense += amount;
            //TODO: BuffAllEnemies
        }
        yield return new WaitForSeconds(postBuffDelay);
    }

    public void TakeDamage(int damage)
    {
        curHealth = Mathf.Max(0, curHealth - damage);
        Debug.Log("Enemy take " + damage + " damage! Cur health: " + curHealth);
        healthBar.SetHealth(curHealth, maxHealth);
        if (curHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        dead = true;
    }
}
