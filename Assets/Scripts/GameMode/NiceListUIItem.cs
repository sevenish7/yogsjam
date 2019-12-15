using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class NiceListUIItem : SerializedMonoBehaviour
{
    [SerializeField] private float waitLengthPerCount = 1f;
    [SerializeField] private float waitBeforeDestroyTime = 3f;
    [SerializeField] private Transform finalProductPreviewParent;
    [SerializeField] private float finalProductScale = 50f;
    [SerializeField] private Vector3 finalProductRotation = new Vector3(0, 160, 0);
    [SerializeField] private GameObject completionIndicator;
    [SerializeField] private List<Image> ingredientImages = new List<Image>();
    [SerializeField] private List<Image> processImages = new List<Image>();

    public ProductType Product { get; private set; }

    private RecipeData recipe;

    public void Initialise(ProductType product, int delayModifier)
    {
        Product = product;
        recipe = IngredientDataLookupManager.Instance.GetRecipeForProductType(Product);

        int counter = 0;
        foreach(var rd in recipe.RequiredIngredients)
        {
            IngredientType ingredient = rd.Item1;
            ProcessType process = rd.Item2;

            ingredientImages[counter].sprite = IngredientDataLookupManager.Instance.GetSpriteForIngredient(ingredient);
            if(process != ProcessType.NONE && process != ProcessType.GARBAGE)
            {
                processImages[counter].enabled = true;
                processImages[counter].sprite = IngredientDataLookupManager.Instance.GetSpriteForProcess(process);
            }
            else
            {
                processImages[counter].enabled = false;
            }

            counter++;
        }

        GameObject prefab = IngredientDataLookupManager.Instance.GetPrefabForProductType(Product);
        GameObject spawned = Instantiate(prefab, finalProductPreviewParent);
        spawned.transform.localScale = Vector3.one * finalProductScale;
        spawned.transform.rotation = Quaternion.Euler(finalProductRotation);
        SetLayerRecursively(spawned, LayerMask.NameToLayer("UI"));
    }

    public void CompleteItem()
    {
        completionIndicator.SetActive(true);
        StartCoroutine(WaitThenDestroy());
    }

    private IEnumerator WaitThenDestroy()
    {
        yield return new WaitForSeconds(waitBeforeDestroyTime);
        Destroy(gameObject);
    }

    private void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
    
        for(int i = 0; i < obj.transform.childCount; i++)
        {
            SetLayerRecursively(obj.transform.GetChild(i).gameObject, newLayer);
        }
    }
}
