using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int numOfHearts = 0;
    [SerializeField] private Image[] hearts = null;
    [SerializeField] private Sprite full = null;
    [SerializeField] private Sprite empty= null;
    //[SerializeField] private Animator animator = null;
    [SerializeField] private SpriteRenderer sprite = null;
    [SerializeField] private float invincibleTime = 0;
    public int health;
    private Color b;
    private bool invincible;
    private float currTime;

    void Start()
    {
        health = GameManager.instance.currHealth;
        b = this.sprite.material.color;
    }

    public void AddHealth(int healthAmount)
    {
        FindObjectOfType<AudioManager>().Play("Heal");
        health += healthAmount;
    }

    public void SubHealth(int healthSub)    
    {
        if(!invincible)
        {
            health -= healthSub;
            FindObjectOfType<AudioManager>().Play("Meow");
            ScreenShake.Instance.Shake(.5f, .1f);
            StartCoroutine("Fade");
            invincible = true;
            currTime = invincibleTime;
        } 
    }

    IEnumerator Fade()
    {
        for(float i = 1f; i >= -0.05f; i -= 0.05f)
        {
            Color c = sprite.material.color;
            c.a = i;
            sprite.material.color = c;
            yield return new WaitForSeconds(0.001f);
        }
        yield return new WaitForSeconds(0.2f);
    }

    void Update()
    {
        sprite.material.color = b;
        
        if(health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = full;
            }
            else
            {
                hearts[i].sprite = empty;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if(invincible) 
            currTime -= Time.deltaTime;

        if(currTime < 0) 
            invincible = false;

        if(health <= 0)
        {
            FindObjectOfType<PlayerPos>().Respawn();
        }
    }
}
