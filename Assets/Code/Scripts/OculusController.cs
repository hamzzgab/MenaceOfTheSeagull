using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Box")
        {
            Debug.Log("Collided!");
            ChestBehaviour behaviour_script = other.GetComponent<ChestBehaviour>();
            if (behaviour_script != null)
            {
                behaviour_script.ShowChest();
            }
        }
    }
}
