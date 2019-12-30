using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchScript : MonoBehaviour
{
    CapsuleCollider2D myFlame;

    // Start is called before the first frame update
    void Start()
    {
        myFlame = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Quench")
        {
            //QuenchFire();
        }
        if(collision.gameObject.tag == "Flammable" )
        {
            collision.gameObject.GetComponent<Flammable>().FlameOn();
        }
    }

    
}
