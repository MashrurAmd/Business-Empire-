using UnityEngine;
using UnityEngine.UI;

public class PanelSwitcher : MonoBehaviour
{
    [Header("Assign your panels here")]
    public GameObject[] panels; // All panels you want to control
    public int startPanelIndex = 0; // Panel to show at game start

    [Header("Assign buttons here (each button matches a panel by index)")]
    public Button[] buttons; // Buttons that will switch to panels

    void Start()
    {
        // Deactivate all panels
        foreach (GameObject panel in panels)
            panel.SetActive(false);

        // Activate the starting panel
        if (panels.Length > 0 && startPanelIndex < panels.Length)
            panels[startPanelIndex].SetActive(true);

        // Set up button listeners
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Capture index for the listener
            buttons[i].onClick.AddListener(() => ShowPanel(index));
        }
    }

    public void ShowPanel(int index)
    {
        // Deactivate all panels
        foreach (GameObject panel in panels)
            panel.SetActive(false);

        // Activate the selected panel
        if (index >= 0 && index < panels.Length)
            panels[index].SetActive(true);
    }
}
