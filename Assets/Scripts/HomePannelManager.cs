using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HomePanelManager : MonoBehaviour
{
    [Header("Game Manager Reference")]
    public GameManager gameManager;

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
    //FOOD
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
        gameManager.IncreaseMaxAge(0.001f);
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
        gameManager.IncreaseMaxAge(0.005f);
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
        gameManager.IncreaseMaxAge(0.01f);
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
        gameManager.IncreaseMaxAge(0.03f);
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
        gameManager.IncreaseMaxAge(0.1f);
        gameManager.PrintMessage("Eat until your heart contents.");
    }


    //House 

    //Move in with friends - "This couch is comfortable." , increase max age by 0.01f, price free
    //Room - "You have your own spot in the fridge." , increase max age by 0.05f, price 
    //Apartment - "Finally your own bathroom." , increase max age by 0.1f
    //Condo - "Sweet Ownership." , increase max age by 2.00f
    //House - "Upstairs and downstairs?!" , increase max age by 10.00f
    //Mansion - "More space than you can ever need." , increase max age by 30.00f, price 100000f



    //Insurance 

    //Health Insurance - "This couch is comfortable." , increase max age by 0.01f, price free
    //Car Insurance - "You have your own spot in the fridge." , increase max age by 0.05f, price 
    //Home Insurance - "Finally your own bathroom." , increase max age by 0.1f
    //Life Insurance - "Sweet Ownership." , increase max age by 2.00f
    //Employee Insurance - "Upstairs and downstairs?!" , increase max age by 10.00f



    //HealthCare 

    //Emergency Room - "This couch is comfortable." , increase max age by 0.01f, price free
    //Dentist - "You have your own spot in the fridge." , increase max age by 0.05f, price 
    //Optimetrist - "Finally your own bathroom." , increase max age by 0.1f
    //Clinic - "Sweet Ownership." , increase max age by 2.00f
    //Annual Visit - "Upstairs and downstairs?!" , increase max age by 10.00f
    //Home Visit - "More space than you can ever need." , increase max age by 30.00f, price 100000f


    //Travel 

    //Weekend Getaway - "This couch is comfortable." , increase max age by 0.01f, price free
    //Cruise - "You have your own spot in the fridge." , increase max age by 0.05f, price 
    //All Inclusive - "Finally your own bathroom." , increase max age by 0.1f
    //Safari - "Sweet Ownership." , increase max age by 2.00f
    //Overseas - "Upstairs and downstairs?!" , increase max age by 10.00f


    //Spirituality 

    //Spiritual App - "This couch is comfortable." , increase max age by 0.01f, price free
    //Meditate - "You have your own spot in the fridge." , increase max age by 0.05f, price 
    //Pray - "Finally your own bathroom." , increase max age by 0.1f
    //House of Worship - "Sweet Ownership." , increase max age by 2.00f
    //Spiritual Retreat - "Upstairs and downstairs?!" , increase max age by 10.00f


    //Family 

    //Date - "This couch is comfortable." , increase max age by 0.01f, price free
    //Engagement - "You have your own spot in the fridge." , increase max age by 0.05f, price 
    //Marriage - "Finally your own bathroom." , increase max age by 0.1f
    //Baby - "Sweet Ownership." , increase max age by 2.00f
    //Twins - "Upstairs and downstairs?!" , increase max age by 10.00f













}
