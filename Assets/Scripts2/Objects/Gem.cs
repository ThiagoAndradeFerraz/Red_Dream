using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorGem
{
    RED, GREEN, BLUE
}

public class Gem : MonoBehaviour
{
    [Header("Color of the gem")]
    [SerializeField] private ColorGem color;

    void OnTriggerEnter()
    {
        //Debug.Log(collision.gameObject.tag);
        Destroy(gameObject);
    }
}
