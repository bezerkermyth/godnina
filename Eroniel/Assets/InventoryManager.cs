using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject databaseTemp;
    public List<Item> shopAvaibleItems;


    public List<GameObject> slots = new List<GameObject>();
    public GameObject slot;
    public GameObject itemObj;


    //contentHolder
    //---------------
    public GameObject shopContentHolder;


    private void Awake()
    {
        for (int i = 0; i < shopAvaibleItems.Count; i++)
        {
            slots.Add(Instantiate(slot));
            slots[i].transform.SetParent(shopContentHolder.transform);
            GameObject itemTemp = Instantiate(itemObj);
            itemTemp.transform.SetParent(slots[i].transform);
            itemTemp.transform.position = Vector2.zero;
            itemTemp.transform.localPosition = Vector2.zero;
            itemTemp.transform.GetChild(0).GetComponent<Text>().text = shopAvaibleItems[i].sellValue.ToString();
            itemTemp.GetComponent<Image>().sprite = shopAvaibleItems[i].artwork;
            /*
         data.transform.GetChild (0).GetComponent<Text> ().text = "x"+items[i].ItemStack.ToString();    
         */
        }
    }



}
