using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public float minRotationSpeed, maxRotationSpeed;

    private float rotationSpeed;
    private bool rotateLeft = false, canRotate = false;

    void Start()
    {
        ChangeChildrenColor();           
        FindObjectOfType<ColorManager>().CheckColors();

        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);        
        DecideRotation();

    }

    void Update()
    {
        if (canRotate)
        {
            if(rotateLeft)
                transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);               
            if (!rotateLeft)
                transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);               
        }
    }

    public void DecideRotation()
    {
        switch (Random.Range(1, 3))             
        {
            case 1:
                canRotate = true;
                rotateLeft = true;
                break;
            case 2:
                canRotate = true;
                rotateLeft = false;
                break;
        }
    }

    public void ChangeChildrenColor()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            FindObjectOfType<ColorManager>().SetRandomColor(transform.GetChild(i).GetComponent<SpriteRenderer>());
        }

        if (GameObject.FindGameObjectWithTag("Arrow") != null)
            FindObjectOfType<ColorManager>().ChangeColor(transform.GetChild(0).GetComponent<SpriteRenderer>(), GameObject.FindGameObjectWithTag("Arrow").GetComponent<SpriteRenderer>());
    }
}
