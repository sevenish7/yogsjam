using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class GameMode : SerializedMonoBehaviour
{
    public static GameMode Instance { get; private set; }

    [SerializeField] private GameTimer timer; 
    [SerializeField] private EndPanel endPanel;
    [SerializeField] private NiceListManager niceListManager;

    [SerializeField] private float niceListPointsMultiplier = 1.5f;
    [SerializeField] private float niceListTimeMultiplier = 2f;
    [OdinSerialize] private Dictionary<ProductType, System.Tuple<int, float>> baseRewardsForProduct = new Dictionary<ProductType, System.Tuple<int, float>>();

    public GameTimer Timer { get { return timer; } }

    public int Points { get; private set; }

    private bool isPaused = false;

    private void Awake()
    {
        if(Instance == null || Instance != this)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        timer.Initialise(GameComplete);
        niceListManager.Initialise();
    }

    private void GameComplete()
    {
        endPanel.Show();
    }

    public void DeliverProduct(ProductType product)
    {
        if(baseRewardsForProduct.ContainsKey(product))
        {
            var baseRewards = baseRewardsForProduct[product];
            int earntPoints = baseRewards.Item1;
            float earntTime = baseRewards.Item2;
            if(isNiceListItem(product))
            {
                earntPoints = Mathf.FloorToInt(earntPoints * niceListPointsMultiplier);
                earntTime *= niceListTimeMultiplier;

                niceListManager.CompleteItemInList(product);
            }

            Points += earntPoints;
            timer.AddTime(earntTime);
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void UnpauseGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }

    private bool isNiceListItem(ProductType product)
    {
        return niceListManager.IsProductInNiceList(product);
    }
}
