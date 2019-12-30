using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    BoxCollider2D myFlame;

    [SerializeField] bool CanBurn = false;
    bool IsOnFire = true;

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
        IsOnFire = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Quench") && IsOnFire)
        {
            if (gameObject.transform.root != transform)
            {
                StartCoroutine(GameOver());
            }
            else
            {
                Debug.Log("Not torch");
            }
            Destroy(this.gameObject);
        }


        if(!CanBurn) { StartCoroutine(FlameStart()); }

        if (collision.gameObject.CompareTag("Flammable"))
        {
            Flammable flammable = collision.gameObject.GetComponent<Flammable>();
            if (!flammable.IsOn)
            {
                flammable.FlameOn();
            }

        }
    }

    IEnumerator GameOver()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        gameSession.GameOver();
        yield return new WaitForSeconds(1);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (!CanBurn) { StartCoroutine(FlameStart()); }

        if (collision.gameObject.CompareTag("Flammable"))
        {
            Flammable flammable = collision.gameObject.GetComponent<Flammable>();
            if (!flammable.IsOn)
            {
                flammable.FlameOn();
            }

        }
    }
}
