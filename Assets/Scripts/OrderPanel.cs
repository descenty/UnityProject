using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderPanel : MonoBehaviour
{
    public Text orderNumberText;
    public Text takeawayText;
    public RectTransform content;
    public Text orderCostText;
    public Text tableNumberText;
    public OrderPos[] orderPosArray;
    // Start is called before the first frame update
    void Start()
    {
        GameObject posSmallPanelPrefab = Resources.Load<GameObject>(@"Prefabs\PosSmallPanel");
        int xmod = 0;
        foreach(OrderPos orderPos in orderPosArray)
        {
            PosSmallPanel posSmallPanel = Instantiate(posSmallPanelPrefab, content).GetComponent<PosSmallPanel>();
            posSmallPanel.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(xmod * 140, 0);
            posSmallPanel.posImage.sprite = Resources.Load<Sprite>(orderPos.restPos.food.foodSprite);
            posSmallPanel.posCountText.text = orderPos.count.ToString();
            posSmallPanel.posName.text = orderPos.restPos.food.foodName;
        }
        
    }
}
