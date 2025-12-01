using UnityEngine;
using UnityEngine.UI;

public class EnhancementPanelManager : MonoBehaviour
{
    public GameManager gameManager;

    public Button lockerBtn, maskBtn, cupBtn, flowerBtn, cartBtn;

    void Start()
    {
        lockerBtn.onClick.AddListener(BuyLocker);
        maskBtn.onClick.AddListener(BuyMask);
        cupBtn.onClick.AddListener(BuyCup);
        flowerBtn.onClick.AddListener(BuyFlower);
        cartBtn.onClick.AddListener(BuyCart);
    }

    void BuyLocker()
    {
        if (gameManager.money >= 250 && !gameManager.hasLocker)
        {
            gameManager.money -= 250;
            gameManager.hasLocker = true;
            gameManager.PrintMessage("You purchased a locker — you are protected from robbery.");
            gameManager.UpdateUI();
        }
        else gameManager.PrintMessage("Not enough money or you already own a locker.");
    }

    void BuyMask()
    {
        if (gameManager.money >= 50)
        {
            gameManager.money -= 50;
            gameManager.hasMask = true;
            gameManager.maskUses = Random.Range(15, 26);
            gameManager.PrintMessage($"Mask purchased — identity hidden for {gameManager.maskUses} steals.");
            gameManager.UpdateUI();
        }
    }

    void BuyCup()
    {
        if (gameManager.money >= 50)
        {
            gameManager.money -= 50;
            gameManager.hasCup = true;
            gameManager.cupUses = Random.Range(45, 61);
            gameManager.PrintMessage($"Cup purchased — begging income increased for {gameManager.cupUses} uses.");
            gameManager.UpdateUI();
        }
    }

    void BuyFlower()
    {
        if (gameManager.money >= 50 && !gameManager.hasFlower)
        {
            gameManager.money -= 50;
            gameManager.hasFlower = true;
            gameManager.PrintMessage("You purchased flowers — borrow success increased by +10% permanently.");
            gameManager.UpdateUI();
        }
    }

    void BuyCart()
    {
        if (gameManager.money >= 10)
        {
            gameManager.money -= 10;
            gameManager.hasCart = true;
            gameManager.cartUses = Random.Range(30, 46);
            gameManager.PrintMessage($"Cart purchased — bottle return boosted for {gameManager.cartUses} uses.");
            gameManager.UpdateUI();
        }
    }
}
