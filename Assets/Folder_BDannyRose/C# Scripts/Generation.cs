using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Generation : MonoBehaviour
{
    public GameObject[] allPlatforms = new GameObject[25];
    public GameObject[] roadBlocks = new GameObject[5];
    public GameObject[] bonuses = new GameObject[8];
    public GameObject[] animals = new GameObject[9];
    private int numOfGenerationPoints;
    private Transform stuffParent;
    private Transform stuffChild;
    private GameObject stuffToGenerate;
    private Vector2 stuffPosition;

    private enum typeOfStuff { roadBlock, bonus, animal };

    public int numOfPlatforms = 25;
    public int platformIndex = 0;
    public GameObject platform;

    public Transform generationPoint;

    public int minDist;
    public int maxDist;

    public float platformWidthDiff = 256;
    public float platformHeightDiff = 256;

    private int aggregatedExtraDistance = 0;
    private int abyssDist;
    public int abyss;
    void Start()
    {
        
    }
    
    void FixedUpdate()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            createPlatform();
            createStuffOnPlatform();
        }
    }

    private void createPlatform()
    {
        abyss = 0;
        switch (platformIndex % 5)
        {
            case 0:
                {
                    platformIndex = Random.Range(0, 5);
                    break;
                }
            case 1:
                {
                    platformIndex = Random.Range(5, 10);
                    break;
                }
            case 2:
                {
                    platformIndex = Random.Range(10, 15);
                    break;
                }
            case 3:
                {
                    platformIndex = Random.Range(15, 20);
                    if ((abyss = Random.Range(0, 2)) == 1)
                    {
                        abyssDist = Random.Range(minDist, maxDist);
                        platformIndex = Random.Range(0, 15);
                    }
                    break;
                }
            case 4:
                {
                    platformIndex = Random.Range(20, 25);
                    break;
                }
        }
        transform.position = new Vector2(transform.position.x + platformWidthDiff + abyssDist, transform.position.y - platformHeightDiff / 2);
        platform = Instantiate(allPlatforms[platformIndex], transform.position, transform.rotation);
        abyssDist = 0;
    }

    private void createStuffOnPlatform()
    {
        stuffParent = platform.transform.GetChild(1);
        numOfGenerationPoints = stuffParent.childCount;
        for (int i = 0; i < numOfGenerationPoints; i++)
        {
            switch(Random.Range((int)typeOfStuff.roadBlock, (int)typeOfStuff.animal + 1))
            {
                case (int)typeOfStuff.roadBlock:
                    {
                        stuffToGenerate = roadBlocks[Random.Range(0, roadBlocks.Length)];
                        break;
                    }
                case (int)typeOfStuff.bonus:
                    {
                        stuffToGenerate = bonuses[Random.Range(0, bonuses.Length)];
                        break;
                    }
                case (int)typeOfStuff.animal:
                    {
                        stuffToGenerate = animals[Random.Range(0, animals.Length)];
                        break;
                    }
            }
            stuffChild = stuffParent.transform.GetChild(i);
            stuffPosition = new Vector2(stuffChild.position.x, stuffChild.position.y);
            Instantiate(stuffToGenerate, stuffPosition, stuffChild.rotation, stuffParent);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.CompareTag("Ground"))
        {

        }
    }*/
}
