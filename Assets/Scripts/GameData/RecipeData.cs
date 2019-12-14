using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName="New Recipe Data", menuName="Yogs/New Recipe")]
public class RecipeData : SerializedScriptableObject
{
    [SerializeField, ListDrawerSettings(AlwaysAddDefaultValue=true)] 
    private List<System.Tuple<IngredientType, ProcessType>> requiredIngredients = new List<System.Tuple<IngredientType, ProcessType>>();
    [SerializeField] private ProductType product;

    public ProductType Product { get { return product; } }

    public bool MatchesRecipe(IEnumerable<System.Tuple<IngredientType, ProcessType>> ingredients)
    {
        return requiredIngredients.Count == ingredients.Count() && !requiredIngredients.Except(ingredients).Any();
    }
}

[System.Serializable]
public class IngredientData
{
    private IngredientType baseIngredientType;
    private ProcessType appliedProcess = ProcessType.NONE;

    public IngredientData(IngredientType type)
    {
        baseIngredientType = type;
        appliedProcess = ProcessType.NONE;
    }

    public ProcessType AppliedProcess
    {
        get { return appliedProcess; }
        set
        {
            if(appliedProcess != ProcessType.NONE)
            {
                appliedProcess = ProcessType.GARBAGE;
            }
            else
            {
                appliedProcess = value;
            }
        }
    }

    public System.Tuple<IngredientType, ProcessType> ProcessedIngredient { get { return new System.Tuple<IngredientType, ProcessType>(baseIngredientType, appliedProcess);}}
}

public enum IngredientType
{
    METAL,
    SALT,
    RUBBER,
    JAFFA,
}

public enum ProcessType
{
    NONE,
    SCIENCE,
    BLASTED,
    GARBAGE,
}

public enum ProductType
{
    BEE,
    RED_MATTER,
    ISRAPHEL,
    DONCANNON,
    PIG,
    GARBAGE,
}