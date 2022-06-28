using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;

public class FoodContainer : Usable
{
    public Game.Food food;
    int count;
   // TextMeshPro textMesh;
    // Start is called before the first frame update
    void Start()
    {
        food = Game.GetFood("Potato");
        count = 5;
      //  textMesh = transform.GetChild(0).GetComponent<TextMeshPro>();
      //  textMesh.text = "Potato " + count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    public override void Use(GameObject player)
    {
        if (count > 0)
        {
            player.GetComponent<PlayerScript>().PickObject(Instantiate(Game.GetFoodObject(food), transform.position, Quaternion.identity));
            count--;
         //   textMesh.text = "Potato " + count.ToString();
        }
    }
    */
}
