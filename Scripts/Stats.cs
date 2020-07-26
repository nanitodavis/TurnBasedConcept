using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public string name;
    public int lvl;
    public int maxHealth;
    public int currentHealth;
    public int maxSpirit;
    public int currentSpirit;
    public int attack;
    public int defense;

    public bool TakeDamage(int damage)
    {
        currentHealth -= (damage - defense);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RecoverHealth(int healthToRecover)
    {
        currentHealth += healthToRecover;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
