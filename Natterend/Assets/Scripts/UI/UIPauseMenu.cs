using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseMenu : MonoBehaviour
{
    CanvasGroup group;

    bool isShown;

    void Start()
    {
        group = GetComponent<CanvasGroup>();
        ShowHideMenu(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowHideMenu(!isShown);
        }
    }

    public void ShowHideMenu(bool shown)
    {
        isShown = shown;
        group.blocksRaycasts = shown;
        group.alpha = shown ? 1f : 0f;

        FindObjectOfType<PlayerMovement>().enabled = !shown;

        Cursor.lockState = shown ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = shown;
    }

    public void QuitToMenu()
    {
        GameInitializer.Instance.LoadMenu();
    }
}
