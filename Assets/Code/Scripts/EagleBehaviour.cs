using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EagleBehaviour : MonoBehaviour
{
    public HealthScript healthScript;
    public GameObject Player;
    public Animator EagleAnimator;

    public float DistanceRadar = 10.0f;
    public float DistanceAttack = 0.75f;
    public float LerpMultiplier = 0.1f;
    public bool IsActivated = false;

    public float DebugDistanceVariable = 0.0f;

    public float LastAttackTime = 0.0f;

    public float DifferenceInAttacks = 6.0f;

    public float[] DifferenceInAttackRange = new float[2];

    public float DamageSize = 1.0f;

    public bool HasPlayedDeathAnimation = false;

    // Start is called before the first frame update
    void Start()
    {
        EagleAnimator = GetComponent<Animator>();
        if (Player == null)
        {
            Debug.LogError("No player has been assigned!");
        }
        healthScript = this.GetComponent<HealthScript>();
        if (healthScript == null)
        {
            healthScript = this.AddComponent<HealthScript>();
        }
        DamageSize = Random.Range(1.0f, 10.0f);
        LerpMultiplier = Random.Range(0.1f, 2.0f);
        DifferenceInAttacks = Random.Range(DifferenceInAttackRange[0], DifferenceInAttackRange[1]);
    }

    // Update is called once per frame
    void Update()
    {

        if (Player != null)
        {
            if (IsActivated)
            {
                if (!healthScript.IsDead)
                {
                    HasPlayedDeathAnimation = true;
                    float _DistanceFromPlayer = Vector3.Distance(this.transform.position, Player.transform.position);
                    DebugDistanceVariable = _DistanceFromPlayer;
                    Vector3 VectorPointToPlayersAttackingRadius = (Player.transform.position - this.transform.position);
                    VectorPointToPlayersAttackingRadius.Normalize();
                    VectorPointToPlayersAttackingRadius *= DistanceAttack;
                    VectorPointToPlayersAttackingRadius += Player.transform.position;
                    if (_DistanceFromPlayer >= DistanceAttack && _DistanceFromPlayer <= DistanceRadar)
                    {
                        //Move the eagle towards the player
                        this.transform.position = Vector3.Lerp(this.transform.position, VectorPointToPlayersAttackingRadius, Time.deltaTime * LerpMultiplier);
                        FacePlayer();
                    }
                    else if (_DistanceFromPlayer <= DistanceAttack)
                    {
                        if (Time.time - LastAttackTime > DifferenceInAttacks)
                        {
                            EagleAnimator.SetTrigger("Attack");
                            PlayerBehaviourScript playerBehaviourScript = Player.GetComponent<PlayerBehaviourScript>();
                            HealthScript playerHealthScript = Player.GetComponent<HealthScript>();
                            if (playerHealthScript != null)
                            {
                                playerHealthScript.GiveDamage(DamageSize);
                            }
                            if (playerBehaviourScript != null)
                            {
                                playerBehaviourScript.DecreaseFoodFromInventory(Mathf.RoundToInt(Random.Range(10, 20)));
                            }
                            LastAttackTime = Time.time;

                        }
                        FacePlayer();

                    }
                    else
                    {
                        // Do nothing, just keep hovering..?
                    }
                }
                else
                {
                    EagleAnimator.SetTrigger("isDead");
                    //Debug.Log(EagleAnimator.GetCurrentAnimatorStateInfo().);
                    //while (!EagleAnimator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
                    //{
                    //EagleAnimator.SetTrigger("isDead");
                    //Debug.Log(EagleAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash);
                    //}
                    //HasPlayedDeathAnimation = true;
                    //}

                }
            }
            
        }
    }
    public void FacePlayer()
    {
        if(Player != null)
        {

            this.transform.rotation = Quaternion.LookRotation(Player.transform.position - this.transform.position);
            //this.transform.Rotate();
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, DistanceAttack);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, DistanceRadar);
    }
}
