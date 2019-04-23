using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour
{
    public GameObject player;

    void OnCollisionEnter(Collision collision)
    {
        RefreshCoffee();
    }

    public void RefreshCoffee()
    {      
        player.GetComponent<CoffeeController>().RefreshCoffee();
        AudioManager.Instance.Play("RefillCoffee");
    }
}
