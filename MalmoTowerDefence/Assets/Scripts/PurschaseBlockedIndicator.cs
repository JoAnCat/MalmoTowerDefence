using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PurschaseBlockedIndicator : MonoBehaviour
{
    public delegate void Cost2Pay(int PRICE);

    public static event Cost2Pay OnCost2Pay;
    [SerializeField] private int unitCostNormal;
    [SerializeField] private EnumValueSO unit;
    [SerializeField] private float purschaseWaitingTime;
    [SerializeField] private Color openColor, closedColor;
    [SerializeField] private Sprite openSpriteButton, closedSpriteButton;
    [SerializeField] private Color openTextColor, closedTextColor;
    private Image image;
    [SerializeField] private Image childButtonImage;
    [SerializeField] private TextMeshProUGUI priceText;
    public string UnitName => unit.name;
    private int unitCounter, unitCostCurrent;
    [SerializeField] private int freeUnits;
    
    private bool openForBusiness, recentPurchase, haveEnoughMoney;

    public bool OpenForBusiness => openForBusiness;

    void Start()
    {
        ScoreManager.OnUpdateMoney += UpdateAndCheckPrice;
        SelectionManager.OnUnitBought += CheckBlock;
        image = GetComponent<Image>();
        //childButtonImage = GetComponentInChildren<Image>();
        //priceText = GetComponentInChildren<TextMeshProUGUI>();
        if (0 < freeUnits)
        {
            unitCostCurrent = 0;
            haveEnoughMoney = true;
            OpenSloth();
        }

        else
        {
            unitCostCurrent = unitCostNormal;
            CloseSloth();
        }
        UpdateCostText();
    }

    private void CheckBlock(string NAME)
    {
        if(NAME.Equals(unit.name))
            StartCoroutine(Purchase());
    }

    private IEnumerator Purchase()
    {
        unitCounter++;
        if (freeUnits <= unitCounter)
        {
            unitCostCurrent = unitCostNormal;
            UpdateCostText();
            if (ScoreManager.Money < unitCostCurrent)
                haveEnoughMoney = false;
        }
            
        recentPurchase = true;
        OnCost2Pay?.Invoke(unitCostCurrent);
        CloseSloth();
        yield return new WaitForSeconds(purschaseWaitingTime);
        
        recentPurchase = false;
        OpenSloth();

    }

    private void UpdateCostText() => priceText.text = unitCostCurrent.ToString();
    

    //göra om till en om jag inte ska lägga till fler saker kanske?
    private void OpenSloth()
    {
        if (recentPurchase && EnoughMoney())
        {
            PriceTextUpdate(openTextColor, openSpriteButton);
        }
        else if (recentPurchase == false && EnoughMoney())
        {
            PriceTextUpdate(openTextColor, openSpriteButton);
            ChangeColor(openColor);
            openForBusiness = true;
        }
        else
        {
            Debug.Log("PBI: Not enough money and recent prurschaed unit so I can't ope sloth");
        }
        
    }

    private void PriceTextUpdate(Color TEXT_COLOR, Sprite BUTTON)
    {
        childButtonImage.sprite = BUTTON;
        priceText.color = TEXT_COLOR;
    }

    private bool EnoughMoney() => haveEnoughMoney;

    private void CloseSloth()
    {
        openForBusiness = false;
        if (EnoughMoney() == false)
            PriceTextUpdate(closedTextColor, closedSpriteButton);
        ChangeColor(closedColor);
    }
    
    private void ChangeColor(Color COLOR)
    {
        image.color = COLOR;
    }

    private void UpdateAndCheckPrice(int TOTAL_MONEY)
    {
        if (TOTAL_MONEY < unitCostCurrent){
            haveEnoughMoney = false;
            if (openForBusiness)
                CloseSloth();
        }
           
        else{
            haveEnoughMoney = true;
            if(openForBusiness == false)
                OpenSloth();
        }
            
    }
}
