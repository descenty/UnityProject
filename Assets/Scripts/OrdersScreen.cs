using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrdersScreen : MonoBehaviour
{
    public Restaurant rest;
    public RectTransform ordersContent;
    // Start is called before the first frame update
    void Start()
    {
        rest.addOrderEvent.AddListener(delegate { UpdateOrderPanel(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void UpdateOrderPanel()
    {
        //Очистка
        for (int i = 0; i < ordersContent.childCount; i++)
        {
            Destroy(ordersContent.GetChild(0).gameObject);
        }
        GameObject orderPanelPrefab = Resources.Load<GameObject>(@"Prefabs\OrderPanel");
        Order[] orders = rest.orders.ToArray();
        int ymod = 0;
        foreach (Order order in orders)
        {
            OrderPanel orderPanel = Instantiate(orderPanelPrefab, ordersContent).GetComponent<OrderPanel>();
            orderPanel.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -140 * ymod);
            orderPanel.orderCostText.text = order.cost.ToString() + " руб";
            if (order.takeaway)
            {
                orderPanel.takeawayText.text = "С собой";
                orderPanel.tableNumberText.text = "";
            }
            else
            {
                orderPanel.tableNumberText.text = "Столик";
                orderPanel.tableNumberText.text = order.tableNumber.ToString();
            }
            orderPanel.orderPosArray = order.orderPosArray;
            orderPanel.orderNumberText.text = "Заказ № " + order.number.ToString();
            ymod++;
        }
    }
}
