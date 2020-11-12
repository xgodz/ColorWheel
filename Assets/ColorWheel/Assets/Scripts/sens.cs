using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class sens : MonoBehaviour
{
    public BoxCollider2D box;

    
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        
        
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                box.enabled = true;
            }
            else
            {
                box.enabled = false;
            }
        
        
    }
}
