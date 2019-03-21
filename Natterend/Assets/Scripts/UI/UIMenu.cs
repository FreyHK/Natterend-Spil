using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    public void OnPlayButton()
    {
        GameInitializer.Instance.LoadGame();
    }

    public void OnMenuButton()
    {
        //Load menu
        GameInitializer.Instance.LoadMenu();
    }
}
