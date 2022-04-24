using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyHolder : MonoBehaviour
{
    public Image[] keys = null;
    private List<ColorKeys.KeyType> keyList = null;

    private void Awake()
    {
        keyList = new List<ColorKeys.KeyType>();
    }

    public void AddKey(ColorKeys.KeyType keyType)
    {
        keyList.Add(keyType);
        FindObjectOfType<AudioManager>().Play("Heal");
    }

    public void RemoveKey(ColorKeys.KeyType keyType)
    {
        keyList.Remove(keyType);
    }

    public bool HasKey(ColorKeys.KeyType keyType)
    {
        return keyList.Contains(keyType);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ColorKeys key = collider.GetComponent<ColorKeys>();
        if(key != null)
        {
            AddKey(key.GetKeyType());
            Destroy(key.gameObject);
            if(key.GetKeyType() == ColorKeys.KeyType.Gold)
            {
                keys[0].enabled = true;
            }
            if(key.GetKeyType() == ColorKeys.KeyType.Green)
            {
                keys[1].enabled = true;
            }
            if(key.GetKeyType() == ColorKeys.KeyType.Blue)
            {
                keys[2].enabled = true;
            }
            if(key.GetKeyType() == ColorKeys.KeyType.Purple)
            {
                keys[3].enabled = true;
            }
            if(key.GetKeyType() == ColorKeys.KeyType.Red)
            {
                keys[4].enabled = true;
            }
        }
    }
}
