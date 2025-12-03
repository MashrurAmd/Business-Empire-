using UnityEngine;
using UnityEngine.UI;

public class HomePanelManager : MonoBehaviour
{
    [Header("Game Manager Reference")]
    public GameManager gameManager;
    public GameObject gameManagerObject;

    [Header("Food Buttons")]
    public Button homeCookingButton;
    public Button orderMealButton;
    public Button mealPrepServiceButton;
    public Button feastButton;
    public Button personalChefButton;

    void Start()
    {
        if (homeCookingButton != null) homeCookingButton.onClick.AddListener(HomeCooking);
        if (orderMealButton != null) orderMealButton.onClick.AddListener(OrderMeal);
        if (mealPrepServiceButton != null) mealPrepServiceButton.onClick.AddListener(MealPrepService);
        if (feastButton != null) feastButton.onClick.AddListener(HaveAFeast);
        if (personalChefButton != null) personalChefButton.onClick.AddListener(PersonalChef);
    }

    bool CanAct()
    {
        return gameManager != null && !gameManager.gameOver;
    }

    // -------------------- HOME COOKING --------------------
    public void HomeCooking()
    {
        if (!CanAct()) return;

        float cost = 15f;
        if (gameManager.money < cost)
        {
            gameManager.PrintMessage("Not enough money to cook a meal.");
            return;
        }

        gameManager.AddMoney(-cost);
        IncreaseLife(0.001f);
        gameManager.PrintMessage("You cooked a meal.");
    }

    // -------------------- ORDER MEAL --------------------
    public void OrderMeal()
    {
        if (!CanAct()) return;

        float cost = 45f;
        if (gameManager.money < cost)
        {
            gameManager.PrintMessage("Not enough money to order food.");
            return;
        }

        gameManager.AddMoney(-cost);
        IncreaseLife(0.005f);
        gameManager.PrintMessage("You ordered from your favorite spot.");
    }

    // -------------------- MEAL PREP SERVICE --------------------
    public void MealPrepService()
    {
        if (!CanAct()) return;

        float cost = 100f;
        if (gameManager.money < cost)
        {
            gameManager.PrintMessage("Not enough money to buy meal prep service.");
            return;
        }

        gameManager.AddMoney(-cost);
        IncreaseLife(0.01f);
        gameManager.PrintMessage("Your meals arrived at your door, ready to eat!");
    }

    // -------------------- FEAST --------------------
    public void HaveAFeast()
    {
        if (!CanAct()) return;

        float cost = 500f;
        if (gameManager.money < cost)
        {
            gameManager.PrintMessage("Not enough money to have a feast.");
            return;
        }

        gameManager.AddMoney(-cost);
        IncreaseLife(0.03f);
        gameManager.PrintMessage("This is the high life, preped meals for all occasions.");
    }

    // -------------------- PERSONAL CHEF --------------------
    public void PersonalChef()
    {
        if (!CanAct()) return;

        float cost = 5000f;
        if (gameManager.money < cost)
        {
            gameManager.PrintMessage("Not enough money to hire a personal chef.");
            return;
        }

        gameManager.AddMoney(-cost);
        IncreaseLife(0.1f);
        gameManager.PrintMessage("Eat until your heart contents.");
    }

    // -------------------- LIFE INCREASE LOGIC --------------------
    private void IncreaseLife(float amount)
    {
        // Uses the GameManager’s existing ApplyAgeIncrement logic
        gameManager.age += amount;

        // Ensure the decimal rollover logic + death checks are applied
        int whole = Mathf.FloorToInt(gameManager.age);
        float decimalPart = gameManager.age - whole;

        if (decimalPart >= gameManager.decimalThreshold)
        {
            whole += 1;
            gameManager.age = whole + 0.000f;
        }

        if (gameManager.age > gameManager.maxAge)
            gameManager.age = gameManager.maxAge;

        gameManager.UpdateUI();
    }
}
