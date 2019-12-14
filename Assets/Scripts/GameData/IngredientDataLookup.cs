using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName="IngredientLookup", menuName="Yogs/New Ingredient Lookup")]
public class IngredientDataLookup : SerializedScriptableObject
{
    [SerializeField] private GameObject garbagePrefab;
    public GameObject GarbagePrefab { get { return garbagePrefab; } }
    
    [OdinSerialize] private Dictionary<System.Tuple<IngredientType, ProcessType>, GameObject> ingredientPrefabDictionary = new Dictionary<System.Tuple<IngredientType, ProcessType>, GameObject>();
    public Dictionary<Tuple<IngredientType, ProcessType>, GameObject> IngredientPrefabDictionary { get => ingredientPrefabDictionary; }
    
    [OdinSerialize] private Dictionary<ProductType, GameObject> productPrefabDictionary = new Dictionary<ProductType, GameObject>();
    public Dictionary<ProductType, GameObject> ProductPrefabDictionary { get { return productPrefabDictionary; } }

    [SerializeField] private List<RecipeData> validRecipes = new List<RecipeData>();
    public List<RecipeData> ValidRecipes { get { return validRecipes; } }
}
