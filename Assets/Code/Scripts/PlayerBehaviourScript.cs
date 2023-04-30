using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerBehaviourScript : MonoBehaviour
{



    public HealthScript healthScript;

    public bool IsShowingDeathMenu;

    public GameObject[] Hearts;


    [Header("Inventory - Items")]
    public int TotalCoins = 0;
    public int TotalFood = 0;
    [Header("GUI Elements - Inventory")]
    public TMP_Text TotalFoodUI;
    public TMP_Text TotalCoinsUI;



    public GameObject Camera;
    public GameObject CameraFront;
    public bool HasFireHeldDown = false;

    public Image EnergyBarBackground;
    public Image EnergyBarForeground;
    public float EnergyBarMaxWidth;
    public float MaxEnergy = 1000.0f;
    public float CurrentEnergy = 0.0f;
    public float EnergyMultiplier = 0.001f;

    public GameObject DummyRockPrefab;
    //public Vector3 DummyRockScale = new Vector3(0.005f, 0.005f, 0.005f);

    

    // Start is called before the first frame update
    void Start()
    {
        healthScript = this.GetComponent<HealthScript>();
        if(healthScript == null)
        {
            healthScript = this.AddComponent<HealthScript>();
        }

        EnergyBarMaxWidth = EnergyBarBackground.rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        TotalFoodUI.text = TotalFood.ToString();
        TotalCoinsUI.text = TotalCoins.ToString();
        if(this.healthScript.IsDead)
        {
            if(!IsShowingDeathMenu)
            {
                //ShowMenu
                IsShowingDeathMenu = true;
            }
        }
        if(Input.GetMouseButton(0))
        {
            HasFireHeldDown = true;
            CurrentEnergy = Mathf.Lerp(CurrentEnergy, MaxEnergy, EnergyMultiplier);
        }
        else
        {
            HasFireHeldDown = false;
            if (CurrentEnergy > 0)
            {
                CurrentEnergy = Mathf.Lerp(CurrentEnergy, 0.0f, EnergyMultiplier);
            }
        }
        EnergyBarForeground.rectTransform.sizeDelta = new Vector3((EnergyBarMaxWidth / MaxEnergy) * CurrentEnergy, EnergyBarBackground.rectTransform.sizeDelta.y);
        if(Input.GetMouseButtonUp(0))
        {
            HasFireHeldDown = false;
        }
        if(Input.GetMouseButtonDown(1))
        {
            if (CurrentEnergy > 0)
            {
                GameObject instantiated_object = GameObject.Instantiate(DummyRockPrefab, Camera.transform.position, Quaternion.identity);
                Rigidbody body = instantiated_object.GetComponent<Rigidbody>();

                body.AddForce((CameraFront.transform.position - Camera.transform.position).normalized * CurrentEnergy, ForceMode.Force);

            }

        }
    }

    public void AddFoodItem(int TotalFoodItem)
    {
        this.TotalFood += TotalFoodItem;
    }
    public void AddCoins(int TotalCoins)
    {
        this.TotalFood += TotalCoins;
    }
    public void DecreaseFoodFromInventory(int TotalFoodItems)
    {
        if(this.TotalFood > 0)
        {
            this.TotalFood -= TotalFoodItems;
            if(this.TotalFood < 0)
            {
                this.TotalFood = 0;
            }
        }
    }
    public void DecreaseCoinsFromInventory(int TotalCoins)
    {
        if (this.TotalCoins > 0)
        {
            this.TotalCoins-= TotalCoins;
            if (this.TotalCoins < 0)
            {
                this.TotalCoins = 0;
            }
        }
    }
}
