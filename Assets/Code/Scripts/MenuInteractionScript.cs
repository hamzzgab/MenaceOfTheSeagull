using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInteractionScript : MonoBehaviour
{
    public GameObject HandMeshObject;
    public GameObject MainMenuObject;

    public void DisplaySettingPoseOptions()
    {
        MainMenuObject.SetActive(false);
        HandMeshObject.SetActive(true);
    }

    public void DisplayMenuOptions()
    {
        MainMenuObject.SetActive(true);
        HandMeshObject.SetActive(false);
    }

    public void VolumeUp()
    {
        if (GlobalsManager.Volume < 1.0f)
        {
            GlobalsManager.Volume += 0.1f;
        }        
    }

    public void VolumeDown()
    {
        if (GlobalsManager.Volume > 0.0f)
        {
            GlobalsManager.Volume -= 0.1f;
        }
    }

    public void HapticsOn()
    {
        if (!GlobalsManager.Haptics)
        {
            GlobalsManager.Haptics = false;
        }
    }

    public void HapticsOff()
    {
        if (GlobalsManager.Haptics)
        {
            GlobalsManager.Haptics = true;
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
