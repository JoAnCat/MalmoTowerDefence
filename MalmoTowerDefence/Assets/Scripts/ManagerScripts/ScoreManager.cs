using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.WebCam;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameObject endOfSceneUI;
    [SerializeField] private TextMeshProUGUI winLoseText;
    public delegate void UpdateMoney(int MONEY);

    public static event UpdateMoney OnUpdateMoney;
    private static int money = 0;
    private static int standardCoinValue = 25;
    private static int standardCrystalValue = 100;

    public static int StandardCoinValue => standardCoinValue;
    public static int StandardCrystalValue => standardCrystalValue;

    public static int Money => money;
    [SerializeField] private Transform textMeshTransform;
    [SerializeField] private TextMeshProUGUI textMesh;
    
    void Start()
    {
        textMesh = textMeshTransform.transform.GetComponent<TextMeshProUGUI>();
        UpdateCashText();
        CoinMovement.OnCoin2Bank += StandardCoinCollect;
        PurschaseBlockedIndicator.OnCost2Pay += DwarfBought;
        OrcFighters.OnLooseGame += GameOver;
        OrcPool.OnVictory += Victory;
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        endOfSceneUI.SetActive(true);
        GameObject.FindWithTag("ContinueButton").GetComponent<Button>().interactable = false;
        winLoseText.text = "Defeat!";
    }

    private void Victory()
    {
        Time.timeScale = 0;
        endOfSceneUI.SetActive(true);
        winLoseText.text = "Victory!";
    }

    private void DwarfBought(int AMOUNT) => MoneyWithdrawal(AMOUNT);

    private void StandardCoinCollect() => MoneyDeposit(standardCoinValue);


    private void MoneyDeposit(int ADD)
    {
        money += ADD;
        UpdateCashText();
    }

    private void MoneyWithdrawal(int SUBTRACT)
    {
        if (SUBTRACT <= money)
        {
            money -= SUBTRACT;
            UpdateCashText();
        }
    }

    private void UpdateCashText()
    {
        textMesh.text = money.ToString();
        OnUpdateMoney?.Invoke(money);
    }


}

