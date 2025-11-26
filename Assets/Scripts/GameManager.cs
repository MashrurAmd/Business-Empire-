using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public Text moneyText;    // assign in inspector
    public Text ageText;      // assign in inspector
    public Text messageText;  // assign in inspector
    public GameObject deathPanel; // assign in inspector
    public Button restartButton;  // assign in inspector

    [Header("Stats")]
    public float money = 0f;         // float-based money
    public float age = 18.000f;      // starts at 18.000
    public float maxAge = 20.000f;   // death at 20.000
    public float ageIncrement = 0.001f; // per action
    public float decimalThreshold = 0.365f; // when decimal reaches this -> increment integer
    public float minMoneyBeforeDeath = -5000f;

    [Header("State")]
    public bool gameOver = false;

    void Start()
    {
        if (deathPanel != null) deathPanel.SetActive(false);
        if (restartButton != null) restartButton.onClick.AddListener(RestartGame);
        UpdateUI();
    }

    // Adds money (positive or negative) AND applies the per-action age increment.
    public void AddMoney(float amount)
    {
        if (gameOver) return;

        money += amount;
        ApplyAgeIncrement(ageIncrement);
        CheckDeath();
        UpdateUI();
    }

    // Public: increase age by one action (0.001)
    public void IncreaseAge()
    {
        if (gameOver) return;

        ApplyAgeIncrement(ageIncrement);
        CheckDeath();
        UpdateUI();
    }

    // Applies arbitrary age increment (can be multiple days * 0.001)
    // Keeps decimal logic: when decimal part >= decimalThreshold => increment whole and reset decimals to .000
    private void ApplyAgeIncrement(float increment)
    {
        age += increment;

        // handle rollover when decimal part reaches threshold
        int whole = Mathf.FloorToInt(age);
        float decimalPart = age - whole;

        if (decimalPart >= decimalThreshold)
        {
            whole += 1;
            age = whole + 0.000f;
        }

        // clamp to max
        if (age > maxAge) age = maxAge;
    }

    // Called when an action should remove multiple life-days (e.g. bottle return loses 1-5 days)
    // amount is integer number of days lost
    public void AddLifeLoss(int amount)
    {
        if (gameOver) return;

        if (amount <= 0) return;

        // each "day" in your decimal system = ageIncrement (0.001)
        float totalIncrement = amount * ageIncrement;
        ApplyAgeIncrement(totalIncrement);

        CheckDeath();
        UpdateUI();
    }

    // Global message printer
    public void PrintMessage(string message)
    {
        if (messageText != null) messageText.text = message;
    }

    // Update money and age UI
    public void UpdateUI()
    {
        if (moneyText != null) moneyText.text = "Money: $" + money.ToString("F2");
        if (ageText != null) ageText.text = "Age: " + age.ToString("F3");
    }

    // Death checks
    private void CheckDeath()
    {
        if (gameOver) return;

        if (age >= maxAge)
        {
            HandleDeath("You have reached the end of your life.");
        }
        else if (money <= minMoneyBeforeDeath)
        {
            HandleDeath("You went bankrupt!");
        }
    }

    // Trigger death state
    private void HandleDeath(string deathMessage)
    {
        gameOver = true;
        PrintMessage(deathMessage);

        if (deathPanel != null) deathPanel.SetActive(true);

        // Optionally disable other UI interactions (EarningPanelManager should check gameOver)
    }

    // Restart the whole game (reset stats)
    public void RestartGame()
    {
        gameOver = false;
        money = 0f;
        age = 18.000f;

        if (deathPanel != null) deathPanel.SetActive(false);

        PrintMessage("Game restarted!");
        UpdateUI();
    }
}
