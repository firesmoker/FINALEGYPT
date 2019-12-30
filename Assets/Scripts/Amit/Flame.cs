using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    BoxCollider2D myFlame;

    [SerializeField] bool CanBurn = false;

    // Start is called before the first frame update
    void Start()
    {
        myFlame = GetComponent<BoxCollider2D>();
        StartCoroutine(FlameStart());
    }

   
    IEnumerator FlameStart()
    {
        yield return new WaitForSeconds(1);
        CanBurn = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(!CanBurn) { return; }

        if (collision.gameObject.tag == "Flammable")
        {
            Flammable flammable = collision.gameObject.GetComponent<Flammable>();
            if (!flammable.IsOn)
            {
                flammable.FlameOn();
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (!CanBurn) { return; }

        if (collision.gameObject.tag == "Flammable")
        {
            Flammable flammable = collision.gameObject.GetComponent<Flammable>();
            if (!flammable.IsOn)
            {
                flammable.FlameOn();
            }

        }
    }
}
