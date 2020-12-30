using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private ColorKeys.KeyType keyType = 0;
    private KeyHolder keyHolder = null;
    private bool open = true;

    public ColorKeys.KeyType GetKeyType()
    {
        return keyType;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        KeyDoor keyDoor = this;
        keyHolder = collision.gameObject.GetComponent<KeyHolder>();
        Image[] keys = keyHolder.keys;
        if(collision.gameObject.tag == "Player" && keyHolder.HasKey(keyDoor.GetKeyType()))
        {
            if(open)
            {
                FindObjectOfType<AudioManager>().Play("DoorOpen");
                open = false;
            }
            keyHolder.RemoveKey(keyDoor.GetKeyType());
            Destroy(keyDoor.gameObject);
            if(keyDoor.GetKeyType() == ColorKeys.KeyType.Gold)
            {
                keys[0].enabled = false;
            }
            if(keyDoor.GetKeyType() == ColorKeys.KeyType.Green)
            {
                keys[1].enabled = false;
            }
            if(keyDoor.GetKeyType() == ColorKeys.KeyType.Blue)
            {
                keys[2].enabled = false;
            }
            if(keyDoor.GetKeyType() == ColorKeys.KeyType.Purple)
            {
                keys[3].enabled = false;
            }
            if(keyDoor.GetKeyType() == ColorKeys.KeyType.Red)
            {
                keys[4].enabled = false;
            }
        }
    }
}
