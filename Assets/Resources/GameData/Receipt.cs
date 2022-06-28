using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New receipt", menuName = "Receipt", order = 51)]
public class Receipt : ScriptableObject
{
    [Serializable]
    public struct IngredientAmount
    {
        public Ingredient ingredient;
        public int ml;
    }
    [SerializeField] private Sprite icon;

    [SerializeField] private IngredientAmount[] ingredientsAmount;
}