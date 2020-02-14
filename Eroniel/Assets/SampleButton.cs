using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleButton : MonoBehaviour
{
    public Button buttonComponent;
    public Text nameLabel;
    public Image iconImage;
    public Text priceText;
    public Text amount;
    public int slotId = 0;
    public ItemOnShop tempShopPrices;

    public Item item;
    public ShopScrollList scrollList;
    public GameObject tempPainelGameObject;
    public bool shopHaveItem = true;

    private int season;
    public int sellValue;
    public int buyValue;

    // Use this for initialization
    void Start()
    {
        buttonComponent.onClick.AddListener(HandleClick);
    }

    //------------------------------------------------------
    public void Setup(Item currentItem, ShopScrollList currentScrollList,GameObject tempPainel,int slotIdTemp,ItemOnShop shopTemp)
    {
        slotId = slotIdTemp;
        item = currentItem;
        nameLabel.text = item.itemName;
        iconImage.sprite = item.artwork;
        tempShopPrices = shopTemp;

        season = currentScrollList.tempData.season;
        switch (season)
        {
            case 0: buyValue = shopTemp.SpringBuy;
                sellValue = shopTemp.SpringSell;
                break;
            case 1:
                buyValue = shopTemp.SummerBuy;
                sellValue = shopTemp.SummerSell;
                break;
            case 2:
                buyValue = shopTemp.AutumBuy;
                sellValue = shopTemp.AutumSell;
                break;
            case 3:
                buyValue = shopTemp.WinterBuy;
                sellValue = shopTemp.WinterSell;
                break;

            default:
                break;
        }
        if (tempPainel.name == "PlayerContentPainel"|| tempPainel.name == "SellContent")
        {
            priceText.text = "<color=green>"+sellValue.ToString()+ "G</color>";
        }
        else {
            priceText.text = "<color=red>-"+buyValue.ToString()+"G</color>";
        }
        if (tempPainel.name == "ShopContent") {
            amount.text = "";
        }
        if (tempPainel.name == "CartContent")
        {
            nameLabel.text = "x1";
        }
        
        scrollList = currentScrollList;
        tempPainelGameObject = tempPainel;
    }


    //------------------------------------------------------
    public void Setup(Item currentItem, ShopScrollList currentScrollList, GameObject tempPainel, int slotIdTemp)
    {
        slotId = slotIdTemp;
        item = currentItem;
        nameLabel.text = item.itemName;
        iconImage.sprite = item.artwork;
        tempShopPrices = null;
        shopHaveItem = false;

        if (tempPainel.name == "PlayerContentPainel")
        {
            priceText.text = "<color=green>" + item.sellValue.ToString() + "G</color>";
        }
        else
        {
            priceText.text = "<color=red>-" + item.buyValue.ToString() + "G</color>";
        }

        scrollList = currentScrollList;
        tempPainelGameObject = tempPainel;
    }


    //------------------------------------------------------

    public void HandleClick()
    {
        if (tempPainelGameObject.name == "ShopContent") {
            scrollList.AddToCart(item,buyValue);
        }

        if (tempPainelGameObject.name == "CartContent") {
            scrollList.RemoveFromCart(slotId);
        }

        if (tempPainelGameObject.name == "PlayerContentPainel") {
            scrollList.PlayerSellCartPlayerToCart(item, sellValue,this.slotId);
        }

        if (tempPainelGameObject.name == "SellContent") {
            scrollList.PlayerSellCartRemoveFromSellCart(slotId);
        }
    }


    //------------------------------------------------------
}