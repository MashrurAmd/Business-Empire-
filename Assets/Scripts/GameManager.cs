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

    // ===== ENHANCEMENTS =====
    public bool hasLocker = false;
    public float lockerUpkeepTimer = 0f;
    public float lockerUpkeepInterval = 600f;

    public bool hasMask = false;
    public int maskUses = 0;

    public bool hasCup = false;
    public int cupUses = 0;

    public bool hasFlower = false;

    public bool hasCart = false;
    public int cartUses = 0;


    void Start()
    {
        if (restartButton != null) restartButton.onClick.AddListener(RestartGame);
        if (deathPanel != null) deathPanel.SetActive(false);
        UpdateUI();
    }

    // ===== MONEY =====
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

    // Converts decimal age to integer when threshold reached
    private void ApplyAgeIncrement(float increment)
    {
        age += increment;
        int whole = Mathf.FloorToInt(age);
        float dec = age - whole;

        if (dec >= decimalThreshold)
        {
            whole += 1;
            age = whole + 0.000f;
        }
        if (age > maxAge) age = maxAge;
    }

    public void AddLifeLoss(int amount)
    {
        if (amount <= 0 || gameOver) return;

        float total = amount * ageIncrement;
        ApplyAgeIncrement(total);
        CheckDeath();
        UpdateUI();
    }

    // ===== BASIC UI =====
    public void PrintMessage(string msg) => messageText.text = msg;

    public void UpdateUI()
    {
        moneyText.text = $"Money: ${money:F2}";
        ageText.text = $"Age: {age:F3}";
    }

    // ===== DEATH =====
    private void CheckDeath()
    {
        if (age >= maxAge) HandleDeath("You lived your life fully.");
        else if (money <= minMoneyBeforeDeath) HandleDeath("You died broke.");
    }

    private void HandleDeath(string msg)
    {
        gameOver = true;
        PrintMessage(msg);
        deathPanel.SetActive(true);
    }

    public void RestartGame()
    {
        money = 0f;
        age = 18.000f;
        gameOver = false;

        // reset everything
        hasLocker = false;
        hasMask = false;
        hasCup = false;
        hasFlower = false;
        hasCart = false;

        deathPanel.SetActive(false);
        PrintMessage("Restarted.");
        UpdateUI();
    }

    // ===== LOCKER ROBBERY PROTECTION =====
    public void TriggerRobbery()
    {
        if (hasLocker) PrintMessage("Thieves found nothing — locker protected you.");
        else { money = 0; UpdateUI(); PrintMessage("You got robbed. Lost everything."); }
    }

    void Update()
    {
        if (hasLocker)
        {
            lockerUpkeepTimer += Time.deltaTime;
            if (lockerUpkeepTimer >= lockerUpkeepInterval)
            {
                lockerUpkeepTimer = 0;
                AddMoney(-1f);
                PrintMessage("Locker upkeep cost -$1.");
            }
        }
    }
}
