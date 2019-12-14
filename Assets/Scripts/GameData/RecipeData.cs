using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class RecipeData : SerializedScriptableObject
{
    [SerializeField] private List<System.Tuple<IngredientType, ProcessType>> recipe = new List<System.Tuple<IngredientType, ProcessType>>();
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