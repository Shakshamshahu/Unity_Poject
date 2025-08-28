using System;
using UnityEngine;

public class Coin_Trigger_Event : MonoBehaviour
{
    public static event Action coinTrigger;

    public static void CoinCollect()
    {
        coinTrigger?.Invoke();
    }
}
