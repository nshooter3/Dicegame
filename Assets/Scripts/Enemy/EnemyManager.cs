using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> enemies;

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
}
