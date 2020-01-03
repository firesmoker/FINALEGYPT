using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head2D : MonoBehaviour
{
    Player2D myPlayer;

    private void Start()
    {
        myPlayer = GetComponentInParent<Player2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlatformBody" && collision != null)
        {
            Debug.Log("ouch");
            myPlayer.ResetY();
        }
    }

}
