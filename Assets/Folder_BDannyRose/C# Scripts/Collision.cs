using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public Inventory2 inv2 = null;
    /*
     * Object IDs:
     * [Animals]:
     * ###Peaceful###
     * Deer = 100
     * Fox = 101
     * Grounhog = 102
     * Yak = 103
     * ###Aggressive###
     * Bear = 200
     * Hawk = 201
     * Snow Leopard = 202
     * Wolf = 203
     * ###Strange###
     * Bigfoot = 250
     * [Bonuses]:
     * BerserkerBerries = 300
     * Chest = 301
     * Clothes = 302
     * EnergyDrink = 303
     * Fuel = 304
     * House = 305
     * Jetpack = 306
     * Poacher Camp = 307
     * Snowboard = 308
     * [Roadblocks]
     * Bushes = 400
     * Fallen Tree = 401
     * Snow Pile = 402
     * Stone = 403
     */

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inv2 == null)
        {
            inv2 = GameObject.Find("InvControl").GetComponent<Inventory2>();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public int OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Avalanche"))
        {
            gameObject.GetComponent<Death>().Die = true;
        }
        else if (collision.transform.parent.gameObject.CompareTag("Animal"))
        {
            Debug.Log(collision.transform.parent.gameObject.name);
            Destroy(collision.transform.parent.gameObject, 1f);
            inv2.AddItemFromCollision(collision.transform.GetComponentInParent<ObjectID>().ID, 1);
        }
        /*else
        {
            Destroy(collision.GetComponentInParent<Transform>().gameObject, 1f);
        }*/
        return 0;
    }
}
