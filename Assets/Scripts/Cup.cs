using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Receipt;
public class Cup : MonoBehaviour
{
    [SerializeField] private int mlCapacity;
    private float ml;
    private float fill;
    private Animation anim;
    private AnimationClip animClip;
    private GameObject fluid;
    private MeshRenderer fluidRenderer;
    private List<IngredientAmount> ingredientAmountList = new List<IngredientAmount>();
    private Color fluidColor;
    public struct ColorInfluence
    {
        public Color color;
        public float influence;
    }

    private void Awake()
    {
        Pickable pickable = gameObject.AddComponent<Pickable>();
        pickable.needRotate = false;
        pickable.Init();
    }
    private void Start()
    {
        fluid = transform.GetChild(0).gameObject;
        fluidRenderer = fluid.GetComponent<MeshRenderer>();
        anim = fluid.GetComponent<Animation>();
        animClip = anim.clip;
        anim[animClip.name].speed = 0f;
    }

    public void Fill(IngredientAmount ingredientAmount)
    {
        IngredientAmount listTarget = ingredientAmountList.Find(x => x.ingredient = ingredientAmount.ingredient);
        if (listTarget.Equals(null))
            ingredientAmountList.Add(ingredientAmount);
        else
            listTarget.ml += ingredientAmount.ml;
        UpdateAppearance();
    }

    private void UpdateAppearance()
    {
        SetFill();
        LerpColors();
    }

    private void LerpColors()
    {
        ColorInfluence[] colorInfluenceArray = ingredientAmountList.Select(
            x => new ColorInfluence()
            {
                color = x.ingredient.color,
                influence = x.ml / ml
            }).ToArray();
        Color outputColor = colorInfluenceArray[0].color;
        for (int i = 1; i < colorInfluenceArray.Length; i++)
            outputColor = Color.Lerp(outputColor, colorInfluenceArray[i].color, colorInfluenceArray[i].influence);
        fluidRenderer.material.color = outputColor;
    }

    private void SetFill()
    {
        fill = ingredientAmountList.Select(x => x.ml).Sum() / mlCapacity;
        anim[animClip.name].normalizedTime = fill;
    }
}
