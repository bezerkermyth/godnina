using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class ShopScrollList : MonoBehaviour
{

    //public List<Item> itemList;
    public List<ItemOnShop> shopItemList;
    [Header("Content Painel Holder")]
    public GameObject contentPanel;
    public GameObject cartPanel;
    public GameObject playerContentPainel;
    public GameObject playerSellContentPainel;
    public GameObject gameManager;
    [Header("Text Display Holder")]
    public Text myGoldDisplay;
    public Text tradeBalanceTxt;
    [Header("Object Pool")]
    public SimpleObjectPool buttonObjectPool;
    public SimpleObjectPool cartObjectPool;
    public SimpleObjectPool playerObjectPool;
    public SimpleObjectPool playerSellObjectPool;


    public List<Item> cartList;
    public List<Item> playerList;
    public List<Item> playerSellList;
    public List<SampleButton> cartButtonList;
    public List<SampleButton> playerButtonList;
    public List<SampleButton> playerSellButtonList;
    public int balance = 0;
    public bool findPlayerStat = false;

    private bool findItemOnShopToCopyValue = false;

    public bool findPlayerData = false;
    public float gold = 20f;
    //public ItemOnShop tempShopHolder;
    public bool haveItemOnShop = false;
    public int shopPositonItemPriceTemp;

    public SaveData tempData;
    //--------------------------------------------------------------------
    private void Update()
    {
        if (!findPlayerData)
        {
            LoadPlayerData();
            RefreshDisplay();
            myGoldDisplay.text = "Gold: " + gold.ToString();
        }

    }
    //--------------------------------------------------------------------
    void Start()
    {

    }
    //--------------------------------------------------------------------
    //-Player to sell cart code ------------------------------------------
    //--------------------------------------------------------------------
    private void PlayerSellCartAddButton() {
        for (int i = 0; i < playerSellList.Count; i++)
        {
            Item item = playerSellList[i];
            GameObject newButton = playerSellObjectPool.GetObject();
            newButton.transform.SetParent(playerSellContentPainel.transform);
            SampleButton sampleButton = newButton.GetComponent<SampleButton>();
            for (int j = 0; j < shopItemList.Count; j++)
            {
                if (item.itemName == shopItemList[j].itemShop.itemName)
                {
                    findItemOnShopToCopyValue = true;
                    shopPositonItemPriceTemp = j;
                }
            }
            if (!findItemOnShopToCopyValue)
            {
                ItemOnShop itm = new ItemOnShop();
                itm.SpringBuy = item.buyValue;
                itm.AutumBuy = item.buyValue;
                itm.WinterBuy = item.buyValue;
                itm.SummerBuy = item.buyValue;
                itm.SpringSell = item.sellValue;
                itm.AutumSell = item.sellValue;
                itm.WinterSell = item.sellValue;
                itm.SummerSell = item.sellValue;
                sampleButton.Setup(item, this, playerSellContentPainel, i, itm);
                playerSellButtonList.Add(sampleButton);
            }
            else {
                sampleButton.Setup(item, this, playerSellContentPainel, i, shopItemList[shopPositonItemPriceTemp]);
                playerSellButtonList.Add(sampleButton);
            }
            findItemOnShopToCopyValue = false;
        }
    }
    //--------------------------------------------------------------------
    private void PlayerSellCartRemoveButton() {
        while (playerSellContentPainel.transform.childCount > 0)
        {
            GameObject toRemove = playerSellContentPainel.transform.GetChild(0).gameObject;
            playerSellObjectPool.ReturnObject(toRemove);
        }
        playerSellButtonList.Clear();
    }
    //--------------------------------------------------------------------
    public void PlayerSellCartPlayerToCart(Item itemToAdd, int buyValue,int id) {
        balance += buyValue;
        UpdateBalance();
        playerSellList.Add(itemToAdd);
        for (int i = 0; i < playerButtonList.Count; i++)
        {
            if (playerButtonList[i].slotId == id) {
                playerList.RemoveAt(i);
            }
        }
        RefreshPlayerContent();
        RefreshSellCart();
    }
    //--------------------------------------------------------------------
    public void PlayerSellCartRemoveFromSellCart(int slotIdTemp) {
        for (int i = 0; i < playerSellList.Count; i++)
        {
            if (playerSellButtonList[i].slotId == slotIdTemp)
            {
                playerSellList.RemoveAt(i);
                playerList.Add(playerSellButtonList[i].item);
                balance -= playerSellButtonList[i].sellValue; 
            }
        }
        UpdateBalance();
        RefreshPlayerContent();
        RefreshSellCart();

    }
    //--------------------------------------------------------------------
    public void ClearSellShopCart() {
        for (int i = 0; i < playerSellList.Count; i++)
        {
            balance -= playerSellButtonList[i].sellValue;
        }
        playerSellList.Clear();
        RefreshSellCart();
        UpdateBalance();
    }
    //--------------------------------------------------------------------
    //-Player inventory Code ---------------------------------------------
    //--------------------------------------------------------------------
    private void PlayerAddButtons() {
        for (int i = 0; i < playerList.Count; i++)
        {
            Item item = playerList[i];
            GameObject newButton = playerObjectPool.GetObject();
            newButton.transform.SetParent(playerContentPainel.transform);
            SampleButton sampleButton = newButton.GetComponent<SampleButton>();
            for (int j = 0; j < shopItemList.Count; j++)
            {
                if (item.itemName == shopItemList[j].itemShop.itemName) {
                    haveItemOnShop = true;
                    shopPositonItemPriceTemp = j;
                }
            }
            if (haveItemOnShop)
            {
                sampleButton.Setup(item, this, playerContentPainel, i, shopItemList[shopPositonItemPriceTemp]);
            }
            else {
                sampleButton.Setup(item, this, playerContentPainel, i);
            }
            playerButtonList.Add(sampleButton);
        }
    }
    //--------------------------------------------------------------------
    private void PlayerRemoveButtons()
    {
        while (playerContentPainel.transform.childCount > 0)
        {
            GameObject toRemove = playerContentPainel.transform.GetChild(0).gameObject;
            playerObjectPool.ReturnObject(toRemove);
        }
        playerButtonList.Clear();
    }
    //--------------------------------------------------------------------
    public void PlayerAddFromCart() {
        if (gameManager.GetComponent<GameManagerScript>().tempPlayerObj.data.gold >= Mathf.Abs(balance)||balance >0)
        {
            gameManager.GetComponent<GameManagerScript>().tempPlayerObj.data.gold += balance;
            if (cartList.Count > 0)
            {
                for (int i = 0; i < cartList.Count; i++)
                {
                    playerList.Add(cartList[i]);
                }
                cartList.Clear();

                RefreshCart();
                balance = 0;
                UpdateBalance();

                gameManager.GetComponent<GameManagerScript>().tempPlayerObj.data.inventory = playerList;
                RefreshPlayerContent();
            }
            if (playerSellList.Count > 0) {
                playerSellList.Clear();
                RefreshSellCart();
                balance = 0;
                UpdateBalance();
                RefreshPlayerContent();
            }
         
        }
    }
    //--------------------------------------------------------------------
    public void LoadPlayerData() {
        if (gameManager.GetComponent<GameManagerScript>().getObj == true)
        {
            findPlayerData = true;
            tempData = gameManager.GetComponent<GameManagerScript>().tempPlayerObj.data;
            gold = tempData.gold;
            playerList = tempData.inventory;
            PlayerRemoveButtons();
            PlayerAddButtons();
        }
    }
    //--------------------------------------------------------------------
    //-Refresh Code-------------------------------------------------------
    //--------------------------------------------------------------------
    void RefreshDisplay()
    {
        
        RemoveButtons();
        AddToButtonsShopInventory();
        
    }
    //--------------------------------------------------------------------
    void RefreshCart() {
        RemoveButtonsCart();
        AddButtonsToCart();
    }
    //--------------------------------------------------------------------
    void RefreshPlayerContent() {
        gold = gameManager.GetComponent<GameManagerScript>().tempPlayerObj.data.gold;
        myGoldDisplay.text = "Gold: " + gold.ToString();
        PlayerRemoveButtons();
        PlayerAddButtons();
    }
    //--------------------------------------------------------------------
    private void RefreshSellCart()
    {
        PlayerSellCartRemoveButton();
        PlayerSellCartAddButton();
    }
    //--------------------------------------------------------------------
    //-Add to Inventory Code----------------------------------------------
    //--------------------------------------------------------------------
    private void AddToButtonsShopInventory()
    {
        for (int i = 0; i < shopItemList.Count; i++)
        {
            Item item = shopItemList[i].itemShop;
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel.transform);

            SampleButton sampleButton = newButton.GetComponent<SampleButton>();
            sampleButton.Setup(item, this, contentPanel, i, shopItemList[i]);
        }
    }
    //--------------------------------------------------------------------
    private void RemoveButtons()
    {
        while (contentPanel.transform.childCount > 0)
        {
            GameObject toRemove = contentPanel.transform.GetChild(0).gameObject;
            buttonObjectPool.ReturnObject(toRemove);
        }
    }
    //--------------------------------------------------------------------
    //add to Cart \/ code ------------------------------------------------
    //--------------------------------------------------------------------
    public void AddToCart(Item itemToAdd,int buyValue)
    {
        balance -= buyValue;
        UpdateBalance();
        cartList.Add(itemToAdd);
        RemoveButtonsCart();
        AddButtonsToCart();
    }
    //--------------------------------------------------------------------
    public void RemoveFromCart(int slotIdTemp)
    {
        for (int i = 0; i < cartList.Count; i++)
        {
            if (cartButtonList[i].slotId == slotIdTemp) {
                cartList.RemoveAt(i);
                balance += cartButtonList[i].buyValue;
            }
        }
        UpdateBalance();
        RemoveButtonsCart();
        AddButtonsToCart();
    }
    //-------------------------------------------------------------------------
    private void RemoveButtonsCart(){
        while (cartPanel.transform.childCount > 0)
        {
            GameObject toRemove = cartPanel.transform.GetChild(0).gameObject;
            cartObjectPool.ReturnObject(toRemove);
        }
       
        cartButtonList.Clear();
       
    }
    //-------------------------------------------------------------------------
    private void AddButtonsToCart()
    {
        for (int i = 0; i < cartList.Count; i++)
        {

            Item item = cartList[i];
            GameObject newButton = cartObjectPool.GetObject();
            newButton.transform.SetParent(cartPanel.transform);

            SampleButton sampleButton = newButton.GetComponent<SampleButton>();
            for (int j = 0; j < shopItemList.Count; j++)
            {
                if (item.itemName == shopItemList[j].itemShop.itemName)
                {
                  shopPositonItemPriceTemp = j;
                }
            }
            sampleButton.Setup(item, this, cartPanel, i, shopItemList[shopPositonItemPriceTemp]);
            cartButtonList.Add(sampleButton);
        }
    }
    //-------------------------------------------------------------------------
    public void UpdateBalance() {
        if (balance > 0)
        {
            tradeBalanceTxt.text = "<color=lime>" + balance.ToString() + "</color>";
        }
        else {
            tradeBalanceTxt.text = "<color=red>" + balance.ToString() + "</color>";
        }

        if (balance == 0) {
            tradeBalanceTxt.text = "";
        }
    }
    //-------------------------------------------------------------------------
    public void ClearCart() {
        for (int i = 0; i < cartList.Count; i++)
        {
            balance += cartButtonList[i].buyValue;
        }

        cartList.Clear();
        RefreshCart();
        UpdateBalance();
    }
    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------


}