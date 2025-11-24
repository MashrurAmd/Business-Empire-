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
        float chance = Random.Range(0f, 1f); // 0.0 to 1.0

        if (chance < 0.3f)
        {
            // 30% chance - positive outcome
            int earned = Random.Range(0, 26); // 0-25$
            gameManager.AddMoney(earned); // updates money and age
            gameManager.PrintMessage("You put your pride to the side, you received $" + earned + ".");
        }
        else
        {
            // 70% chance - nobody felt sorry
            gameManager.IncreaseAge(); // increase age by 0.001, updates UI
            gameManager.PrintMessage("Nobody felt sorry for you, you made $0.");
        }
    }


}
