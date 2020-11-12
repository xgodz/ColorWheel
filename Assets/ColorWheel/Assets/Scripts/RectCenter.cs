using UnityEngine;
using System.Collections;

public class RectCenter : MonoBehaviour
{
    public Rect rect = new Rect(0, 0, 100, 100);
    void Example()
    {
        print(rect.center);
        rect.center = new Vector2(10, 10);
    }
}