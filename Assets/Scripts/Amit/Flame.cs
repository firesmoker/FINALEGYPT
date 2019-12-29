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
        yield return new WaitForSeconds(2);
        CanBurn = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!CanBurn) { return; }

        Debug.Log("flame?");

        if (collision.gameObject.tag == "Flammable")
        {
            Debug.Log("Flame On");
            collision.gameObject.GetComponent<Flammable>().FlameOn();
        }
    }
}
