using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerRotation : MonoBehaviour
{
    public float minSpeed = 80f, maxSpeed = 180f, increaseSpeedBy = 10f;

    private Animation sensorAnim;
    private float rotationSpeed;
    private ParticleSystem clickParticle1, clickParticle2;
    private SpriteRenderer rodRenderer;

    [HideInInspector]
    public bool rotateLeft = true, canRotate = true;

    void Start()
    {
        clickParticle1 = transform.GetChild(0).GetComponent<ParticleSystem>();
        clickParticle2 = clickParticle1.transform.GetChild(0).GetComponent<ParticleSystem>();
        rodRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();          
        sensorAnim = GameObject.FindGameObjectWithTag("Sensor").GetComponent<Animation>();            
        rotationSpeed = minSpeed;
    }

    void Update()
    {
        if (canRotate)
        {
            if (rotateLeft)
                transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);         
            else
                transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);        
        }
    }

    public void ClickButton()
    {
        canRotate = false;

        Invoke("ChangeDirection", 0.02f);
    }

    public void ChangeDirection()
    {
        canRotate = true;

        FindObjectOfType<ColorManager>().ChangeColor(clickParticle1, rodRenderer);
        FindObjectOfType<ColorManager>().ChangeColor(clickParticle2, rodRenderer);
        clickParticle1.Play();

                      
        if (rotationSpeed + increaseSpeedBy <= maxSpeed)
            rotationSpeed += increaseSpeedBy;        

        rotateLeft = !rotateLeft;           
    }
}
