using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPickup : Pickupable
{
    [SerializeField] private IngredientData ingredient;
    [SerializeField] private Transform visualRoot;

    public IngredientData Ingredient { get { return ingredient; } }

    public void InitialiseIngredientData(IngredientData data)
    {
        ingredient = data;
        
        GameObject prefab = IngredientDataLookupManager.Instance.GetPrefabForIngredientType(ingredient);
        Instantiate(prefab, visualRoot);
    }
}
