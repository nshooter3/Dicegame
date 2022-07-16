using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBar;
    [SerializeField]
    private EnergyPipManager pipManager;

    public void SetHealth(int curHealth, int maxHealth, bool tookDamage = true)
    {
        healthBar.SetHealth(curHealth, maxHealth, tookDamage);
    }

    public void SetEnergy(int curEnergy, int maxEnergy)
    {
        pipManager.SetMaxPips(maxEnergy);
        pipManager.SetCurrentPips(curEnergy);
    }
}
