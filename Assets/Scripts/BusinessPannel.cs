using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class BusinessManager : MonoBehaviour
{
    [Header("References")]
    public GameManager gameManager;

    [Header("Income Settings")]
    public float incomeIntervalSeconds = 600f; // CHANGE LATER
    private float timer;

    [Header("Businesses")]
    public List<BusinessData> businesses = new List<BusinessData>();

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= incomeIntervalSeconds)
        {
            timer = 0f;
            PayIncome();
        }
    }

    void PayIncome()
    {
        double totalIncome = 0;

        foreach (BusinessData b in businesses)
        {
            if (b.owned)
                totalIncome += b.incomeAmount;
        }

        if (totalIncome > 0)
        {
            gameManager.AddMoney(totalIncome);
            gameManager.PrintMessage(
                $"Your businesses generated ${gameManager.FormatMoney(totalIncome)}"
            );
        }
    }

    public void BuyBusiness(string businessId)
    {
        BusinessData b = businesses.Find(x => x.id == businessId);

        if (b == null)
        {
            Debug.LogError("Business not found: " + businessId);
            return;
        }

        if (b.owned)
        {
            gameManager.PrintMessage("You already own this.");
            return;
        }

        if (gameManager.money < b.cost)
        {
            gameManager.PrintMessage("Not enough money.");
            return;
        }

        gameManager.AddMoney(-b.cost);
        b.owned = true;

        gameManager.PrintMessage($"Purchased {b.displayName}");
    }
}
