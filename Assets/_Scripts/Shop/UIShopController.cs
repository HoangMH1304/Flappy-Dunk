using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIShopController : MonoBehaviour
{
    public static UIShopController Instance;
    [SerializeField] private Image fillBarInShop, fillBarInLobby;
    [SerializeField] private TextMeshProUGUI processText;

    private void Awake() 
    {
        Instance = this;
    }

    private void Start() {
        UpdateProcess();    
    }

    public void UpdateProcess()
    {
        int totalUnlockItems = ShopController.Instance.TotalUnlockItems;
        int totalItems = ShopController.Instance.TotalItems;
        processText.text = totalUnlockItems + "/" + totalItems;

        float percentOfProcess = 1.0f * totalUnlockItems / totalItems;
        fillBarInLobby.fillAmount = percentOfProcess;
        fillBarInLobby.color = ColorContainer.Instance.GetColorByPercent(percentOfProcess);
        fillBarInShop.fillAmount = percentOfProcess;
        fillBarInShop.color = ColorContainer.Instance.GetColorByPercent(percentOfProcess);
    }
}
