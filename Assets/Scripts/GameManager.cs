using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Global UI References")]
    public Text moneyText;       // UI Text to show current money
    public Text lifeText;        // UI Text to show current life (days/age)
    public Text messageText;     // UI Text to show messages

    [Header("Player Stats")]
    public int money = 0;
    public int lifeDays = 730;  // 2 years = 730 days
    public int startAge = 18;

    void Start()
    {
        UpdateUI();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        lifeDays--; // Each action costs 1 day
        UpdateUI();
    }

    public void SubtractLife(int days = 1)
    {
        lifeDays -= days;
        UpdateUI();
    }

    public void PrintMessage(string message)
    {
        messageText.text = message;
    }

    void UpdateUI()
    {
        moneyText.text = "Money: $" + money;
        int age = startAge + (730 - lifeDays) / 365;
        lifeText.text = "Age: " + age + " (" + lifeDays + " days left)";
    }
}
