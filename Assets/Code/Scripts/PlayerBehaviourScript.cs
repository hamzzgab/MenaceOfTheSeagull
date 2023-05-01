using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerBehaviourScript : MonoBehaviour
{


    [Header("Variables")]
    public HealthScript healthScript;
    public GameObject RockPrefab;
    public bool IsShowingDeathMenu;
    [Header("Camera Related Settings")]
    public GameObject Camera;
    public GameObject CameraFront;
    [Header("Inventory - Items")]
    public int TotalCoins = 0;
    public int TotalFood = 0;
    [Header("GUI Elements - Inventory")]
    public TMP_Text TotalFoodUI;
    public TMP_Text TotalCoinsUI;
    [Header("GUI Elements - Health bar")]
    public Image HealthBar;
    public Image HealthBarBackground;
    [Header("GUI Elements - Energy bar")]
    public Image EnergyBarBackground;
    public Image EnergyBarForeground;
    
    [Header("Variables - Energy")]
    public bool HasFireHeldDown = false;
    public float EnergyBarMaxWidth;
    public float MaxEnergy = 1000.0f;
    public float CurrentEnergy = 0.0f;
    public float EnergyMultiplier = 0.001f;

    public GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        healthScript = this.GetComponent<HealthScript>();
        if(healthScript == null)
        {
            healthScript = this.AddComponent<HealthScript>();
            
        }
        healthScript.HealthBar = this.HealthBar;
        healthScript.HealthBarBackground = this.HealthBarBackground;
        EnergyBarMaxWidth = EnergyBarBackground.rectTransform.rect.width;
        GlobalsManager.Player = this.gameObject;
    }

    public IEnumerator VibratorEx(float waitSec, float intensity)
    {
        OVRInput.SetControllerVibration(1f, intensity, OVRInput.Controller.LHand);
        yield return new WaitForSeconds(waitSec);
        OVRInput.SetControllerVibration(0f, 0f);
    }

    public IEnumerator Vibrator()
    {
        OVRInput.SetControllerVibration(1f, 0.1f);
        yield return new WaitForSeconds(0.1f);
        OVRInput.SetControllerVibration(0f, 0f);
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
        if(OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            StartCoroutine(VibratorEx(0.1f, 0.1f));
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
        if(OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
        {
            HasFireHeldDown = false;
        }        

        if(OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            if (CurrentEnergy > 0)
            {
                GameObject instantiated_object = GameObject.Instantiate(RockPrefab, Camera.transform.position + (CameraFront.transform.position - Camera.transform.position ).normalized , Quaternion.identity);
                Rigidbody body = instantiated_object.GetComponent<Rigidbody>();

                body.AddForce((CameraFront.transform.position - Camera.transform.position).normalized * CurrentEnergy, ForceMode.Force);

            }

        }


        TextMeshPro tmp = button.GetComponent<TextMeshPro>();
        tmp.text = CurrentEnergy.ToString();
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
