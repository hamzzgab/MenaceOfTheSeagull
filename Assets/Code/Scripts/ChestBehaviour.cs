using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    public BoxCollider Collider;

    public GameObject ChestObject;
    public GameObject ChestLidObject;
    public GameObject ChestPrizeObject;

    public Animator ChestAnimator;

    public bool OpenVar = false;

    public void Start()
    {
        Collider = GetComponent<BoxCollider>();
    }
    public void Open()
    {
        
    }
    public void Update()
    {
        ChestAnimator.SetBool("Open", OpenVar);        
    }

    public void HideChest()
    {
        ToggleMeshRenderer(ChestObject, false);
        ToggleMeshRenderer(ChestLidObject, false);
        Transform prize_object = ChestPrizeObject.GetComponentInChildren<Transform>();
        Transform[] children = ChestPrizeObject.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            ToggleMeshRenderer(child.gameObject, false);
        }
    }

    public IEnumerator Vibrator()
    {
        OVRInput.SetControllerVibration(1f, 0.3f);
        yield return new WaitForSeconds(0.5f);
        OVRInput.SetControllerVibration(0f, 0f);
    }

    public void ShowChest()
    {
        StartCoroutine(Vibrator());
        //OVRInput.SetControllerVibration(1f, 1f);
        ToggleMeshRenderer(ChestObject, true);
        ToggleMeshRenderer(ChestLidObject, true);
        Transform[] children = ChestPrizeObject.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            ToggleMeshRenderer(child.gameObject, true);
        }
        Collider.enabled = false;
        OpenVar = true;
    }
    public void ToggleMeshRenderer(GameObject gameObject, bool toggle)
    {
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        Debug.Log("==== " + gameObject.name + " ====");
        Debug.Log(renderer);
        if(renderer != null)
        {
            renderer.enabled = toggle;
        }
    }

}
