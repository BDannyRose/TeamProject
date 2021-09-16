using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject player;
    public GameObject DS;
    public AvalancheMove avalancheMovement;
    public bool Die;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        avalancheMovement = GameObject.FindWithTag("Avalanche").GetComponent<AvalancheMove>();
        Die = false;
        DS.SetActive(false);
    }

    void Update()
    {
        if (Die)
        {
            avalancheMovement.enabled = false;
            DS.SetActive(true);
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            Time.timeScale = 0;
        }

    }

}
