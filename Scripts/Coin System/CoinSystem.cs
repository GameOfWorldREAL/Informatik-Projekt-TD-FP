using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{

    public float money;

    public void buy(float amount)
    {
        if (money - amount > 0)
        {
            money -= amount;
        }
        else
        {
            money = 0;
        }
    }

    public void increase(float amount)
    {
        money += amount;
    }
}

