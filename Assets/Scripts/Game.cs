using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    public enum DoObject { Furnace, Huy };
    public class Food
    {
        public string foodName;
        public string foodSprite;
        public Food(string foodName, string foodSprite)
        {
            this.foodName = foodName;
            this.foodSprite = foodSprite;
        }
    }
    public class Receipt
    {
        public Food[] input;
        public Food output;
        public DoObject objectType;
        public int time;
        public Receipt(DoObject objectType)
        {
            this.objectType = objectType;
        }
        public Receipt(Food[] input, Food output, DoObject objectType, int time)
        {
            this.input = input;
            this.output = output;
            this.objectType = objectType;
            this.time = time;
        }
        public Receipt(Food input, Food output, DoObject objectType, int time)
        {
            this.input = new Food[1];
            this.input[0] = input;
            this.output = output;
            this.objectType = objectType;
            this.time = time;
        }
    }
    public static GameObject GetFoodObject(Food food)
    {
        return Resources.Load<GameObject>(@"Prefabs\" + food.foodName);
    }
    public static Food[] Foods = new Food[]
    {
       // new Food("Potato", null),
       // new Food("Fried Potato", null),
        new Food("Капучино", @"Sprites\capuccino")
    };
    public static Food GetFood(string foodName)
    {
        foreach (Food food in Foods)
        {
            if (food.foodName == foodName)
                return food;
        }
        return null;
    }
    public static Receipt[] receipts = new Receipt[]
    {
        new Receipt(GetFood("Potato"), GetFood("Fried Potato"), DoObject.Furnace, 5)
    };
    public static Receipt GetReceipt(Food input, DoObject objectType)
    {
        foreach(Receipt receipt in receipts)
        {
            if (receipt.input[0] == input)
                return receipt;
        }
        return null;
    }
}
