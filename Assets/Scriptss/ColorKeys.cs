using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorKeys : MonoBehaviour
{
    [SerializeField] private KeyType keyType = 0;

    public enum KeyType
    {
        Gold, Red, Green, Blue, Purple
    }

    public KeyType GetKeyType()
    {
        return keyType;
    }
}
