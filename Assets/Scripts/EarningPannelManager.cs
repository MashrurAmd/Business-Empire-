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
    public Button borrowButton;

    private void Start()
    {
        if (begButton != null) begButton.onClick.AddListener(OnBegClicked);
        if (stealButton != null) stealButton.onClick.AddListener(OnStealClicked);
        if (bottleReturnButton != null) bottleReturnButton.onClick.AddListener(OnBottleReturnClicked);
        if (scammerButton != null) scammerButton.onClick.AddListener(OnScammerClicked);
        if (messengerButton != null) messengerButton.onClick.AddListener(OnMessengerClicked);
        if (borrowButton != null) borrowButton.onClick.AddListener(OnBorrowClicked);
    }

    bool CanAct()
    {
        if (gameManager == null) return false;
        return !gameManager.gameOver;
    }

    // ------------------- BEG -------------------
    public void OnBegClicked()
    {
        if (!CanAct()) return;

        float chance = Random.value; // 0 - 1

        if (chance < 0.30f) // 30% positive
        {
            int earned = Random.Range(0, 26); // $0–25
            gameManager.AddMoney((float)earned);     // also increases age
            gameManager.PrintMessage($"You put your pride aside and received ${earned}.");
        }
        else
        {
            gameManager.IncreaseAge(); // Add life cost (0.001)
            gameManager.PrintMessage("Nobody felt sorry for you, you made $0.");
        }
    }


    // ------------------- STEAL -------------------
    public void OnStealClicked()
    {
        if (!CanAct()) return;

        float chance = Random.value;

        if (chance < 0.5f)
        {
            HandleCaught();
        }
        else
        {
            HandleSuccessfulSteal();
        }
    }

    private void HandleSuccessfulSteal()
    {
        (string item, int min, int max) stolen = GetRandomStealItem();

        int earned = Random.Range(stolen.min, stolen.max + 1);

        gameManager.AddMoney((float)earned); // also increases age
        gameManager.PrintMessage($"You stole a {stolen.item} and received ${earned}.");
    }

    private void HandleCaught()
    {
        int fine = Random.Range(100, 2501); // $100–2500 fine

        // apply fine and age correctly through AddMoney and IncreaseAge to keep logic consistent
        gameManager.AddMoney(-(float)fine); // this will also increase age by ageIncrement
        gameManager.PrintMessage($"Caught! You got arrested and fined ${fine}.");
    }

    // item list with values and rarity
    private (string item, int min, int max) GetRandomStealItem()
    {
        (string item, int min, int max, bool rare)[] items =
        {
            ("Wallet", 5, 100, false),
            ("Computer", 100, 500, true),
            ("Phone", 100, 350, true),
            ("Watch", 25, 125, false),
            ("Bottles", 1, 20, false), // using integer bottle counts here
            ("Dust", 0, 0, true),
            ("Toothbrush", 1, 3, false),
            ("Gift Card", 10, 100, false),
            ("Trash", 1, 4, false),
            ("Designer Clothes", 75, 325, true),
            ("Gold Picture Frame", 20, 30, false)
        };

        bool pickRare = (Random.value < 0.2f); // 20% rare
        var pool = System.Array.FindAll(items, i => i.rare == pickRare);
        if (pool.Length == 0) pool = items;

        var chosen = pool[Random.Range(0, pool.Length)];
        return (chosen.item, chosen.min, chosen.max);
    }


    // ------------------- BOTTLE RETURN -------------------
    // Each bottle returned gives $0.25 and costs 1-5 "days" in your decimal age system.
    public void OnBottleReturnClicked()
    {
        if (!CanAct()) return;

        int lostLifeDays = Random.Range(1, 6); // 1–5 days
        float earned = 0.25f;

        // apply money (AddMoney handles age increment by one action)
        // We want lifeLoss to be applied separately: AddLifeLoss applies multiple day increments
        gameManager.AddMoney(earned);           // adds $0.25 and also increases age by 0.001
        gameManager.AddLifeLoss(lostLifeDays); // then apply additional life loss (n * 0.001)

        gameManager.PrintMessage($"You returned a bottle and earned $0.25 but lost {lostLifeDays} days of life.");
    }


    // ------------------- SCAMMER -------------------
    public void OnScammerClicked()
    {
        if (!CanAct()) return;

        float chance = Random.value;

        if (chance < 0.5f) // 50% caught
        {
            int fine = Random.Range(0, 2001); // $0 - $2000
            gameManager.AddMoney(-(float)fine); // negative money and increases age
            gameManager.PrintMessage($"Law enforcement found your location, you were fined ${fine}.");
        }
        else
        {
            int earned = Random.Range(0, 1001); // $0 - $1000
            gameManager.AddMoney((float)earned);
            gameManager.PrintMessage($"You scammed someone out of ${earned}.");
        }
    }


    // ------------------- MESSENGER -------------------
    public void OnMessengerClicked()
    {
        if (!CanAct()) return;

        float chance = Random.value;

        if (chance < 0.4f) // 40% failure
        {
            gameManager.IncreaseAge();
            gameManager.PrintMessage("You got lost, you made $0.");
        }
        else
        {
            int earned = Random.Range(0, 201); // $0 - $200
            gameManager.AddMoney((float)earned);
            gameManager.PrintMessage($"Successful delivery! You made ${earned}.");
        }
    }

    // ------------------- BORROW (NEW) -------------------
    public void OnBorrowClicked()
    {
        if (!CanAct()) return;

        float chance = Random.value;

        if (chance < 0.80f) // NEGATIVE — 80% chance
        {
            string[] responses = {
                "You didn't receive anything.",
                "Your family has shunned you."
            };

            string response = responses[Random.Range(0, responses.Length)];
            gameManager.IncreaseAge(); // life cost
            gameManager.PrintMessage(response);
        }
        else // POSITIVE — 20% chance
        {
            int earned = Random.Range(0, 201);
            gameManager.AddMoney((float)earned);
            gameManager.PrintMessage($"Enabling is what family is for, you received ${earned}.");
        }
    }




}
