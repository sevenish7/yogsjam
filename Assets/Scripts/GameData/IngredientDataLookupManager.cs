using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class IngredientDataLookupManager : MonoBehaviour
{
    public static IngredientDataLookupManager Instance { get; private set; }

    [SerializeField, InlineEditor] private IngredientDataLookup lookup;

    private void Awake()
    {
        if(Instance != this)
        {
            Instance = this;
        }
    }

    public GameObject GetPrefabForIngredientType(IngredientData data)
    {
        if(lookup.IngredientPrefabDictionary.ContainsKey(data.ProcessedIngredient))
        {
            return lookup.IngredientPrefabDictionary[data.ProcessedIngredient];
        }
        else
        {
            return lookup.GarbagePrefab;
        }
    }

    public GameObject GetPrefabForProductType(ProductType data)
    {
        if(lookup.ProductPrefabDictionary.ContainsKey(data))
        {
            return lookup.ProductPrefabDictionary[data];
        }
        else
        {
            return lookup.GarbagePrefab;
        }
    }

    public ProductType GetProductTypeForRecipe(IEnumerable<System.Tuple<IngredientType, ProcessType>> attemptedRecipe)
    {
        if(attemptedRecipe != null)
        {
            foreach(var recipe in lookup.ValidRecipes)
            {
                if(recipe.MatchesRecipe(attemptedRecipe))
                {
                    return recipe.Product;
                }
            }
        }
        return ProductType.GARBAGE;
    }
}
