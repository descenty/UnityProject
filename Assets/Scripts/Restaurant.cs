using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RestPos
{
    public Game.Food food;
    public int price;
    public RestPos(Game.Food food, int price)
    {
        this.food = food;
        this.price = price;
    }
}
public class Order
{
    public OrderPos[] orderPosArray;
    public bool takeaway;
    public int number;
    public int cost;
    public int tableNumber;
    public Order(OrderPos[] orderPosArray, bool takeaway, int cost, int number)
    {
        this.orderPosArray = orderPosArray;
        this.takeaway = takeaway;
        this.cost = cost;
        this.number = number;
    }
}
public class OrderPos
{
    public RestPos restPos;
    public int count = 1;
    public OrderPos(RestPos restPos, int count)
    {
        this.restPos = restPos;
        this.count = count;
    }
}
public class Restaurant : MonoBehaviour
{
    public UnityEvent addOrderEvent;
    public List<Order> orders = new List<Order>();
    public List<RestPos> restFood = new List<RestPos>()
    {
       // new RestPos(Game.GetFood("Fried Potato"), 100),
        new RestPos(Game.GetFood("Капучино"), 120)
    };
    public RestPos[] GetRestFood()
    {
        return restFood.ToArray();
    }
    public void AddOrder(Order order)
    {
        orders.Add(order);
        print("Новый заказ");
        addOrderEvent.Invoke();
    }
    public void Open()
    {

    }
}
