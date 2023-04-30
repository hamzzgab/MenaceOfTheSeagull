using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float Health = 100.0f;
    public bool IsDead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsDead)
        {
            if(Health > 0.0f)
            {
                IsDead = false;
            }
            else
            {
                IsDead = true;
            }
        }
    }

    public float GetHealth()
    {
        return this.Health;
    }
    public void SetHealth(float value)
    {
        this.Health = value;
    }
    public void GiveDamage(float damageAmount)
    {
        if (!this.IsDead)
        {
            this.Health -= damageAmount;
        }
    }
    
}
