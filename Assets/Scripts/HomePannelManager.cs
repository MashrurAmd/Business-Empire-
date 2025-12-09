using UnityEngine;
using UnityEngine.UI;

public class HomePanelManager : MonoBehaviour
{
    [Header("Game Manager Reference")]
    public GameManager gameManager;

    // ================== FOOD ==================
    public Button homeCookingButton;
    public Button orderMealButton;
    public Button mealPrepServiceButton;
    public Button feastButton;
    public Button personalChefButton;

    // ================== HOUSE ==================
    public Button moveInWithFriendsButton;
    public Button roomButton;
    public Button apartmentButton;
    public Button condoButton;
    public Button houseButton;
    public Button mansionButton;

    // ================== INSURANCE ==================
    public Button healthInsuranceButton;
    public Button carInsuranceButton;
    public Button homeInsuranceButton;
    public Button lifeInsuranceButton;
    public Button employeeInsuranceButton;

    // ================== HEALTHCARE ==================
    public Button emergencyRoomButton;
    public Button dentistButton;
    public Button optometristButton;
    public Button clinicButton;
    //public Button annualVisitButton;
    public Button homeVisitButton;

    // ================== TRAVEL ==================
    public Button weekendGetawayButton;
    public Button cruiseButton;
    public Button allInclusiveButton;
    public Button safariButton;
    public Button overseasButton;

    // ================== SPIRITUALITY ==================
    public Button spiritualAppButton;
    public Button meditateButton;
    public Button prayButton;
    public Button houseOfWorshipButton;
    public Button spiritualRetreatButton;

    // ================== FAMILY ==================
    public Button dateButton;
    public Button engagementButton;
    public Button marriageButton;
    public Button babyButton;
    public Button twinsButton;

    void Start()
    {
        // FOOD
        homeCookingButton.onClick.AddListener(HomeCooking);
        orderMealButton.onClick.AddListener(OrderMeal);
        mealPrepServiceButton.onClick.AddListener(MealPrepService);
        feastButton.onClick.AddListener(HaveAFeast);
        personalChefButton.onClick.AddListener(PersonalChef);

        // HOUSE
        moveInWithFriendsButton.onClick.AddListener(MoveInWithFriends);
        roomButton.onClick.AddListener(Room);
        apartmentButton.onClick.AddListener(Apartment);
        condoButton.onClick.AddListener(Condo);
        houseButton.onClick.AddListener(House);
        mansionButton.onClick.AddListener(Mansion);

        // INSURANCE
        healthInsuranceButton.onClick.AddListener(HealthInsurance);
        carInsuranceButton.onClick.AddListener(CarInsurance);
        homeInsuranceButton.onClick.AddListener(HomeInsurance);
        lifeInsuranceButton.onClick.AddListener(LifeInsurance);
        employeeInsuranceButton.onClick.AddListener(EmployeeInsurance);

        // HEALTHCARE
        emergencyRoomButton.onClick.AddListener(EmergencyRoom);
        dentistButton.onClick.AddListener(Dentist);
        optometristButton.onClick.AddListener(Optometrist);
        clinicButton.onClick.AddListener(Clinic);
        homeVisitButton.onClick.AddListener(HomeVisit);

        // TRAVEL
        weekendGetawayButton.onClick.AddListener(WeekendGetaway);
        cruiseButton.onClick.AddListener(Cruise);
        allInclusiveButton.onClick.AddListener(AllInclusive);
        safariButton.onClick.AddListener(Safari);
        overseasButton.onClick.AddListener(Overseas);

        // SPIRITUALITY
        spiritualAppButton.onClick.AddListener(SpiritualApp);
        meditateButton.onClick.AddListener(Meditate);
        prayButton.onClick.AddListener(Pray);
        houseOfWorshipButton.onClick.AddListener(HouseOfWorship);
        spiritualRetreatButton.onClick.AddListener(SpiritualRetreat);

        // FAMILY
        dateButton.onClick.AddListener(Date);
        engagementButton.onClick.AddListener(Engagement);
        marriageButton.onClick.AddListener(Marriage);
        babyButton.onClick.AddListener(Baby);
        twinsButton.onClick.AddListener(Twins);
    }

    bool CanAct()
    {
        return gameManager != null && !gameManager.gameOver;
    }

    void Buy(float cost, float lifeIncrease, float reputationGain, string msg)
    {
        if (!CanAct()) return;

        if (gameManager.money < cost)
        {
            gameManager.PrintMessage("Not enough money.");
            return;
        }

        gameManager.AddMoney(-cost);
        gameManager.IncreaseMaxAge(lifeIncrease);
        gameManager.AddReputation(reputationGain);
        gameManager.PrintMessage(msg);
    }


    // ================= FOOD =================
    public void HomeCooking() => Buy(15f, 0.001f,0f, "You cooked a meal.");
    public void OrderMeal() => Buy(45f, 0.005f, 0f, "You ordered from your favorite spot.");
    public void MealPrepService() => Buy(100f, 0.01f, 0f, "Your meals arrived at your door, ready to eat!");
    public void HaveAFeast() => Buy(500f, 0.03f, 0f, "This is the high life, preped meals for all occasions.");
    public void PersonalChef() => Buy(5000f, 0.1f, 0f, "Eat until your heart contents.");

    // ================= HOUSE =================
    public void MoveInWithFriends() => Buy(2f, 0.01f, 2f, "This couch is comfortable.");
    public void Room() => Buy(500f, 0.05f, 2f, "You have your own spot in the fridge.");
    public void Apartment() => Buy(5000f, 0.1f, 5f, "Finally your own bathroom.");
    public void Condo() => Buy(20000f, 2f, 10f, "Sweet Ownership.");
    public void House() => Buy(50000f, 10f, 20f, "Upstairs and downstairs?!");
    public void Mansion() => Buy(100000f, 30f, 50f, "More space than you can ever need.");

    // ================= INSURANCE =================
    public void HealthInsurance() => Buy(0f, 0.01f, 0f, "Healthy as a horse.");
    public void CarInsurance() => Buy(500f, 0.05f, 0f, "Protect your wheels.");
    public void HomeInsurance() => Buy(1500f, 0.1f, 0f, "Protect your home.");
    public void LifeInsurance() => Buy(10000f, 2f, 0f, "Protect your life.");
    public void EmployeeInsurance() => Buy(50000f, 10f, 0f, "Healthy employees support healthy business.");

    // ================= HEALTHCARE =================
    public void EmergencyRoom() => Buy(0f, 0.01f, 0f, "Hopefully the wait isn't too long.");
    public void Dentist() => Buy(200f, 0.05f, 0f, "Pearly whites");
    public void Optometrist() => Buy(400f, 0.1f, 0f, "E, X, D, Q, Y");
    public void Clinic() => Buy(2000f, 2f, 0f, "All vitals are in order.");
    public void AnnualVisit() => Buy(10000f, 10f, 0f, "Healthy as a horse.");
    public void HomeVisit() => Buy(100000f, 30f, 0f, "Health care for the rich and famous.");

    // ================= TRAVEL =================
    public void WeekendGetaway() => Buy(0f, 0.01f, 0f, "Weekend away.");
    public void Cruise() => Buy(800f, 0.05f, 0f, "Site seeing on the seas.");
    public void AllInclusive() => Buy(2000f, 0.1f, 0f, "Rest, relax and repeat.");
    public void Safari() => Buy(15000f, 2f, 0f, "Natures beauty.");
    public void Overseas() => Buy(50000f, 10f, 0f, "High life abroad.");

    // ================= SPIRITUALITY =================
    public void SpiritualApp() => Buy(0f, 0.01f, 0f, "Spirituality in your pocket.");
    public void Meditate() => Buy(250f, 0.05f, 0f, "Breathe in and breathe out.");
    public void Pray() => Buy(100f, 0.1f, 0f, "Pray in your peace.");
    public void HouseOfWorship() => Buy(2000f, 2f, 0f, "Hear the word");
    public void SpiritualRetreat() => Buy(10000f, 10f, 0f, "Be with others.");

    // ================= FAMILY =================
    public void Date() => Buy(0f, 0.01f, 0f, "That was fun.");
    public void Engagement() => Buy(1000f, 0.05f, 0f, "Let's make this official.");
    public void Marriage() => Buy(5000f, 0.1f, 0f, "Newlyweds.");
    public void Baby() => Buy(20000f, 2f, 0f, "First born.");
    public void Twins() => Buy(50000f, 10f, 0f, "Double the trouble.");
}
