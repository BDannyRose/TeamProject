using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Inventory : MonoBehaviour
{
    //public static Inventory instance = null;

    public DataBase data;
    public List<ItemInventory> items = new List<ItemInventory>();
    public List<InventoryInformation> invInf = new List<InventoryInformation>();

    public GameObject InventoryMainObject;

    public GameObject gameObjShow;

    public int maxCount;

    public Camera cam;
    public EventSystem es;

    public int currentId;
    public ItemInventory currentItem;

    public void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        InventoryMainObject = GameObject.Find("Inv");


        if (items.Count == 0 && InventoryMainObject.transform.childCount == 0)
        {
            for (int i = 0; i < maxCount; i += 1) // заполнение инвентаря для теста 
            {
                AddItem(i, data.items[Random.Range(0, data.items.Count)], Random.Range(1, 128));
            }
            
        }
    }

    private void Update()
    {
        UpdateInventory();
        
    }

    public void AddItem(int id, Item item, int count)
    {
        items[id].ID = item.ID;
        items[id].count = count;
        items[id].itemGameObj.GetComponent<Image>().sprite = item.IMG;

        if (count > 1 && item.ID != 0)
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = count.ToString();
        }
        else
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddInventoryItem(int id, ItemInventory invItem)
    {
        items[id].ID = invItem.ID;
        items[id].count = invItem.count;
        items[id].itemGameObj.GetComponent<Image>().sprite = data.items[invItem.ID].IMG;

        if (invItem.count > 1 && invItem.ID != 0)
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = invItem.count.ToString();
        }
        else
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddGraphics()
    {
        for (int i = 0; i < maxCount; i++)
        {
            GameObject newItem = Instantiate(gameObjShow, InventoryMainObject.transform) as GameObject;

            newItem.name = i.ToString();

            ItemInventory ii = new ItemInventory();
            ii.itemGameObj = newItem;

            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = newItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { SelectObject(); });

            items.Add(ii);
        }
       
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].ID != 0 && items[i].count > 1)
            {
                items[i].itemGameObj.GetComponentInChildren<Text>().text = items[i].count.ToString();
            }
            else
            {
                items[i].itemGameObj.GetComponentInChildren<Text>().text = "";
            }
            items[i].itemGameObj.GetComponent<Image>().sprite = data.items[items[i].ID].IMG;
        }
        SaveInv();
    }

    public void SelectObject()
    {
        
        if (currentId == -1)
        {
            if (items[int.Parse(es.currentSelectedGameObject.name)].ID == 0)
            {
                return;
            }
            else
            {
                currentId = int.Parse(es.currentSelectedGameObject.name);
                currentItem = CopyInventoryItem(items[currentId]);
                AddItem(currentId, data.items[0], 0);
                UpdateInventory();
            }
        }
        else
        {
            AddInventoryItem(currentId, items[int.Parse(es.currentSelectedGameObject.name)]);

            AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
            currentId = -1;
            UpdateInventory();
        }
    }

    public ItemInventory CopyInventoryItem(ItemInventory old)
    {
        ItemInventory New = new ItemInventory();

        New.ID = old.ID;
        New.itemGameObj = old.itemGameObj;
        New.count = old.count;

        return New;
    }
    
    public void SaveInv()
    {
        for (int i = 0; i < maxCount; i++)
        {
            invInf[i].ItemID = items[i].ID;
            invInf[i].NumberOfItems = items[i].count;
        }
    }

    [System.Serializable]

    public class InventoryInformation
    {
        public int ItemID;
        public int NumberOfItems;
    }

    [System.Serializable]

    public class ItemInventory
    {
        public int ID;
        public GameObject itemGameObj;
        public int count;
    }
}