using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AssemblyStation : BaseStation
{

    [SerializeField] private ProductPickup productPrefab;

    protected override void FinishProcess()
    {
        base.FinishProcess();

        ProductType product = IngredientDataLookupManager.Instance.GetProductTypeForRecipe(containedIngredients.Select(i => i.ProcessedIngredient));
        ProductPickup pickup = Instantiate(productPrefab, finalProductSpawnTransform.position, finalProductSpawnTransform.rotation);
        pickup.InitialiseProductData(product);

        containedIngredients.Clear();
    }

}
