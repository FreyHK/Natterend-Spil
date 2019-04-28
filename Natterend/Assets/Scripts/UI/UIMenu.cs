using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    public void OnPlayButton()
    {
        GameInitializer.Instance.LoadGame();
        AudioManager.Instance.Stop("MenuMusic");
    }

    public void OnMenuButton()
    {
        //Load menu
        GameInitializer.Instance.LoadMenu();
    }
    public void OnExitButton()
    {
        Application.Quit();

    }
}
