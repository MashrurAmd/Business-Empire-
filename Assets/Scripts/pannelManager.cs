using UnityEngine;
using UnityEngine.UI;

public class PanelSwitcher : MonoBehaviour
{
    [Header("Assign your panels here")]
    public GameObject[] panels; // All panels to control
    public int startPanelIndex = 0; // Which panel to show first

    [Header("Assign your buttons here")]
    public Button button1;
    public Button button2;

    void Start()
    {
        // Deactivate all panels first
        foreach (GameObject panel in panels)
            panel.SetActive(false);

        // Activate the starting panel
        if (panels.Length > 0 && startPanelIndex < panels.Length)
            panels[startPanelIndex].SetActive(true);

        // Add button listeners
        if (button1 != null)
            button1.onClick.AddListener(() => ShowPanel(0)); // show panel 0
        if (button2 != null)
            button2.onClick.AddListener(() => ShowPanel(1)); // show panel 1
    }

    public void ShowPanel(int index)
    {
        
        foreach (GameObject panel in panels)
            panel.SetActive(false);

        
        if (index >= 0 && index < panels.Length)
            panels[index].SetActive(true);
    }
}
