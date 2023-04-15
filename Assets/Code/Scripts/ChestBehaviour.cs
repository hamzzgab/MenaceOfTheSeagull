using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    public Animator ChestAnimator;

    public bool OpenVar = false;
    public void Open()
    {
        
    }
    public void Update()
    {
        ChestAnimator.SetBool("Open", OpenVar);
    }

}
