using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBehaviourScript : MonoBehaviour
{

    

    public float Health = 100.0f;
    public float HealthRanges = 0.0f;
    public bool IsDead = false;
    public bool IsDeathMenuShown = false;
    public GameObject[] Hearts;


    [Header("Inventory - Items")]
    public int TotalCoins = 0;
    public int TotalFood = 0;
    [Header("GUI Elements - Inventory")]
    public TMP_Text TotalFoodUI;
    public TMP_Text TotalCoinsUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.IsDead)
        {
            if (this.Health > 0)
            {

            }
            else
            {
                this.IsDead = true;
            }
        }
        else
        {
            if(!this.IsDeathMenuShown)
            {
                this.IsDeathMenuShown = true;
                //Show the death menu for respawning and stuff like that...........
            }
        }
    }
    public void GiveDamage(float damage)
    {
        this.Health -= damage;
    }

    public void AddFoodItem(int TotalFoodItem)
    {
        this.TotalFood += TotalFoodItem;
    }
    public void AddCoins(int TotalCoins)
    {
        this.TotalFood += TotalCoins;
    }
}
