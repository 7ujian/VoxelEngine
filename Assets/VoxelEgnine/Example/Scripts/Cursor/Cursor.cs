using System.Collections;
using UnityEngine;

public class Cursor:MonoBehaviour
{
    private Renderer renderer;
    
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }
    public void FadeOutAndIn()
    {
        StartCoroutine(CoFadeOutAndIn());
    }

    IEnumerator CoFadeOutAndIn()
    {
        float a = 1;
        while (a > 0)
        {
            a -= 0.01f;
            if (a < 0)
                a = 0;
            var color = renderer.material.color;
            renderer.material.color = new Color(color.r, color.g, color.b, a);
            yield return null;
        }
        
        while (a < 1)
        {
            a += 0.01f;
            if (a > 1)
                a = 1;
            var color = renderer.material.color;
            renderer.material.color = new Color(color.r, color.g, color.b, a);
            yield return null;
        }
    }
}
