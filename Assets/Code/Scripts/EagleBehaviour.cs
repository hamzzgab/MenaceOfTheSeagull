using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleBehaviour : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        EagleAnimator = GetComponent<Animator>();
        if (Player == null)
        {
            Debug.LogError("No player has been assigned!");
        }
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
                        LastAttackTime = Time.time;
                    }
                    FacePlayer();
                }
                else
                {
                    // Do nothing, just keep hovering..?
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
}
