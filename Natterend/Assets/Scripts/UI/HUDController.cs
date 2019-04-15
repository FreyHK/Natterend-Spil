using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public static HUDController Instance;

    public TextMeshProUGUI headerText;
    public TextMeshProUGUI interactText;

    void Awake()
    {
        Instance = this;
    }

    public void SetHeader(string s)
    {
        headerText.text = s;
    }

    public void ShowInteractText (bool enabled)
    {
        interactText.enabled = enabled;
    }
}
