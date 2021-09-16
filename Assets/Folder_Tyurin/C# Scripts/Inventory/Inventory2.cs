using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Inventory2 : MonoBehaviour
{
    //public static Inventory instance = null;

    public DataBase data;
    public List<ItemInventory> items = new List<ItemInventory>();

    public GameObject InventoryMainObject;

    public GameObject gameObjShow;

    public int maxCount;
    public int f = 1;
    public Camera cam;
    public EventSystem es;

    public int currentId = -1;
    public ItemInventory currentItem;

    public bool reload;

    public void Start()
    {
        
    }

    private void Update()
    {

        //SaveInventory();
        if (reload == true)
        {
            f = 0;
            reload = false;
        }
        
        
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        data = cam.GetComponent<DataBase>();
        if (InventoryMainObject.transform.childCount == 0)
        {
            CreateInventory();
        }
        
        
        /*if (f == 0) // заполнение инвентаря для теста 
        {
            for (int i = 0; i < maxCount; i += 1) 
            {
                AddItem(i, data.items[Random.Range(0, data.items.Count)], Random.Range(1, 128));
            }
            f++;
        }*/
        

    }

    public void CreateInventory()
    {
        AddGraphics();
        LoadInventory();
    }

    public void AddItemFromCollision( int id, int count)
    {
        //int dataNumber = 0;

        /*
        int f = 0;
        do
        {
            f++;
            if (id == data.items[f].ID)
            {
                dataNumber = f;
                Debug.Log(dataNumber);
                break;
                
            }
            
        } while (id != data.items[f].ID);
        */
        /*
        for (int i = 0; i < data.items.Count; i++) 
        { 
            if (id == data.items[i].ID)
            {
                dataNumber = i;
                Debug.Log(dataNumber);
                break;
            }
            
        }
        */

        for (int j = 0; j < maxCount; j++)
        {
            if (items[j].ID == 0)
            {
                items[j].ID = id;
                items[j].count = 1;
                break;
            }
            else if (items[j].ID == id)
            {
                items[j].count += count;

                break;
            }
        }
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
            DontDestroyOnLoad(newItem);
            newItem.name = i.ToString();

            ItemInventory ii = new ItemInventory();
            ii.itemGameObj = newItem;

            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = newItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { SelectObject(); });

            if (items.Count < maxCount)
            {
                items.Add(ii);
            }
        }
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].ID != 0 && items[i].count > 1)
            {
                GameObject.Find(i.ToString()).GetComponentInChildren<Text>().text = items[i].count.ToString();
            }
            else
            {
                GameObject.Find(i.ToString()).GetComponentInChildren<Text>().text = "";
            }
            GameObject.Find(i.ToString()).GetComponent<Image>().sprite = data.items[items[i].ID].IMG;
            items[i].itemGameObj = GameObject.Find(i.ToString());
        }
    }

    public void LoadInventory()
    {
        for (int i = 0; i < maxCount; i++)
        {
            items[i].count = PlayerPrefs.GetInt("Count: " + i.ToString());
            items[i].ID = PlayerPrefs.GetInt("ID: " + i.ToString());
            items[i].itemGameObj = GameObject.Find(i.ToString());
        }
    }

    public void SaveInventory()
    {
        for (int i = 0; i < maxCount; i++)
        {
            PlayerPrefs.SetInt("Count: " + i.ToString(), items[i].count);
            PlayerPrefs.SetInt("ID: " + i.ToString(), items[i].ID);
            
        }
    }

    public void SelectObject()
    {

        if (currentId == -1)
        {
            if (items[int.Parse(es.currentSelectedGameObject.name)].ID == 0)
            {
                UpdateInventory();
                return;
            }
            else
            {
                currentId = int.Parse(es.currentSelectedGameObject.name);
                currentItem = CopyInventoryItem(items[currentId]);
                UpdateInventory();
            }
        }
        else if (items[int.Parse(es.currentSelectedGameObject.name)].ID != items[currentId].ID)
        {
            AddInventoryItem(currentId, items[int.Parse(es.currentSelectedGameObject.name)]);

            AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
            currentId = -1;
            UpdateInventory();
        }
        else if (items[int.Parse(es.currentSelectedGameObject.name)].ID == items[currentId].ID) 
        {
            if (items[int.Parse(es.currentSelectedGameObject.name)].count < 128)
            {
                items[int.Parse(es.currentSelectedGameObject.name)].count += items[currentId].count;

                if (items[int.Parse(es.currentSelectedGameObject.name)].count > 128)
                {
                    items[currentId].count = items[int.Parse(es.currentSelectedGameObject.name)].count - 128;
                    items[int.Parse(es.currentSelectedGameObject.name)].count = items[int.Parse(es.currentSelectedGameObject.name)].count - items[currentId].count;
                }
                else
                {
                    items[currentId].ID = 0;
                    items[currentId].count = 0;
                }
                
            }
            currentId = -1;
            UpdateInventory();
        }
    }

    public void SearchForSameItem(Item item, int count)
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].ID == item.ID)
            {
                if (items[i].count < 128)
                {
                    items[i].count += count;

                    if (items[i].count > 128)
                    {
                        count = items[i].count - 128;
                        items[i].count = 64;
                    }
                    else
                    {
                        count = 0;
                        i = maxCount;
                    }
                }
            }
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

    [System.Serializable]

    public class ItemInventory
    {
        public int ID;
        public GameObject itemGameObj;
        public int count;
    }
}