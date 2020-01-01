using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviormentFlame : MonoBehaviour
{
    //BoxCollider2D myFlame;

    [SerializeField] bool CanBurn = false;
    bool isOnFire = true;
    //[SerializeField] private GameObject _myLight;
    //[SerializeField] private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //myFlame = GetComponent<BoxCollider2D>();
        StartCoroutine(FlameStart());
    }

   
    IEnumerator FlameStart()
    {
        yield return new WaitForSeconds(0.2f);
        CanBurn = true;
        isOnFire = true;
        //_myLight.SetActive(true);
        //_spriteRenderer.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Quench") && isOnFire)
        {
            Debug.Log("quenched");
            if (gameObject.transform.root != transform)
            {
                //GameManager.GameOver();
                //Destroy(this.gameObject);
                //_myLight.SetActive(false);
                //_spriteRenderer.enabled = false;
                isOnFire = false;

            }
            else
            {
                Debug.Log("Not torch");
            }
        }


        if(!CanBurn) { StartCoroutine(FlameStart()); }

        if (collision.gameObject.CompareTag("Flammable"))
        {
            Debug.Log("touching flamable");
            Flammable flammable = collision.gameObject.GetComponent<Flammable>();
            if (!flammable.IsOn)
            {
                flammable.FlameOn();
            }
            else
            {
                StartCoroutine(FlameStart());
            }

        }
    }

    IEnumerator GameOverRoutine()
    {
        //GameManager gameManager = FindObjectOfType<GameManager>();
        //gameManager.GameOver();
        yield return new WaitForSeconds(1);
        GameManager.GameOver();
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
