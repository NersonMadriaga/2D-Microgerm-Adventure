using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private bool dev = false;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Health healthBar;
    [SerializeField] private int currentHealth;

    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (dev)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                TakeDamage(20);
                Debug.Log("R Button");
            }
            
        }

        if(currentHealth > maxHealth)
        {
            maxHealth = currentHealth;
            healthBar.SetMaxHealth(maxHealth);
        }

        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
            GameManager.Instance.GameOver();
        }

        healthBar.SetHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
