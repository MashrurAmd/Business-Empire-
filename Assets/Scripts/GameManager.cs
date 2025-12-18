using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public Text moneyText;
    public Text ageText;                 // ✅ Current Age UI
    public Text lifeExpectancyText;     // ✅ NEW: Max Age UI
    public Text messageText;
    public Text reputationText;
    public GameObject deathPanel;
    public Button restartButton;

    [Header("Stats")]
    public float money = 0f;
    public float age = 18.000f;
    public float maxAge = 20.000f;      // ✅ Life Expectancy
    public float ageIncrement = 0.001f;
    public float decimalThreshold = 0.365f;
    public float minMoneyBeforeDeath = -5000f;
    

    [Header("State")]
    public bool gameOver = false;

    [Header("Enhancements")]
    public bool hasLocker = false;
    public bool hasMask = false;
    public bool hasCup = false;
    public bool hasFlower = false;
    public bool hasCart = false;

    public float lockerUpkeepTimer = 0f;
    public float lockerUpkeepInterval = 600f;
    public int maskClicksRemaining = 0;
    public int cupClicksRemaining = 0;
    public int cartClicksRemaining = 0;

    [Header("Resources Unlocks")]
    public bool hasCartboard = false;
    public bool hasKnife = false;
    public bool hasPhone = false;

    [Header("Robbery System")]
    public bool robberyEnabled = true;
    private float robberyTimer = 0f;
    private float nextRobberyTime = 0f;

    [Header("Reputation")]
    public float reputation = 0f;


    [Header("Education Flags")]
    public bool wentToTradeSchool = false;

    public bool hasGED = false;
    public bool hasBachelors = false;
    public bool hasMBA = false;
    public bool hasPHD = false;
    public bool hasMD = false;

    [Header("Exams Passed")]
    public bool passedGEDExam = false;
    public bool passedUndergradExam = false;
    public bool passedMBAExam = false;
    public bool passedPHDExam = false;
    public bool passedBarExam = false;
    public bool passedMDExam = false;



    void Start()
    {
        if (deathPanel != null) deathPanel.SetActive(false);
        if (restartButton != null) restartButton.onClick.AddListener(RestartGame);

        UpdateUI();
        ScheduleNextRobbery();
    }

    // ----------------- MONEY + AGE SYSTEM -----------------
    public void AddMoney(float amount)
    {
        if (gameOver) return;

        money += amount;
        ApplyAgeIncrement(ageIncrement);
        CheckDeath();
        UpdateUI();
    }

    public void IncreaseAge()
    {
        if (gameOver) return;

        ApplyAgeIncrement(ageIncrement);
        CheckDeath();
        UpdateUI();
    }

    // ----------------- REPUTATION SYSTEM -----------------
    public void AddReputation(float amount)
    {
        reputation += amount;
        reputation = Mathf.Max(0, reputation);
        UpdateUI(); // 🔥 THIS LINE WAS MISSING
    }




    // ✅ THIS IS ONLY FOR LIFE EXPECTANCY (MAX AGE)
    public void IncreaseMaxAge(float amount)
    {
        maxAge += amount;
        Debug.Log("Max Age increased by " + amount + ". New Max Age: " + maxAge);
        UpdateUI();
    }

    private void ApplyAgeIncrement(float increment)
    {
        age += increment;

        int whole = Mathf.FloorToInt(age);
        float decimalPart = age - whole;

        if (decimalPart >= decimalThreshold)
        {
            whole += 1;
            age = whole + 0.000f;
        }

        if (age > maxAge)
            age = maxAge;
    }

    public void AddLifeLoss(int amount)
    {
        if (gameOver || amount <= 0) return;

        float totalIncrement = amount * ageIncrement;
        ApplyAgeIncrement(totalIncrement);

        CheckDeath();
        UpdateUI();
    }

    // ----------------- UI + DEATH SYSTEM -----------------
    public void PrintMessage(string msg)
    {
        if (messageText != null) messageText.text = msg;
    }

    public void UpdateUI()
    {
        if (moneyText)
            

            moneyText.text = "Money: $" + FormatMoney(money);


        if (ageText)
            ageText.text = "Age: " + age.ToString("F3");  // ✅ Current Age Only

        if (lifeExpectancyText)
            lifeExpectancyText.text = "Life Expectancy: " + maxAge.ToString("F3"); // ✅ Max Age Only

        if (reputationText)
            reputationText.text = "Reputation: " + reputation.ToString("F1");

    }

    private void CheckDeath()
    {
        if (gameOver) return;

        if (age >= maxAge)
            HandleDeath("You have reached the end of your life.");

        if (money <= minMoneyBeforeDeath)
            HandleDeath("You went bankrupt!");
    }

    private void HandleDeath(string msg)
    {
        gameOver = true;
        PrintMessage(msg);

        if (deathPanel != null)
            deathPanel.SetActive(true);
    }

    public void RestartGame()
    {
        gameOver = false;
        money = 0f;
        age = 18.000f;
        maxAge = 20.000f; // ✅ Reset Life Expectancy

        hasLocker = false;
        hasMask = false;
        hasCup = false;
        hasFlower = false;
        hasCart = false;

        hasCartboard = false;
        hasKnife = false;
        hasPhone = false;

        maskClicksRemaining = 0;
        cupClicksRemaining = 0;
        cartClicksRemaining = 0;

        if (deathPanel != null)
            deathPanel.SetActive(false);

        PrintMessage("Game restarted!");
        UpdateUI();
        ScheduleNextRobbery();
    }

    // ----------------- UPDATE LOOP -----------------
    void Update()
    {
        if (hasLocker)
        {
            lockerUpkeepTimer += Time.deltaTime;
            if (lockerUpkeepTimer >= lockerUpkeepInterval)
            {
                lockerUpkeepTimer = 0f;
                AddMoney(-1f);
                PrintMessage("Locker upkeep cost -$1");
            }
        }

        if (robberyEnabled && !gameOver)
        {
            robberyTimer += Time.deltaTime;
            if (robberyTimer >= nextRobberyTime)
            {
                robberyTimer = 0f;
                TriggerRobbery();
                ScheduleNextRobbery();
            }
        }
    }

    // ----------------- ROBBERY -----------------
    private void ScheduleNextRobbery()
    {
        nextRobberyTime = Random.Range(300f, 900f);
    }

    public void TriggerRobbery()
    {
        if (hasLocker)
        {
            PrintMessage("Robbers tried to steal from you but found nothing.");
        }
        else
        {
            money = 0;
            UpdateUI();
            PrintMessage("You were robbed! All money is gone.");
        }
    }


    public string FormatMoney(double amount)
    {
        if (amount >= 1_000_000_000)
            return (amount / 1_000_000_000d).ToString("0.#") + "B";
        if (amount >= 1_000_000)
            return (amount / 1_000_000d).ToString("0.#") + "M";
        if (amount >= 1_000)
            return (amount / 1_000d).ToString("0.#") + "K";

        return amount.ToString("0");
    }

}
