using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodPanel : MonoBehaviour
{
    public Text foodPrice;
    public Text foodName;
    public Image foodImage;
    public Image foodCountPanel;
    public Button foodButton;
    private int count;
    public int foodCount
    {
        get { return count; }
        set
        {
            count = value;
            if(count > 0)
            {
                foodCountPanel.gameObject.SetActive(true);
                foodCountPanel.transform.GetChild(0).GetComponent<Text>().text = count.ToString();
            }
            else
            {
                foodCountPanel.gameObject.SetActive(false);
            }
        }
    }
}
