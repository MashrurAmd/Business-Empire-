using UnityEngine;
using UnityEngine.UI;

public class EarningPanelManager : MonoBehaviour
{
    [Header("Game Manager Reference")]
    public GameManager gameManager;

    [Header("Hustle Buttons")]
    public Button begButton;
    public Button stealButton;
    public Button bottleReturnButton;
    public Button scammerButton;
    public Button messengerButton;
    public Button borrowButton;

    [Header("Resource Buttons")]
    public Button cartboardButton;
    public Button knifeButton;
    public Button phoneButton;

    private void Start()
    {
        if (begButton != null) begButton.onClick.AddListener(OnBegClicked);
        if (stealButton != null) stealButton.onClick.AddListener(OnStealClicked);
        if (bottleReturnButton != null) bottleReturnButton.onClick.AddListener(OnBottleReturnClicked);
        if (scammerButton != null) scammerButton.onClick.AddListener(OnScammerClicked);
        if (messengerButton != null) messengerButton.onClick.AddListener(OnMessengerClicked);
        if (borrowButton != null) borrowButton.onClick.AddListener(OnBorrowClicked);

        if (cartboardButton != null) cartboardButton.onClick.AddListener(OnCartboardClicked);
        if (knifeButton != null) knifeButton.onClick.AddListener(OnKnifeClicked);
        if (phoneButton != null) phoneButton.onClick.AddListener(OnPhoneClicked);
    }

    bool CanAct() => gameManager != null && !gameManager.gameOver;

    // ------------------- BEG -------------------
    public void OnBegClicked()
    {
        if (!CanAct()) return;

        if (!gameManager.hasCartboard)
        {
            gameManager.PrintMessage("Please buy cartboard to unlock beg");
            return;
        }

        float chance = Random.value;
        if (chance < 0.30f)
        {
            int earned = Random.Range(0, 26);
            if (gameManager.hasCup && gameManager.cupClicksRemaining > 0) { earned += 2; gameManager.cupClicksRemaining--; }
            gameManager.AddMoney((float)earned);
            gameManager.PrintMessage($"You put your pride aside and received ${earned}.");
        }
        else
        {
            gameManager.IncreaseAge();
            gameManager.PrintMessage("Nobody felt sorry for you, you made $0.");
        }
    }

    // ------------------- STEAL -------------------
    public void OnStealClicked()
    {
        if (!CanAct()) return;

        if (!gameManager.hasKnife)
        {
            gameManager.PrintMessage("Please buy knife to unlock steal");
            return;
        }

        float caughtRate = (gameManager.hasMask && gameManager.maskClicksRemaining > 0) ? 0.4f : 0.5f;
        if (gameManager.hasMask && gameManager.maskClicksRemaining > 0) gameManager.maskClicksRemaining--;

        float chance = Random.value;
        if (chance < caughtRate)
            HandleCaught();
        else
            HandleSuccessfulSteal();
    }

    private void HandleSuccessfulSteal()
    {
        (string item, int min, int max) stolen = GetRandomStealItem();
        int earned = Random.Range(stolen.min, stolen.max + 1);
        gameManager.AddMoney((float)earned);
        gameManager.PrintMessage($"You stole a {stolen.item} and received ${earned}.");
    }

    private void HandleCaught()
    {
        int fine = Random.Range(100, 2501);
        gameManager.AddMoney(-(float)fine);
        gameManager.PrintMessage($"Caught! You got arrested and fined ${fine}.");
    }

    private (string item, int min, int max) GetRandomStealItem()
    {
        (string item, int min, int max, bool rare)[] items =
        {
            ("Wallet",5,100,false),
            ("Computer",100,500,true),
            ("Phone",100,350,true),
            ("Watch",25,125,false),
            ("Bottles",1,20,false),
            ("Dust",0,0,true),
            ("Toothbrush",1,3,false),
            ("Gift Card",10,100,false),
            ("Trash",1,4,false),
            ("Designer Clothes",75,325,true),
            ("Gold Picture Frame",20,30,false)
        };

        bool pickRare = (Random.value < 0.2f);
        var pool = System.Array.FindAll(items, i => i.rare == pickRare);
        if (pool.Length == 0) pool = items;
        var chosen = pool[Random.Range(0, pool.Length)];
        return (chosen.item, chosen.min, chosen.max);
    }

    // ------------------- BOTTLE RETURN -------------------
    public void OnBottleReturnClicked()
    {
        if (!CanAct()) return;

        int lostLifeDays = Random.Range(1, 6);
        float earned = gameManager.hasCart && gameManager.cartClicksRemaining > 0 ? 0.5f : 0.25f;
        if (gameManager.hasCart && gameManager.cartClicksRemaining > 0) gameManager.cartClicksRemaining--;

        gameManager.AddMoney(earned);
        gameManager.AddLifeLoss(lostLifeDays);
        gameManager.PrintMessage($"You returned a bottle and earned ${earned:F2} but lost {lostLifeDays} days of life.");
    }

    // ------------------- SCAMMER -------------------
    public void OnScammerClicked()
    {
        if (!CanAct()) return;

        float chance = Random.value;

        if (chance < 0.5f)
        {
            int fine = Random.Range(0, 2001);
            gameManager.AddMoney(-(float)fine);
            gameManager.PrintMessage($"Law enforcement found your location, you were fined ${fine}.");
        }
        else
        {
            int earned = Random.Range(0, 1001);
            gameManager.AddMoney((float)earned);
            gameManager.PrintMessage($"You scammed someone out of ${earned}.");
        }
    }

    // ------------------- MESSENGER -------------------
    public void OnMessengerClicked()
    {
        if (!CanAct()) return;

        if (!gameManager.hasPhone)
        {
            gameManager.PrintMessage("Please buy phone to unlock messenger");
            return;
        }

        float chance = Random.value;
        if (chance < 0.4f)
        {
            gameManager.IncreaseAge();
            gameManager.PrintMessage("You got lost, you made $0.");
        }
        else
        {
            int earned = Random.Range(0, 201);
            gameManager.AddMoney((float)earned);
            gameManager.PrintMessage($"Successful delivery, you made ${earned}.");
        }
    }

    // ------------------- BORROW -------------------
    public void OnBorrowClicked()
    {
        if (!CanAct()) return;

        float positiveChance = gameManager.hasFlower ? 0.30f : 0.20f;

        if (Random.value > positiveChance)
        {
            gameManager.IncreaseAge();
            string[] responses = { "You didn't receive anything.", "Your family has shunned you." };
            gameManager.PrintMessage(responses[Random.Range(0, responses.Length)]);
        }
        else
        {
            int earned = Random.Range(0, 201);
            gameManager.AddMoney((float)earned);
            gameManager.PrintMessage($"Enabling is what family is for, you received ${earned}.");
        }
    }

    // ------------------- RESOURCE BUTTONS -------------------
    public void OnCartboardClicked()
    {
        if (gameManager.hasCartboard) { gameManager.PrintMessage("You already have a cartboard."); return; }
        gameManager.hasCartboard = true;
        gameManager.PrintMessage("You acquired a cartboard. Beg and Messenger unlocked!");
    }

    public void OnKnifeClicked()
    {
        if (gameManager.hasKnife) { gameManager.PrintMessage("You already have a knife."); return; }
        if (gameManager.money < 10) { gameManager.PrintMessage("You do not have enough money to buy a knife ($10 required)."); return; }

        gameManager.AddMoney(-10);
        gameManager.hasKnife = true;
        gameManager.PrintMessage("You bought a knife. Steal button unlocked!");

        //gameManager.AddReputation(5f);
    }

    public void OnPhoneClicked()
    {
        if (gameManager.hasPhone) { gameManager.PrintMessage("You already have a phone."); return; }
        if (gameManager.money < 1500) { gameManager.PrintMessage("You do not have enough money to buy a phone ($1500 required)."); return; }

        gameManager.AddMoney(-1500);
        gameManager.hasPhone = true;
        gameManager.PrintMessage("You bought a phone. Messenger button unlocked!");
    }
}
