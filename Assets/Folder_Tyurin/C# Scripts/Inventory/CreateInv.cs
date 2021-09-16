using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreateInv : MonoBehaviour
{
    public DataBase data;
    public List<ItemInventory> items = new List<ItemInventory>();

    public GameObject InvControl;
    public Inventory2 CreateItems;

    public GameObject InventoryMainObject;

    public int maxCount;

    public GameObject gameObjShow;

    void Start()
    {
        if (GameObject.Find("InvControl") == false)
        {
            Instantiate(InvControl).name = "InvControl";
            DontDestroyOnLoad(GameObject.Find("InvControl"));
        }
        InvControl = GameObject.Find("InvControl");
        //CreateItems = InvControl.GetComponent<Inventory2>();
        //CreateItems.AddGraphics();
    }
}

[System.Serializable]

public class ItemInventory
{
    public int ID;
    public GameObject itemGameObj;
    public int count;
}