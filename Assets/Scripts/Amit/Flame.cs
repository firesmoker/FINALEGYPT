using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    BoxCollider2D myFlame;

    bool CanBurn = false;

    // Start is called before the first frame update
    void Start()
    {
        myFlame = GetComponent<BoxCollider2D>();
        StartCoroutine(FlameStart());
    }

   

    IEnumerator FlameStart()
    {
        yield return new WaitForSeconds(2);
        CanBurn = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!CanBurn) { return; }

        if(collision.gameObject.tag == "Flammable")
        {
            collision.gameObject.GetComponent<Flammable>().FlameOn();
        }
    }
}
