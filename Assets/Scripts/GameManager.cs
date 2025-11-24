using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Global UI References")]
    public Text moneyText;       // UI Text to show current money
    public Text ageText;         // UI Text to show current age
    public Text messageText;     // UI Text to show messages

    [Header("Player Stats")]
    public int money = 0;
    public float age = 18.000f;      // Starts at 18.000
    public float maxAge = 20.000f;   // Ends at 20.000
    public float ageIncrement = 0.001f; // Each button click increases by 0.001

    void Start()
    {
        UpdateUI();
    }

    // Call this whenever the player earns money
    public void AddMoney(int amount)
    {
        money += amount;
        IncreaseAge();
        UpdateUI();
    }

    // Call this if only life/age decreases (without earning)
    public void SubtractAge(float increment = 0.001f)
    {
        age += increment;
        if (age >= maxAge)
        {
            age = maxAge;
            GameOver();
        }
        UpdateUI();
    }

    public void IncreaseAge()
    {
        age += ageIncrement;

        // Check if decimal part reached 0.365
        int whole = Mathf.FloorToInt(age);
        float decimalPart = age - whole;

        if (decimalPart >= 0.365f)
        {
            // Increase integer part
            whole += 1;
            age = whole + 0.000f; // reset decimal
        }

        // Check max age
        if (age >= maxAge)
        {
            age = maxAge;
            GameOver();
        }
    }

    public void PrintMessage(string message)
    {
        messageText.text = message;
    }

    void UpdateUI()
    {
        moneyText.text = "Money: $" + money;
        ageText.text = "Age: " + age.ToString("F3"); // Show 3 decimals
    }

    void GameOver()
    {
        PrintMessage("You have reached your life limit. Game Over!");
        // Optional: disable buttons or show end panel
    }
}
