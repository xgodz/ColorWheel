using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collision : MonoBehaviour
{
    public TextMeshPro counterText;
    public int maxCount = 15;

    private SpriteRenderer arrowRenderer, rodRenderer;
    private int counter = 2, tempCount = 2;

    void Start()
    {
        counterText.text = counter.ToString();
        arrowRenderer = GameObject.FindGameObjectWithTag("Arrow").GetComponent<SpriteRenderer>();
        rodRenderer = GameObject.FindGameObjectWithTag("Rod").GetComponent<SpriteRenderer>();

        ChangePointerColor();            
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))           
        {
            if (FindObjectOfType<ColorManager>().CompareColor(arrowRenderer, collision.GetComponent<SpriteRenderer>()))               
            {
                if (counter > 1)
                {
                    counter--;
                    counterText.text = counter.ToString();
                }
                else
                {
                    tempCount++;
                    counter = tempCount;
                    counterText.text = counter.ToString();

                    FindObjectOfType<Spawner>().Spawn();          
                }

                FindObjectOfType<ScoreManager>().IncrementScore(tempCount, counter);         
                Invoke("ChangePointerColor", 0.05f);             
            }
            else          
            {
                FindObjectOfType<GameManager>().EndPanelActivation();
            }
        }
    }

    public void ChangePointerColor()
    {
        FindObjectOfType<ColorManager>().SetRandomActiveColor(arrowRenderer);            
        FindObjectOfType<ColorManager>().ChangeColor(rodRenderer, arrowRenderer);          
    }
}
