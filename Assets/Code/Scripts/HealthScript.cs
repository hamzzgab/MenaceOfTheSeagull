using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public float MaxHealth = 100.0f;
    public float Health = 100.0f;
    public bool IsDead = false;
    // Start is called before the first frame update

    public Image HealthBarBackground;
    public Image HealthBar;
    public float HealthBarMaxWidth;
    void Start()
    {
        this.Health = MaxHealth;
        this.HealthBarMaxWidth = HealthBarBackground.rectTransform.rect.width;
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
            if (HealthBar != null)
            {
                /*
                    MaxHealth -> CurrentHealth
                    BarWidth -> ?
                    BarWidth * CurrentHEalth / MaxHealth
                 */
                HealthBar.rectTransform.sizeDelta = new Vector2(
                    (this.HealthBarMaxWidth * this.Health) / this.MaxHealth,
                    HealthBarBackground.rectTransform.sizeDelta.y
                );
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
