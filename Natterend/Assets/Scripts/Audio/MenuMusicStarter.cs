using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicStarter : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.Play("MenuMusic");
    }

}
