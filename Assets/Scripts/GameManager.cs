using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public Text moneyText;
    public Text ageText;
    public Text messageText;
    public GameObject deathPanel;
    public Button restartButton;

    [Header("Stats")]
    public float money = 0f;
    public float age = 18.000f;
    public float maxAge = 20.000f;
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
    public float lockerUpkeepInterval = 600f; // 10 min
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

    public void IncreaseMaxAge()
    {
        maxAge += 0.001f;
        Debug.Log(maxAge);
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

        if (age > maxAge) age = maxAge;
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
        if (moneyText) moneyText.text = "Money: $" + money.ToString("F2");
        if (ageText) ageText.text = "Age: " + age.ToString("F3");
    }

    private void CheckDeath()
    {
        if (gameOver) return;

        if (age >= maxAge) HandleDeath("You have reached the end of your life.");
        if (money <= minMoneyBeforeDeath) HandleDeath("You went bankrupt!");
    }

    private void HandleDeath(string msg)
    {
        gameOver = true;
        PrintMessage(msg);

        if (deathPanel != null) deathPanel.SetActive(true);
    }

    public void RestartGame()
    {
        gameOver = false;
        money = 0f;
        age = 18.000f;

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

        if (deathPanel != null) deathPanel.SetActive(false);

        PrintMessage("Game restarted!");
        UpdateUI();
        ScheduleNextRobbery();
    }

    // ----------------- UPDATE LOOP -----------------
    void Update()
    {
        // Locker upkeep every 10 min
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

        // Auto Robbery
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
        nextRobberyTime = Random.Range(300f, 900f); // 5–15 minutes
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


    public void IncreaseMaxAge(float amount)
    {
        maxAge += amount;
        Debug.Log("Max Age increased by " + amount + ". New Max Age: " + maxAge);
    }


}
