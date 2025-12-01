using UnityEngine;
using UnityEngine.UI;

public class EnhancementPanelManager : MonoBehaviour
{
    public GameManager gameManager;

    [Header("Enhancement Buttons")]
    public Button lockerButton;
    // add more enhancements later...

    void Start()
    {
        lockerButton.onClick.AddListener(OnLockerPurchase);
    }

    // ===================== LOCKER PURCHASE FUNCTION =====================
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

        gameManager.AddMoney(-250); // deduct price
        gameManager.hasLocker = true;

        gameManager.PrintMessage("You purchased a locker, your items will be secured.");
    }
}
