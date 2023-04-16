using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateChest : MonoBehaviour
{
    public GameObject ChestPrefab;
    public string ChestPrefabItemSpawnLocStr = "MysteryObject";

    public GameObject[] SpawnPoints;

    public GameObject[] MysteryObject;

    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            int randomNumber = Random.Range(0, MysteryObject.Length);
            GameObject result = Instantiate(ChestPrefab, SpawnPoints[i].transform.position, SpawnPoints[i].transform.rotation);
            result.SetActive(true);

            Transform[] children = result.GetComponentsInChildren<Transform>();          
            foreach (Transform child in children)
            {
                if(child.name == ChestPrefabItemSpawnLocStr)
                {   
                    GameObject mysteryobj = Instantiate(MysteryObject[randomNumber], child.transform);
                    MeshRenderer prize_mesh = mysteryobj.GetComponent<MeshRenderer>();
                    if(prize_mesh != null)
                    {
                        prize_mesh.enabled = false;
                    }
                    //mysteryobj.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
                    break;
                }
            }
        }
        
        //Instantiate(MysteryObject[randomNumber], ChestPrefab.GetComponentInChildren<>.transform.position, Chest.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
