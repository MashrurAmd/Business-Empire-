using UnityEngine;
using UnityEngine.UI;

public class EarningPanelManager : MonoBehaviour
{
    [Header("Game Manager Reference")]
    public GameManager gameManager;

    [Header("Buttons")]
    public Button begButton, stealButton, bottleReturnButton, scammerButton, messengerButton, borrowButton;

    int stealClicks = 0;
    int begClicks = 0;
    int bottleClicks = 0;

    bool CanAct() => gameManager != null && !gameManager.gameOver;

    void Start()
    {
        begButton.onClick.AddListener(OnBeg);
        stealButton.onClick.AddListener(OnSteal);
        bottleReturnButton.onClick.AddListener(OnBottle);
        scammerButton.onClick.AddListener(OnScam);
        messengerButton.onClick.AddListener(OnMessage);
        borrowButton.onClick.AddListener(OnBorrow);
    }

    //------------------ BEG ------------------//
    void OnBeg()
    {
        if (!CanAct()) return;

        begClicks++;

        float chance = 0.30f;
        int earn = Random.Range(0, 16);

        if (gameManager.hasCup) { earn += 2; }

        if (Random.value < chance)
        {
            gameManager.AddMoney(earn);
            gameManager.PrintMessage($"You earned ${earn} from begging.");
        }
        else
        {
            gameManager.IncreaseAge();
            gameManager.PrintMessage("Nobody gave you money.");
        }

        if (gameManager.hasCup && begClicks >= gameManager.cupUses)
        { gameManager.hasCup = false; gameManager.PrintMessage("Your cup broke."); }
    }


    //------------------ STEAL ------------------//
    void OnSteal()
    {
        if (!CanAct()) return;

        stealClicks++;

        float caughtChance = gameManager.hasMask ? 0.40f : 0.50f;

        if (Random.value < caughtChance) { HandleCaught(); }
        else { HandleSuccessSteal(); }

        if (gameManager.hasMask && stealClicks >= gameManager.maskUses)
        { gameManager.hasMask = false; gameManager.PrintMessage("Your mask wore out."); }
    }

    void HandleSuccessSteal()
    {
        var data = GetItem();
        int money = Random.Range(data.min, data.max + 1);
        gameManager.AddMoney(money);
        gameManager.PrintMessage($"You stole **{data.name}** & earned **${money}**.");
    }

    void HandleCaught()
    {
        int fine = Random.Range(100, 2501);
        gameManager.AddMoney(-fine);
        gameManager.PrintMessage($"Caught! Fined **${fine}**.");
    }

    (string name, int min, int max) GetItem()
    {
        (string item, int min, int max, bool rare)[] list =
        {
            ("Wallet",5,100,false), ("Computer",100,500,true), ("Phone",100,350,true),
            ("Watch",25,125,false), ("Bottles",1,20,false), ("Dust",0,0,true),
            ("Toothbrush",1,3,false), ("Gift Card",10,100,false), ("Trash",1,4,false),
            ("Designer Clothes",75,325,true), ("Gold Picture Frame",20,30,false)
        };

        bool rarePick = Random.value < 0.2f;
        var pool = System.Array.FindAll(list, x => x.rare == rarePick);
        var pick = pool[Random.Range(0, pool.Length)];
        return (pick.item, pick.min, pick.max);
    }


    //------------------- BOTTLE RETURN -------------------//
    void OnBottle()
    {
        if (!CanAct()) return;

        bottleClicks++;

        float value = gameManager.hasCart ? 0.50f : 0.25f;
        int lifeLoss = Random.Range(1, 6);

        gameManager.AddMoney(value);
        gameManager.AddLifeLoss(lifeLoss);
        gameManager.PrintMessage($"Bottle returned +${value} (-{lifeLoss} days).");

        if (gameManager.hasCart && bottleClicks >= gameManager.cartUses)
        { gameManager.hasCart = false; gameManager.PrintMessage("Your cart broke."); }
    }


    //------------------- SCAM -------------------//
    void OnScam()
    {
        if (!CanAct()) return;

        if (Random.value < 0.5f)
        {
            int fine = Random.Range(0, 2001);
            gameManager.AddMoney(-fine);
            gameManager.PrintMessage($"Scam failed! Fined ${fine}.");
        }
        else
        {
            int earn = Random.Range(0, 1001);
            gameManager.AddMoney(earn);
            gameManager.PrintMessage($"You scammed someone & earned ${earn}.");
        }
    }

    //------------------- MESSENGER -------------------//
    void OnMessage()
    {
        if (!CanAct()) return;

        if (Random.value < 0.40f)
        {
            gameManager.IncreaseAge();
            gameManager.PrintMessage("Delivery failed — earned nothing.");
        }
        else
        {
            int earn = Random.Range(0, 201);
            gameManager.AddMoney(earn);
            gameManager.PrintMessage($"Delivery completed — earned ${earn}.");
        }
    }


    //------------------- BORROW -------------------//
    void OnBorrow()
    {
        if (!CanAct()) return;

        float success = gameManager.hasFlower ? 0.30f : 0.20f;

        if (Random.value < success)
        {
            int earn = Random.Range(0, 201);
            gameManager.AddMoney(earn);
            gameManager.PrintMessage($"Family helped — gained ${earn}.");
        }
        else
        {
            gameManager.IncreaseAge();
            gameManager.PrintMessage("No one helped you.");
        }
    }
}
