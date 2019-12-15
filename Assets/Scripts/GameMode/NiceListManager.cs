using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NiceListManager : MonoBehaviour
{
    // [SerializeField] private List<System.Tuple<ProductType, float>> productChanceOfAppearing = new List<System.Tuple<ProductType, float>>();
    [SerializeField] private int numItemsInListAtOnce = 3;
    [SerializeField] private Transform niceListUIParent;
    [SerializeField] private NiceListUIItem uiItemPrefab;
    [SerializeField] private float listCooldown = 2f;

    private List<ProductType> currentProductsInList = new List<ProductType>();
    private List<NiceListUIItem> spawnedNiceListUI = new List<NiceListUIItem>();

    private WaitForSeconds yieldInstruction;

    private void Awake()
    {
        yieldInstruction = new WaitForSeconds(listCooldown);
    }

    public void Initialise()
    {
        BuildNewList();
    }

    public void CompleteItemInList(ProductType p)
    {
        currentProductsInList.Remove(p);

        RemoveUIItem(p);

        if(currentProductsInList.Count == 0)
        {
            StartCoroutine(WaitThenBuild());
        }
    }

    public bool IsProductInNiceList(ProductType p)
    {
        return currentProductsInList.Contains(p);
    }

    private void RemoveUIItem(ProductType p)
    {
        NiceListUIItem item = spawnedNiceListUI.Find(i => i.Product == p);
        if(item != null)
        {
            item.CompleteItem();
        }
    }

    private void BuildNewList()
    {
        currentProductsInList.Clear();
        spawnedNiceListUI.Clear();
        
        for(int i = 0; i < numItemsInListAtOnce; i++)
        {
            ProductType p = GetRandomProductForNiceList();
            currentProductsInList.Add(p);
        }

        SpawnUIForList();
    }

    private void SpawnUIForList()
    {
        int count = 0;
        foreach(ProductType p in currentProductsInList)
        {
            NiceListUIItem item = Instantiate(uiItemPrefab, niceListUIParent);
            item.Initialise(p, count);
            spawnedNiceListUI.Add(item);
            count++;
        }
    }

    private IEnumerator WaitThenBuild()
    {
        yield return yieldInstruction;
        BuildNewList();
    }

    private ProductType GetRandomProductForNiceList()
    {
        // float sumOfAllChances = productChanceOfAppearing.Sum(p => p.Item2);

        // float roll = Random.Range(0f, sumOfAllChances);
        // float runningTotal = 0f;

        // foreach(var p in productChanceOfAppearing)
        // {
        //     if(roll < runningTotal + p.Item2)
        //     {
        //         return p.Item1;
        //     }
        //     else
        //     {
        //         runningTotal += p.Item2;
        //     }
        // }

        var values = System.Enum.GetValues(typeof(ProductType));

        int random = Random.Range(0, values.Length - 1);//-1 because garbage is a product type
        return (ProductType)values.GetValue(random);
    }
}
