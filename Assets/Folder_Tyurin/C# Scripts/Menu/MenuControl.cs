using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject Inv;
    public GameObject DeathScreen;
    public Inventory2 updateInv;

    public void Start()
    {
        Time.timeScale = 1;
    }

    public void Update()
    {
        updateInv = GameObject.Find("InvControl").GetComponent<Inventory2>();
        updateInv.InventoryMainObject = Inv;
    }

    public void Exit()
    {
        
        updateInv.SaveInventory();
        Application.Quit();
    }

    public void Restart()
    {
        updateInv = GameObject.Find("InvControl").GetComponent<Inventory2>();
        updateInv.SaveInventory();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void ActiveInventory()
    {
        updateInv = GameObject.Find("InvControl").GetComponent<Inventory2>();
        Inventory.SetActive(true);
        DeathScreen.SetActive(false);
        updateInv.SaveInventory();
        updateInv.UpdateInventory();
    }

    public void DeactiveInventory()
    {
        updateInv = GameObject.Find("InvControl").GetComponent<Inventory2>();
        updateInv.UpdateInventory();
        updateInv.SaveInventory();
        Inventory.SetActive(false);
        DeathScreen.SetActive(true);
    }

    public void ResetAllProgress()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
