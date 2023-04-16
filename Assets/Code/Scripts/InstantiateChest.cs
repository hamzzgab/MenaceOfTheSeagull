using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateChest : MonoBehaviour
{
    public GameObject ChestPrefab;
    public GameObject[] SpawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            Instantiate(ChestPrefab, SpawnPoints[i].transform.position, SpawnPoints[i].transform.rotation);  
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
