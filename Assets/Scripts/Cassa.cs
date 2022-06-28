using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Cassa : MonoBehaviour
{
    public Text costText;
    public Text orderCostText;
    public Text awaitText;
    public Text doneText;
    public Text paymentCostText;
    public GameObject emptyText;
    public Restaurant rest;
    public RectTransform content;
    public RectTransform orderContent;
    public GameObject paymentPanel;
    public POSTerminal posTerminal;
    public Text posCountText;
    public Toggle takeawayToggle;
    private int dayOrderNumber;
    private int cost;
    private List<OrderPos> orderPosList = new List<OrderPos>();
    private List<FoodPanel> foodPanels = new List<FoodPanel>();
    public void ReceiveInput(float xOffset, float yOffset)
    {
    }
    public void Activate()
    {
    }
    public void Deactivate()
    {
    }
    public void AddPos(RestPos pos)
    {
        cost += pos.price;
        if(orderPosList.Exists(x => x.restPos == pos))
            orderPosList.Find(x => x.restPos == pos).count++;
        else
            orderPosList.Add(new OrderPos(pos, 1));
        UpdateData();
    }
    public void RemovePos(RestPos pos)
    {
        OrderPos orderPos = orderPosList.Find(x => x.restPos == pos);
        orderPos.count--;
        if(orderPos.count == 0)
        {
            orderPosList.Remove(orderPos);
        }
        UpdateData();
    }
    public void UpdateData()
    {
        cost = 0;
        orderPosList.ForEach(x => cost += x.restPos.price * x.count);
        costText.text = cost.ToString() + " руб";
        orderCostText.text = costText.text;
        paymentCostText.text = costText.text;
        int allposCount = 0;
        orderPosList.ForEach(x => allposCount+= x.count);
        posCountText.text = allposCount.ToString();
        //OrderPanel
        if (orderPosList.Count == 0)
            emptyText.SetActive(true);
        else
            emptyText.SetActive(false);
        //Очистка
        for (int i = 0; i < orderContent.childCount; i++)
        {
            Destroy(orderContent.GetChild(0).gameObject);
        }
        GameObject posPanelPrefab = Resources.Load<GameObject>(@"Prefabs\PosPanel");
        int ymod = 0;
        foreach (OrderPos orderPos in orderPosList)
        {
            PosPanel posPanel = Instantiate(posPanelPrefab, orderContent.transform).GetComponent<PosPanel>();
            posPanel.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -150 * ymod);
            posPanel.posNName.text = (ymod + 1).ToString() + ". " + orderPos.restPos.food.foodName;
            posPanel.posCount.text = orderPos.count.ToString() + " шт";
            posPanel.posPrice.text = (orderPos.restPos.price * orderPos.count).ToString() + " руб";
            posPanel.upButton.onClick.AddListener(delegate { AddPos(orderPos.restPos); });
            posPanel.downButton.onClick.AddListener(delegate { RemovePos(orderPos.restPos); });
            ymod++;
        }
        //Food Panels
        foreach (FoodPanel foodPanel in foodPanels)
        {
            if (orderPosList.Exists(x => x.restPos.food.foodName == foodPanel.foodName.text))
                foodPanel.foodCount = orderPosList.Find(x => x.restPos.food.foodName == foodPanel.foodName.text).count;
            else
                foodPanel.foodCount = 0;
        }

    }
    public void MakeOrder()
    {
        dayOrderNumber++;
        rest.AddOrder(new Order(orderPosList.ToArray(), takeawayToggle.isOn, cost, dayOrderNumber));
        orderPosList.Clear();
        UpdateData();
    }
    public void Payment()
    {
        StartCoroutine(WaitForPayment());
    }
    IEnumerator WaitForPayment()
    {
        print("payment start");
        awaitText.GetComponent<TextAwaiter>().Play();
        bool pay = false;
        Action action = () => pay = true;
        posTerminal.payevent.AddListener(action.Invoke);
        yield return new WaitUntil(() => pay);
        print("payment over");
        posTerminal.payevent.RemoveListener(action.Invoke);
        awaitText.GetComponent<TextAwaiter>().StopAllCoroutines();
        awaitText.gameObject.SetActive(false);
        doneText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        awaitText.gameObject.SetActive(true);
        doneText.gameObject.SetActive(false);
        paymentPanel.SetActive(false);
        MakeOrder();
    }
    public void Start()
    {
        GameObject foodPanelprefab = Resources.Load<GameObject>(@"Prefabs\FoodPanel");
        RestPos[] restFood = rest.GetRestFood();
        int xmod = 0;
        int ymod = 0;
        foreach (RestPos restPos in restFood)
        {
            if (xmod == 2)
            {
                xmod = 0;
                ymod++;
            }
            FoodPanel foodPanel = Instantiate(foodPanelprefab, content.transform).GetComponent<FoodPanel>();
            foodPanel.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(45 + xmod * 285, -45 + ymod * 285);
            foodPanel.foodImage.sprite = Resources.Load<Sprite>(restPos.food.foodSprite);
            foodPanel.foodName.text = restPos.food.foodName;
            foodPanel.foodPrice.text = restPos.price.ToString() + " руб";
            foodPanel.foodButton.onClick.AddListener(delegate { AddPos(restPos); });
            xmod++;
            foodPanels.Add(foodPanel);
        }
    }
}
