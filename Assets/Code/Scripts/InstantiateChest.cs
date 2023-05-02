using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateChest : MonoBehaviour
{
    public GameObject ChestPrefab;
    public string ChestPrefabItemSpawnLocStr = "MysteryObject";

    public GameObject[] SpawnPoints;

    public GameObject[] MysteryObject;

    public List<GameObject> ChestObjects;

    // Start is called before the first frame update
    void Start()
    {

        this.InitializeChestSpawner();
        //Instantiate(MysteryObject[randomNumber], ChestPrefab.GetComponentInChildren<>.transform.position, Chest.transform.rotation);
    }
    public void InitializeChestSpawner()
    {
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            int randomNumber = Random.Range(0, MysteryObject.Length);
            GameObject result = Instantiate(ChestPrefab, SpawnPoints[i].transform.position, SpawnPoints[i].transform.rotation);
            ChestObjects.Add(result);
            result.SetActive(true);

            Transform[] children = result.GetComponentsInChildren<Transform>();
            foreach (Transform child in children)
            {
                if (child.name == ChestPrefabItemSpawnLocStr)
                {
                    GameObject mysteryobj = Instantiate(MysteryObject[randomNumber], child.transform);
                    result.GetComponent<ChestBehaviour>().TargetPrizeObject = mysteryobj;
                    
                    if (mysteryobj.name.Contains("Eagle_Elite"))
                    {
                        mysteryobj.GetComponent<EagleBehaviour>().IsActivated = false;
                        mysteryobj.SetActive(false);
                    }

                    MeshRenderer prize_mesh = mysteryobj.GetComponent<MeshRenderer>();
                    if (prize_mesh != null)
                    {
                        prize_mesh.enabled = false;
                    }
                    //mysteryobj.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
                    break;
                }
            }
        }
    }
    public void DeleteAllChests()
    {
        foreach(GameObject chest in this.ChestObjects)
        {
            Destroy(chest);
        }
        this.ChestObjects.Clear();
    }
    // Update is called once per frame
    void Update()
    {
        if(OVRInput.Get(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.R))
        {
            this.DeleteAllChests();
            this.InitializeChestSpawner();
        }    
    }
    public IEnumerator RespawnChestInSomeTime()
    {
        yield return new WaitForSeconds(2.0f);
        this.InitializeChestSpawner();
    }
}
