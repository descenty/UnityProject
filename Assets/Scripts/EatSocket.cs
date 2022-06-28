using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatSocket : MonoBehaviour
{
    public bool free = true;
    private List<Game.Food> doneFood = new List<Game.Food>();
    private RestPos[] orderFood;
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<People>())
        {
            Restaurant rest = transform.parent.parent.parent.parent.parent.GetComponent<Restaurant>();
            orderFood = rest.GetRestFood();
            rest.AddOrder(orderFood.GetRange(0, orderFood.Count));
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void AddFood(Game.Food food)
    {
        doneFood.Add(food);
        bool contains = true;
        foreach (Game.Food ifood in orderFood)
        {
            if (!doneFood.Contains(ifood))
            {
                contains = false;
            }
        }
        if (contains)
            print("Order is over");
    }
    */
}
