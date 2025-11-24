using UnityEngine;
using UnityEngine.UI;

public class EarningPanelManager : MonoBehaviour
{
    [Header("Game Manager Reference")]
    public GameManager gameManager;

    [Header("Earning Panel Buttons")]
    public Button begButton;
    public Button stealButton;
    public Button bottleReturnButton;
    public Button scammerButton;
    public Button messengerButton;

    public void Start()
    {
        // Assign button listeners
        begButton.onClick.AddListener(OnBegClicked);
        stealButton.onClick.AddListener(OnStealClicked);

        // Placeholder listeners for other buttons
        bottleReturnButton.onClick.AddListener(() => gameManager.PrintMessage("Bottle Return clicked!"));
        scammerButton.onClick.AddListener(() => gameManager.PrintMessage("Scammer clicked!"));
        messengerButton.onClick.AddListener(() => gameManager.PrintMessage("Messenger clicked!"));
    }

    // ------------------- Beg Button -------------------
    public void OnBegClicked()
    {
        float chance = Random.Range(0f, 1f); // 0-1

        if (chance < 0.3f) // 30% chance positive
        {
            int earned = Random.Range(0, 26); // $0-25
            gameManager.AddMoney(earned); // adds money + increases age
            gameManager.PrintMessage("You put your pride to the side, you received $" + earned + ".");
        }
        else
        {
            // 70% chance nobody felt sorry
            gameManager.IncreaseAge(); // increases age by 0.001
            gameManager.PrintMessage("Nobody felt sorry for you, you made $0.");
        }
    }

    // ------------------- Steal Button -------------------
    public void OnStealClicked()
    {
        float chance = Random.Range(0f, 1f); // 0-1

        if (chance < 0.8f) // 80% chance caught
        {
            int fine = Random.Range(100, 2501); // $100-$2500
            gameManager.money -= fine;          // Money can go negative
            gameManager.IncreaseAge();          // age +0.001
            gameManager.UpdateUI();             // refresh UI after manual money change
            gameManager.PrintMessage("Caught! You got arrested and fined $" + fine + ".");
        }
        else // 20% chance success
        {
            string[] stolenItems = { "computer", "phone", "car", "wallet", "backpack", "tv" };
            string item = stolenItems[Random.Range(0, stolenItems.Length)];
            int earned = Random.Range(0, 501); // $0-$500
            gameManager.AddMoney(earned);      // adds money + age
            gameManager.PrintMessage("You stole a " + item + " and received $" + earned + ".");
        }
    }

    // ------------------- Other buttons -------------------
    // You can implement BottleReturn, Scammer, Messenger in a similar way
}

