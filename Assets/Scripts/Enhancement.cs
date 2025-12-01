using UnityEngine;
using UnityEngine.UI;

public class EnhancementPanelManager : MonoBehaviour
{
    public GameManager gameManager;

    [Header("Enhancement Buttons")]
    public Button lockerButton;
    public Button maskButton;
    public Button cupButton;
    public Button flowerButton;
    public Button cartButton;

    void Start()
    {
        if (lockerButton != null) lockerButton.onClick.AddListener(OnLockerPurchase);
        if (maskButton != null) maskButton.onClick.AddListener(OnMaskPurchase);
        if (cupButton != null) cupButton.onClick.AddListener(OnCupPurchase);
        if (flowerButton != null) flowerButton.onClick.AddListener(OnFlowerPurchase);
        if (cartButton != null) cartButton.onClick.AddListener(OnCartPurchase);
    }

    // -------------------- LOCKER --------------------
    public void OnLockerPurchase()
    {
        if (gameManager.hasLocker)
        {
            gameManager.PrintMessage("You already own a locker.");
            return;
        }

        if (gameManager.money < 250)
        {
            gameManager.PrintMessage("You do not have enough money to buy a locker ($250 required).");
            return;
        }

        gameManager.AddMoney(-250);
        gameManager.hasLocker = true;
        gameManager.PrintMessage("You purchased a locker, your items will be secured.");
    }

    // -------------------- MASK --------------------
    public void OnMaskPurchase()
    {
        if (gameManager.hasMask)
        {
            gameManager.PrintMessage("You already have a mask.");
            return;
        }

        if (gameManager.money < 50)
        {
            gameManager.PrintMessage("You do not have enough money to buy a mask ($50).");
            return;
        }

        gameManager.AddMoney(-50);
        gameManager.hasMask = true;
        gameManager.maskClicksRemaining = Random.Range(15, 26);
        gameManager.PrintMessage("You purchased a mask, your identity will be protected.");
    }

    // -------------------- CUP --------------------
    public void OnCupPurchase()
    {
        if (gameManager.hasCup)
        {
            gameManager.PrintMessage("You already have a cup.");
            return;
        }

        if (gameManager.money < 50)
        {
            gameManager.PrintMessage("You do not have enough money to buy a cup ($50).");
            return;
        }

        gameManager.AddMoney(-50);
        gameManager.hasCup = true;
        gameManager.cupClicksRemaining = Random.Range(45, 61);
        gameManager.PrintMessage("You purchased a cup, your chances of begging have increased.");
    }

    // -------------------- FLOWER --------------------
    public void OnFlowerPurchase()
    {
        if (gameManager.hasFlower)
        {
            gameManager.PrintMessage("You already purchased flowers.");
            return;
        }

        if (gameManager.money < 50)
        {
            gameManager.PrintMessage("You do not have enough money to buy flowers ($50).");
            return;
        }

        gameManager.AddMoney(-50);
        gameManager.hasFlower = true;
        gameManager.PrintMessage("You purchased flowers, your chances of borrowing have increased.");
    }

    // -------------------- CART --------------------
    public void OnCartPurchase()
    {
        if (gameManager.hasCart)
        {
            gameManager.PrintMessage("You already have a cart.");
            return;
        }

        if (gameManager.money < 10)
        {
            gameManager.PrintMessage("You do not have enough money to buy a cart ($10).");
            return;
        }

        gameManager.AddMoney(-10);
        gameManager.hasCart = true;
        gameManager.cartClicksRemaining = 30;
        gameManager.PrintMessage("You purchased a cart, your chances of bottle return income have increased.");
    }
}
