using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoinBehaviour : MonoBehaviour
{
    public GameObject Player;

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
        {
            if (GlobalsManager.Player != null)
            {
                Player = GlobalsManager.Player;
            }
        }
    }

    public void AddCoins()
    {
        if (Player != null)
        {
            PlayerBehaviourScript playerBehaviourScript = Player.GetComponent<PlayerBehaviourScript>();
            playerBehaviourScript.AddCoins(Mathf.RoundToInt(Random.Range(50, 100)));            
        }
    }
}
