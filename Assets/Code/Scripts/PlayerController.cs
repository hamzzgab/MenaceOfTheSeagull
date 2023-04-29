using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    //Variables
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        // is the controller on the ground?
        if (controller.isGrounded)
        {
            //Feed moveDirection with input.
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            //Multiply it by speed.
            moveDirection *= speed;
            //Jumping
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        //Applying gravity to the controller
        moveDirection.y -= gravity * Time.deltaTime;
        //Making the character move
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"Collided: {other.gameObject.name}");
        if (other.gameObject.tag == "Box")
        {
            Debug.Log("Collided!");            
            ChestBehaviour behaviour_script = other.GetComponent<ChestBehaviour>();
            if(behaviour_script != null)
            {
                behaviour_script.ShowChest();
            }
            //other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            //Transform[] children = other.gameObject.GetComponentsInChildren<Transform>();
            //foreach(Transform child in children)
            //{
            //    MeshRenderer childRenderer = child.GetComponent<MeshRenderer>();
            //    if (childRenderer != null)
            //    {
            //        childRenderer.enabled = false;
            //    }
            //}
            //other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
    public void EnableMeshes(Transform gameObject)
    {        
        Debug.Log(gameObject.name);
        MeshRenderer object_mesh = gameObject.gameObject.GetComponent<MeshRenderer>();
        if (object_mesh != null)
        {
            object_mesh.enabled = true;
        }
        BoxCollider object_box = gameObject.gameObject.GetComponent<BoxCollider>();
        if (object_box != null)
        {
            object_box.enabled = true;
        }
        Transform[] children = gameObject.gameObject.GetComponentsInChildren<Transform>();
        if (children.Length > 0)
        {
            foreach (Transform child in children)
            {
                //MeshRenderer childRenderer = child.GetComponent<MeshRenderer>();
                //if (childRenderer != null)
                //{
                //    childRenderer.enabled = false;
                //}
                EnableMeshes(child);
            }
        }
        
    }
}
