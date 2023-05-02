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

    [Header("GUI Elements - Death Menu")]
    public GameObject DeathMenu;
    
    [Header("Variables - Energy")]
    public bool HasFireHeldDown = false;
    public float EnergyBarMaxWidth;
    public float MaxEnergy = 1000.0f;
    public float CurrentEnergy = 0.0f;
    public float EnergyMultiplier = 0.001f;

    public GameObject button;

    [Header("Variables - Related to OVR Input")]
    public OVRPlayerController OVRController;
    public float OldOVRAccelerationValue;

    [Header("Variables - Related to Reset")]
    public GameObject DefaultSpawnPoint;
    public AudioSource ResetAudio;

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
        OVRController = this.GetComponent<OVRPlayerController>();
        if (OVRController !=null)
        {
            OldOVRAccelerationValue = OVRController.Acceleration;

        }
    }

    public void Reset()
    {
        TotalCoins = 500;
        TotalFood = 500;

        healthScript = this.GetComponent<HealthScript>();
        healthScript.SetHealth(100.0f);

        healthScript.IsDead = false;
        IsShowingDeathMenu = false;


        //Reset the OVR controller values
        OVRController.Acceleration = OldOVRAccelerationValue;

        ResetAudio.Play();

        DeathMenu.SetActive(false); // Hide the death screen

        if(DefaultSpawnPoint != null)
        {
            this.gameObject.transform.position = DefaultSpawnPoint.transform.position;
            this.gameObject.transform.rotation = DefaultSpawnPoint.transform.rotation;
        }

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
        if (this.healthScript.IsDead)
        {
            if (!IsShowingDeathMenu)
            {
                //ShowMenu
                IsShowingDeathMenu = true;
            }
        }
        if (!this.healthScript.IsDead)
        {
            TotalFoodUI.text = TotalFood.ToString();
            TotalCoinsUI.text = TotalCoins.ToString();
            
            if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger) || Input.GetMouseButton(0))
            {
                CurrentEnergy = Mathf.Lerp(CurrentEnergy, 0.0f, EnergyMultiplier);
            }
        }
        EnergyBarForeground.rectTransform.sizeDelta = new Vector3((EnergyBarMaxWidth / MaxEnergy) * CurrentEnergy, EnergyBarBackground.rectTransform.sizeDelta.y);
        if(OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger) || Input.GetMouseButtonUp(0))
        {
            HasFireHeldDown = false;
        }        

        if(OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || Input.GetMouseButtonDown(1))
        {
            if (CurrentEnergy > 0)
            {
                GameObject instantiated_object = GameObject.Instantiate(RockPrefab, Camera.transform.position + (CameraFront.transform.position - Camera.transform.position ).normalized , Quaternion.identity);
                Rigidbody body = instantiated_object.GetComponent<Rigidbody>();

                body.AddForce((CameraFront.transform.position - Camera.transform.position).normalized * CurrentEnergy, ForceMode.Force);

            }

        }

        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            if (TotalFood - 5 > 0)
            {
                healthScript = this.GetComponent<HealthScript>();

                float CurrentHealth = healthScript.GetHealth();

                if (CurrentHealth < 100.0f)
                if (GlobalsManager.Haptics)
                {
                    StartCoroutine(VibratorEx(0.1f, 0.1f));
                }

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
            if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger) || Input.GetMouseButtonUp(0))
            {
                HasFireHeldDown = false;
            }

            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) || Input.GetMouseButtonDown(1))
            {
                if (CurrentEnergy > 0)
                {
                    GameObject instantiated_object = GameObject.Instantiate(RockPrefab, Camera.transform.position + (CameraFront.transform.position - Camera.transform.position).normalized, Quaternion.identity);
                    Rigidbody body = instantiated_object.GetComponent<Rigidbody>();

                    body.AddForce((CameraFront.transform.position - Camera.transform.position).normalized * CurrentEnergy, ForceMode.Force);

                }

            }

            if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                if (TotalFood > 0)
                {
                    healthScript = this.GetComponent<HealthScript>();

                    float CurrentHealth = healthScript.GetHealth();

                    if (CurrentHealth < 100.0f)
                    {
                        TotalFood -= 5;
                        CurrentHealth += 5;

                        healthScript.SetHealth(CurrentHealth);
                    }
                }
            }
        }
        else
        {
            if(IsShowingDeathMenu)
            {
                DeathMenu.SetActive(true);
                OVRController.Acceleration = 0;
            }
        }
        if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.R))
        {
            if (GlobalsManager.Haptics)
            {
                StartCoroutine(Vibrator());
            }
            Reset();
        }

        if (OVRInput.Get(OVRInput.Touch.SecondaryThumbRest))
        {
            if (GlobalsManager.Haptics)
            {
                StartCoroutine(Vibrator());
            }
            if (TotalCoins - 5 > 0)
            {
                TotalCoins -= 5;
                TotalFood += 5;
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
        this.TotalCoins += TotalCoins;
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
