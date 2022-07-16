using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBar;
    [SerializeField]
    private EnergyPipManager pipManager;
    [SerializeField]
    private StatsUI stats;

    public void SetHealth(int curHealth, int maxHealth, bool tookDamage = true)
    {
        healthBar.SetHealth(curHealth, maxHealth, tookDamage);
    }

    public void SetEnergy(int curEnergy, int maxEnergy)
    {
        pipManager.SetMaxPips(maxEnergy);
        pipManager.SetCurrentPips(curEnergy);
    }

    public void SetAttack(int attack)
    {
        stats.SetAttack(attack);
    }

    public void SetDefense(int defense)
    {
        stats.SetDefense(defense);
    }
}
