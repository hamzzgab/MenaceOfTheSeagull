using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RockBehaviour : MonoBehaviour
{
    public float TimeToLive = 20.0f;
    public float StartTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - StartTime > TimeToLive)
        {
            Destroy(this.gameObject);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name == "joint1")
        {
            GameObject parentObject = collision.gameObject.transform.parent.gameObject.transform.parent.gameObject;
            EagleBehaviour eagle_behaviour = parentObject.GetComponent<EagleBehaviour>();
            Debug.Log(eagle_behaviour);
            HealthScript eagle_health_script = eagle_behaviour.healthScript;
            
            if (eagle_health_script != null)
            {
                if (!eagle_health_script.IsDead )
                {
                    eagle_behaviour.EagleAnimator.SetTrigger("Hit");
                    eagle_health_script.GiveDamage(Random.Range(5.0f, 25.0f));
                }
            }
            //Destroy(this.gameObject);
        }
    }
}
