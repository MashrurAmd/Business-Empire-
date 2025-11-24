using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Global UI References")]
    public Text moneyText;
    public Text ageText;
    public Text messageText;
    public GameObject deathPanel;    // Assign your Death Panel here
    public Button restartButton;     // Assign Restart button

    [Header("Player Stats")]
    public int money = 0;
    public float age = 18.000f;
    public float maxAge = 20.000f;
    public float ageIncrement = 0.001f;
    public int minMoney = -5000;    // Minimum money before death

    void Start()
    {
        deathPanel.SetActive(false);    // Hide death panel
        restartButton.onClick.AddListener(RestartGame);
        UpdateUI();
    }

    // Add money and increase age
    public void AddMoney(int amount)
    {
        money += amount;
        IncreaseAge();
        CheckDeath();
        UpdateUI();
    }

    // Increase age by 0.001
    public void IncreaseAge()
    {
        age += ageIncrement;

        int whole = Mathf.FloorToInt(age);
        float decimalPart = age - whole;

        if (decimalPart >= 0.365f)
        {
            whole += 1;
            age = whole + 0.000f; // reset decimal
        }

        CheckDeath();
        UpdateUI();
    }

    // Print global messages
    public void PrintMessage(string message)
    {
        messageText.text = message;
    }

    // Update UI
    public void UpdateUI()
    {
        moneyText.text = "Money: $" + money;
        ageText.text = "Age: " + age.ToString("F3");
    }

    // Check if player is dead
    void CheckDeath()
    {
        if (age >= maxAge)
        {
            Death("You have reached the end of your life.");
        }
        else if (money <= minMoney)
        {
            Death("You went bankrupt!");
        }
    }

    // Handle death
    void Death(string deathMessage)
    {
        PrintMessage(deathMessage);
        deathPanel.SetActive(true);

        // Optional: disable other buttons in EarningPanelManager
        // Or set a bool like 'gameOver = true' to stop clicks
    }

    // Restart game
    void RestartGame()
    {
        money = 0;
        age = 18.000f;
        deathPanel.SetActive(false);
        PrintMessage("Game restarted!");
        UpdateUI();
    }
}
