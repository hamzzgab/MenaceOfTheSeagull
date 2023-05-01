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
        AudioListener.volume += 0.25f;       
    }

    public void VolumeDown()
    {
        AudioListener.volume -= 0.25f;
    }

    public void HapticsOn()
    {
        GlobalsManager.Haptics = true;
    }

    public void HapticsOff()
    {
        GlobalsManager.Haptics = false;
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
