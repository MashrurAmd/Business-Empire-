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
            gameManager.IncreaseAge(); // increase age by 0.001
            gameManager.PrintMessage("Nobody felt sorry for you, you made $0.");
        }
        else
        {
            // Earn random money 0-10
            int earned = Random.Range(0, 11);
            gameManager.AddMoney(earned); // adds money AND increases age by 0.001
            gameManager.PrintMessage("You put your pride to the side, you received $" + earned + ".");
        }
    }

}
