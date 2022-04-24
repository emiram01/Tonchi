using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    private SpriteRenderer sprite;


    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        Color c = sprite.material.color;
        c.a = 0f;
        sprite.material.color = c;
    }

    IEnumerator Fade()
    {
        for(float i = 0.05f; i<= 1; i += 0.05f)
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
