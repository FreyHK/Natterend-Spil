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

    public static CoffeeController Instance;

    void Awake()
    {
        Instance = this;

        playerMovement = FindObjectOfType<PlayerMovement>();

        curTime = maxTime / coffeeBar.Length;
        curCoffeeBar = coffeeBar.Length;
        for (int i = 0; i < coffeeBar.Length; i++)
        {
            coffeeBar[i].SetActive(true);
        }
    }

    public bool CanSprint()
    {
        return curCoffeeBar > 0;
    }

    void Update()
    {
        if (playerMovement.currentMovement == PlayerMovement.MovementType.Sprinting)
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
        //Play sound
        AudioManager.Instance.Play("RefillCoffee");
    }
}
