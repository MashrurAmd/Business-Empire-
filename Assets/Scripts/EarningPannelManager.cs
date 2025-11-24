using UnityEngine;
using UnityEngine.UI;

public class EarningPanelManager : MonoBehaviour
{
    public GameManager gameManager; // Reference to the GameManager

    public Button begButton;

    void Start()
    {
        begButton.onClick.AddListener(OnBegClicked);
    }

    void OnBegClicked()
    {
        int randomOutcome = Random.Range(0, 2); // 0 or 1

        if (randomOutcome == 0)
        {
            // Nobody felt sorry
            gameManager.SubtractLife(1);
            gameManager.PrintMessage("Nobody felt sorry for you, you made $0.");
        }
        else
        {
            // Earn random money 0-25
            int earned = Random.Range(0, 26);
            gameManager.AddMoney(earned);
            gameManager.PrintMessage("You put your pride to the side, you received $" + earned + ".");
        }
    }
}
