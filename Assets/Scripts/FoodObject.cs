using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodObject : MonoBehaviour
{
    public Game.Food food;
    public string foodType;
    // Start is called before the first frame update
    void Start()
    {
        food = Game.GetFood(foodType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
