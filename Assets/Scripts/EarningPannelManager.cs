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

    private void Start()
    {
        begButton.onClick.AddListener(OnBegClicked);
        stealButton.onClick.AddListener(OnStealClicked);

        bottleReturnButton.onClick.AddListener(() => gameManager.PrintMessage("Bottle Return clicked!"));
        scammerButton.onClick.AddListener(() => gameManager.PrintMessage("Scammer clicked!"));
        messengerButton.onClick.AddListener(() => gameManager.PrintMessage("Messenger clicked!"));
    }


    // ------------------- BEG FUNCTION -------------------
    public void OnBegClicked()
    {
        float chance = Random.value; // 0 - 1

        if (chance < 0.30f) // 30% positive
        {
            int earned = Random.Range(0, 26); // $0–25
            gameManager.AddMoney(earned);     // also increases age
            gameManager.PrintMessage($"You put your pride aside and received ${earned}.");
        }
        else
        {
            gameManager.IncreaseAge(); // Add life cost (0.001)
            gameManager.PrintMessage("Nobody felt sorry for you, you made $0.");
        }
    }


    // ------------------- STEAL FUNCTION -------------------
    public void OnStealClicked()
    {
        float chance = Random.value;

        if (chance < 0.2f)
        {
            HandleCaught();
        }
        else
        {
            HandleSuccessfulSteal();
        }
    }


    // ----------- SUCCESSFUL STEAL -----------
    private void HandleSuccessfulSteal()
    {
        (string item, int min, int max) stolen = GetRandomStealItem();

        int earned = Random.Range(stolen.min, stolen.max + 1);

        gameManager.AddMoney(earned); // also increases age

        gameManager.PrintMessage($"You stole a {stolen.item} and received ${earned}.");
    }


    // ----------- GETTING CAUGHT -----------
    private void HandleCaught()
    {
        int fine = Random.Range(100, 2501); // $100–2500

        gameManager.IncreaseAge(); // age must increase every click
        gameManager.money -= fine;

        gameManager.UpdateUI();

        gameManager.PrintMessage($"Caught! You got arrested and fined ${fine}.");
    }


    // ----------- ITEM LIST WITH VALUES -----------
    private (string item, int min, int max) GetRandomStealItem()
    {
        // Full item database
        (string item, int min, int max, bool rare)[] items =
        {
            ("Wallet", 5, 100, false),
            ("Computer", 100, 500, true),
            ("Phone", 100, 350, true),
            ("Watch", 25, 125, false),
            ("Bottles", 0, 20, false),
            ("Dust", 0, 0, true),
            ("Toothbrush", 1, 3, false),
            ("Gift Card", 10, 100, false),
            ("Trash", 1, 4, false),
            ("Designer Clothes", 75, 325, true),
            ("Gold Picture Frame", 20, 30, false)
        };

        // Rare 20%, Common 80%
        bool pickRare = (Random.value < 0.2f);

        var pool = System.Array.FindAll(items, i => i.rare == pickRare);

        // Fallback safety
        if (pool.Length == 0) pool = items;

        var chosen = pool[Random.Range(0, pool.Length)];

        return (chosen.item, chosen.min, chosen.max);
    }
}
