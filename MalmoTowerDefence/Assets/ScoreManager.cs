using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public delegate void UpdateMoney(int MONEY);

    public static event UpdateMoney OnUpdateMoney;
    private static int money = 0;

    public static int Money => money;
    [SerializeField] private Transform textMeshTransform;
    [SerializeField] private TextMeshProUGUI textMesh;
    
    void Start()
    {
        textMesh = textMeshTransform.transform.GetComponent<TextMeshProUGUI>();
        UpdateCashText();
    }

    public static void MoneyDeposit(int ADD)
    {
        money += ADD;
        OnUpdateMoney?.Invoke(money);
    }

    public static void MoneyWithdrawal(int SUBTRACT)
    {
        if (SUBTRACT <= money)
        {
            money -= SUBTRACT;
            OnUpdateMoney?.Invoke(money);
        }
    }

    private void UpdateCashText()
    {
        textMesh.text = money.ToString();
    }


}

