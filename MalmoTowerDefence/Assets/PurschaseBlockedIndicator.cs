using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PurschaseBlockedIndicator : MonoBehaviour
{
    [SerializeField] private EnumValueSO unit;
    [SerializeField] private float purschaseWaitingTime;
    [SerializeField] private Color openColor, closedColor;
    private Image image;

    public string UnitName => unit.name;

    public bool openForBusiness = true;
    void Start()
    {
        SelectionManager.OnUnitBought += CheckBlock;
        image = GetComponent<Image>();
    }

    private void CheckBlock(string NAME)
    {
        if(NAME.Equals(unit.name))
            StartCoroutine(RecentPurchase());
    }

    private IEnumerator RecentPurchase()
    {
        openForBusiness = false;
        CloseSloth();
        yield return new WaitForSeconds(purschaseWaitingTime);
        OpenSloth();
        openForBusiness = true;
    }

    //göra om till en om jag inte ska lägga till fler saker kanske?
    private void OpenSloth()
    {
        ChangeColor(openColor);
    }

    private void CloseSloth()
    {
        ChangeColor(closedColor);
    }
    
    private void ChangeColor(Color COLOR)
    {
        image.color = COLOR;
    }

    private bool OpenForBusiness(string NAME) => openForBusiness;

    // Update is called once per frame
    void Update()
    {
        
    }
}
