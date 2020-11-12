using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public Color[] colors;
    public bool[] activeColors;            


    public void SetRandomColor(Renderer obj)
    {
        obj.material.color = colors[Random.Range(0, colors.Length)];
    }

    public void SetRandomColor(SpriteRenderer obj)
    {
        obj.color = colors[Random.Range(0, colors.Length)];
    }

    public void ChangeColor(Renderer from, Renderer to)
    {
        from.material.color = to.material.color;
    }

    public void ChangeColor(SpriteRenderer from, SpriteRenderer to)
    {
        from.color = to.color;
    }

    public void ChangeColor(ParticleSystem particleFrom, SpriteRenderer spriteTo)
    {
#pragma warning disable CS0618      
        particleFrom.startColor = spriteTo.color;
#pragma warning restore CS0618      
    }

    public bool CompareColor(SpriteRenderer obj1, Color col)
    {
        return obj1.color == col;
    }

    public bool CompareColor(Renderer obj1, Renderer obj2)
    {
        return obj1.material.color == obj2.material.color;
    }

    public bool CompareColor(SpriteRenderer obj1, SpriteRenderer obj2)
    {
        return obj1.color == obj2.color;
    }

    public void SetRandomActiveColor(SpriteRenderer obj)
    {
        int randomIndex;

        do
        {
            randomIndex = Random.Range(0, colors.Length);
        } while (activeColors[randomIndex] == false);

        obj.color = colors[randomIndex];
    }

    public void CheckColors()
    {
        for (int i = 0; i < colors.Length; i++)
        {
            activeColors[i] = false;
        }

        foreach (GameObject obstacle in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            for (int i = 0; i < colors.Length; i++)
            {
                if (CompareColor(obstacle.GetComponent<SpriteRenderer>(), colors[i]))
                    activeColors[i] = true;
            }
        }
    }
}
