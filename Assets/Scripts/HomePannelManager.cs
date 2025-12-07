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

    //Health Insurance - "Healthy as a horse." , increase max age by 0.01f, price free
    //Car Insurance - "Protect your wheels." , increase max age by 0.05f, price 
    //Home Insurance - "Protect your home." , increase max age by 0.1f
    //Life Insurance - "Protect your life.", increase max age by 2.00f
    //Employee Insurance - "Healthy employees support healthy business." , increase max age by 10.00f



    //HealthCare 

    //Emergency Room - "Hopefully the wait isn't too long." , increase max age by 0.01f, price free
    //Dentist - "Pearly whites" , increase max age by 0.05f, price 
    //Optimetrist - "E, X, D, Q, Y" , increase max age by 0.1f
    //Clinic - "All vitals are in order." , increase max age by 2.00f
    //Annual Visit - "Healthy as a horse." , increase max age by 10.00f
    //Home Visit - "Health care for the rich and famous." , increase max age by 30.00f, price 100000f


    //Travel 

    //Weekend Getaway - "Weekend away." , increase max age by 0.01f, price free
    //Cruise - "Site seeing on the seas." , increase max age by 0.05f, price 
    //All Inclusive - "Rest, relax and repeat." , increase max age by 0.1f
    //Safari - "Natures beauty." , increase max age by 2.00f
    //Overseas - "High life abroad.", increase max age by 10.00f


    //Spirituality 

    //Spiritual App - "Spirituality in your pocket." , increase max age by 0.01f, price free
    //Meditate - "Breathe in and breathe out." , increase max age by 0.05f, price 
    //Pray - "Pray in your peace." , increase max age by 0.1f
    //House of Worship - "Hear the word" , increase max age by 2.00f
    //Spiritual Retreat - "Be with others." , increase max age by 10.00f


    //Family 

    //Date - "That was fun." , increase max age by 0.01f, price free
    //Engagement - "Let's make this official." , increase max age by 0.05f, price 
    //Marriage - "Newlyweds." , increase max age by 0.1f
    //Baby - "First born." , increase max age by 2.00f
    //Twins - "Double the trouble." , increase max age by 10.00f













}
