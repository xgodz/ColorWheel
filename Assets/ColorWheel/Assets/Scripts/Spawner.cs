using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstacles;

    private GameObject tempObstacle = null;

    [HideInInspector]
    public int obstacleType = 0;

    void Start()
    {
        Spawn();        
    }

    public void Spawn()
    {
        if (!FindObjectOfType<GameManager>().gameIsOver)        
        {
            if (tempObstacle != null)      
            {
                for (int i = 0; i < tempObstacle.transform.childCount; i++)     
                    tempObstacle.transform.GetChild(i).tag = "Untagged";

                tempObstacle.GetComponent<Animation>().Play();       
                Destroy(tempObstacle.gameObject, 0.35f);        
            }

            tempObstacle = Instantiate(obstacles[obstacleType], transform.position, Quaternion.identity);        

            if (obstacleType < obstacles.Length - 1)
                obstacleType++;

            FindObjectOfType<ScoreManager>().ChangeLevelTexts();       
        }
    }
}
