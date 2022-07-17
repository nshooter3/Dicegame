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
    [SerializeField]
    private StatsSprites stats;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject enemyUI;
    [SerializeField]
    private Collider col;
    [SerializeField]
    private ParticleSystem attackBuffParticles, defenseBuffParticles, deathParticles;

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
                intent.SetAttack(enemyActions[actionIndex].amount + curAttack, enemyActions[actionIndex].numTimes);
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
    }

    public void IncrementActionIndex()
    {
        actionIndex = (actionIndex + 1) % enemyActions.Count;
    }

    private IEnumerator Attack(int damage, int numTimes)
    {
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < numTimes; i++)
        {
            GameManager.instance.playerTurnManager.TakeDamage(damage + curAttack);
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(1.0f);
    }

    public IEnumerator StrengthBuff(int amount, bool isAll)
    {
        anim.SetTrigger("Buff");
        yield return new WaitForSeconds(0.35f);
        attackBuffParticles.Play();
        if (!isAll)
        {
            curAttack += amount;
        }
        else
        {
            curAttack += amount;
            //TODO: BuffAllEnemies
        }
        GameManager.instance.enemyManager.AllEnemiesShowIntent();
        stats.SetAttack(curAttack);
        yield return new WaitForSeconds(1.0f);
    }

    public IEnumerator DefenseBuff(int amount, bool isAll)
    {
        anim.SetTrigger("Buff");
        yield return new WaitForSeconds(0.35f);
        defenseBuffParticles.Play();
        if (!isAll)
        {
            curDefense += amount;
        }
        else
        {
            curDefense += amount;
            //TODO: BuffAllEnemies
        }
        stats.SetDefense(curDefense);
        yield return new WaitForSeconds(1.0f);
    }

    public void TakeDamage(int damage)
    {
        anim.SetTrigger("Hurt");
        damage = Mathf.Max(damage - curDefense, 0);
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
        deathParticles.Play();
        col.enabled = false;
        enemyUI.SetActive(false);
        anim.SetTrigger("Die");
        dead = true;
    }
}
