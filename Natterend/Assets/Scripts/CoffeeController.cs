using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeController : MonoBehaviour
{
    public float maxTime = 10f;
    float curTime;

    public GameObject[] coffeeBar;
    int curCoffeeBar;

    PlayerMovement playerMovement;

    void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        RefreshCoffee();
    }

    void Update()
    {
        if (Input.GetButtonDown("Sprint") && curCoffeeBar > 0)
        {
            playerMovement.IsSprinting = true;
        }else if (Input.GetButtonUp("Sprint") || curCoffeeBar <= 0)
        {
            playerMovement.IsSprinting = false;
        }else if (playerMovement.IsSprinting)
        {
            RemoveCoffee();
        }
    }

    void RemoveCoffee()
    {
        curTime -= Time.deltaTime;

        if (curTime <= 0)
        {
            coffeeBar[curCoffeeBar - 1].SetActive(false);
            curCoffeeBar--;
            curTime = maxTime / coffeeBar.Length;
        }
    }

    public void RefreshCoffee()
    {
        curTime = maxTime / coffeeBar.Length;
        curCoffeeBar = coffeeBar.Length;
        for (int i = 0; i < coffeeBar.Length; i++)
        {
            coffeeBar[i].SetActive(true);
        }
    }
}
