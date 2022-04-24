using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    IEnumerator Fade()
    {
        for(float i = 1f; i >= -0.05f; i -= 0.05f)
        {
            Color c = sprite.material.color;
            c.a = i;
            sprite.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void StartFade()
    {
        StartCoroutine("Fade");
    }
}
