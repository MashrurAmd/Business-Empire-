using UnityEngine;
using UnityEngine.UI;

public class BusinessButton : MonoBehaviour
{
    public string businessId;
    public BusinessManager businessManager;
    public Button button;

    void Start()
    {
        if (button == null)
            button = GetComponent<Button>();

        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        businessManager.BuyBusiness(businessId);
    }
}
