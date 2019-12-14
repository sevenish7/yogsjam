using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPickup : Pickupable
{
    [SerializeField] private IngredientData ingredient;

    public IngredientData Ingredient { get { return ingredient; } }

    public void InitialiseIngredientData(IngredientData data)
    {
        ingredient = data;
        //lookup appearance prefab from manager
    }
}
