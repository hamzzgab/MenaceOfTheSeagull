using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.FilePathAttribute;
using static UnityEditor.PlayerSettings;

public class InstantiateRandomly : MonoBehaviour
{
    public GameObject objectsToInstantiate;


    public void createObject()
    {

    }

    // Start is called before the first frame update
    public InstantiateRandomly()
    {
    }
    public void Start()
    {
        Debug.Log(objectsToInstantiate);
        //GameObject newobject = GameObject.Instantiate(objectsToInstantiate);
        var pos = new Vector3(Random.Range(-7.0f, 7.0f), 1.0f, Random.Range(-7.0f, 7.0f));
        //newobject.SetActive(true);
        GameObject newobject = Instantiate(objectsToInstantiate, pos, Quaternion.identity);
        newobject.SetActive(true);
        //newobject.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
