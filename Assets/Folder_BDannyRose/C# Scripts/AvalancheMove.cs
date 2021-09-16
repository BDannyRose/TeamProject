using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvalancheMove : MonoBehaviour
{
    public Transform player;
    private float moveSpeed = 0;
    private Vector3 moveDir;
    private float dangerZone = 300;
    [SerializeField]
    private float upperLimit = 1.5f, lowerLimit = 0.9f;
    [SerializeField]
    private int ticksTooFast = 50, ticksTooSlow = 100, minSpeed = 20; 
    //TicksTooFast - как быстро лавина замедляется
    //TicksTooSlow - как быстро лавина ускоряется
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveAvalanche();
    }
    void moveAvalanche()
    {
        moveDir = (player.position - transform.position).normalized;
        if (player.position.x - this.transform.position.x > dangerZone)
        {
            if (moveSpeed < player.GetComponent<Move>().SpeedX * upperLimit)
            {
                moveSpeed += (player.GetComponent<Move>().SpeedX) / ticksTooSlow;
            }
            else
            {
                moveSpeed -= (player.GetComponent<Move>().SpeedX) / ticksTooFast;
            }
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
        else
        {
            if (moveSpeed < minSpeed)
            {
                moveSpeed += minSpeed / ticksTooSlow;
            }
            else if (moveSpeed > player.GetComponent<Move>().SpeedX * lowerLimit)
            {
                moveSpeed -= (player.GetComponent<Move>().SpeedX) / ticksTooFast;
            }
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
    }
}
