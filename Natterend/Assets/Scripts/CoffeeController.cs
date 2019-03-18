using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeController : MonoBehaviour
{
    public float maxCoffeeTime = 100;
    float curCoffeeTime;

    public GameObject[] coffeeBar;
    int curCoffeeBar;

    public float maxTimeBeforeLoss = 10;
    float curTimeBeforeLoss;

    void Awake()
    {
        curCoffeeBar = coffeeBar.Length;
        curTimeBeforeLoss = maxTimeBeforeLoss;
        curCoffeeTime = maxCoffeeTime / coffeeBar.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (curCoffeeBar <= 0)
            return;

        if (curTimeBeforeLoss <= 0)
        {
            curCoffeeTime -= Time.deltaTime;
        }
        else
        {
            curTimeBeforeLoss -= Time.deltaTime;
        }

        if (curCoffeeTime <= 0)
        {
            coffeeBar[curCoffeeBar - 1].SetActive(false);
            curCoffeeBar--;
            curCoffeeTime = maxCoffeeTime / coffeeBar.Length;
        }

        if (curCoffeeBar <= 0)
        {
            CoffeeEmpty();
        }
    }

    public void RefreshCoffee()
    {
        curCoffeeTime = maxCoffeeTime / coffeeBar.Length;
        curCoffeeBar = coffeeBar.Length;       
        curTimeBeforeLoss = maxTimeBeforeLoss;
        for (int i = 0; i < coffeeBar.Length; i++)
        {
            coffeeBar[i].SetActive(true);
        }
    }

    void CoffeeEmpty()
    {
        // do some shiitttt
    }
}
