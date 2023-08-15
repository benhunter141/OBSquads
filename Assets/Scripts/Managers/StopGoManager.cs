using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopGoManager : MonoBehaviour
{
    //This script stores refs to all orders

    //On Go,
    //1. Orders show some visual indictation that they are being performed (flash green?)
    //2. Balls are given an AddForce (Order.cs does this)
    //3. Orders are cleared
    //4. When balls all stop, some visual indication shows they are ready to be ordered again

    public List<Order> orders;
    public List<BaseUnit> movingUnits;
    public GameState gameState;
    private void Awake()
    {
        gameState = GameState.stopped;  
        orders = new List<Order>();
        movingUnits = new List<BaseUnit>();
    }

    public void ReportStoppedUnit(BaseUnit unit)
    {
        movingUnits.Remove(unit);
    }

    IEnumerator GoPhase()
    {
        Debug.Log("Entering Go Phase");
        gameState = GameState.going;
        while(movingUnits.Count > 0)
        {
            yield return Helpers.EndOfFrame;
        }
        Debug.Log("Entering Stopped Phase");
        gameState = GameState.stopped;
    }

    public void ExecuteOrders()
    {
        if (gameState == GameState.going) return;
        foreach (Order o in orders)
        {
            {
                o.ExecuteOrder();
                movingUnits.Add(o.unit);
            }
        }
        orders.Clear();
        StartCoroutine(GoPhase());
    }

    public void AddToOrders(Order order)
    {
        orders.Add(order);
    }

    public void RemoveFromOrders(Order order)
    {
        if (order is null) return;
        orders.Remove(order);
    }
}

public enum GameState
{
    stopped,
    going
}
