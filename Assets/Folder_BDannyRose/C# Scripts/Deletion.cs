using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deletion : MonoBehaviour
{
    
    public GameObject deletionPoint;
    // Start is called before the first frame update
    void Start()
    {
        deletionPoint = GameObject.Find("DeletionPoint");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (deletionPoint.transform.position.x > gameObject.transform.position.x)
        {
            Destroy(gameObject);
        }
    }
    
}
