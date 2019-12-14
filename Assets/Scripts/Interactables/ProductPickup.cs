using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductPickup : Pickupable
{
    [SerializeField] private Transform visualRoot = null;
    private ProductType product;

    public void InitialiseProductData(ProductType type)
    {
        product = type;
        GameObject visualPrefab = IngredientDataLookupManager.Instance.GetPrefabForProductType(type);
        Instantiate(visualPrefab, visualRoot);
    }
}
