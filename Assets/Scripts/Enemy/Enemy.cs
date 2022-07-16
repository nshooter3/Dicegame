using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private HealthBar healthBar;

    private int curHealth;
    public bool dead;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
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
