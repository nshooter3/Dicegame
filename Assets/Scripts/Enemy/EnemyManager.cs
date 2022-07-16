using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> enemies;

    public delegate void OnFinishAttack();
    public OnFinishAttack onFinishAttack;

    public void AllEnemiesShowIntent()
    {
        enemies.ForEach(p => p.SetIntent());
    }

    public void AllEnemiesAttack(OnFinishAttack callback)
    {
        onFinishAttack += callback;
        StartCoroutine(AllEnemiesAttackCoroutine());
    }

    private IEnumerator AllEnemiesAttackCoroutine(float postAttackDelay = 1f)
    {
        foreach (Enemy enemy in enemies)
        {
            yield return StartCoroutine(enemy.PerformAction());
        }
        yield return new WaitForSeconds(postAttackDelay);
        onFinishAttack();
        onFinishAttack = null;
    }

    public void DamageTargetedEnemy(int targetIndex, int damage)
    {
        enemies[targetIndex].TakeDamage(damage);
        ClearDeadEnemiesFromList();
    }

    public void DamageAllEnemies(int damage)
    {
        enemies.ForEach(p => p.TakeDamage(damage));
        ClearDeadEnemiesFromList();
    }

    void ClearDeadEnemiesFromList()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].dead)
            {
                enemies.RemoveAt(i);
                i--;
            }
        }
    }
}
