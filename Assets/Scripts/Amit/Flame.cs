using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Flame : MonoBehaviour
{
    //BoxCollider2D myFlame;

    [SerializeField] bool CanBurn = false;
    bool isOnFire = true;
    [SerializeField] private GameObject _myLight;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Light2D _lightComponent;
    [SerializeField] private float _flickerSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        //myFlame = GetComponent<BoxCollider2D>();
        CanBurn = true;
        StartCoroutine(FlameStart());
        if (_myLight != null)
        {
            _lightComponent = _myLight.GetComponent<Light2D>();
            Debug.Log("getting light component");
        }

        if (_lightComponent != null)
            StartCoroutine(FlickerFlame(_lightComponent));
        else
            Debug.Log("light component is null");
    }

    IEnumerator FlickerFlame(Light2D light)
    {
        while(true)
        {
            yield return new WaitForSeconds(_flickerSpeed);
            light.intensity = Random.Range(1.35f, 1.44f);
        }
        
    }
   
    IEnumerator FlameStart()
    {
        yield return new WaitForSeconds(0.2f);
        if(!isOnFire)
        {
            isOnFire = true;
            _myLight.SetActive(true);
            _spriteRenderer.enabled = true;
        }
    }

    IEnumerator FlameStop()
    {
        yield return new WaitForSeconds(0.2f);
        if (isOnFire)
        {
            isOnFire = false;
            _myLight.SetActive(false);
            _spriteRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Quench") && isOnFire)
        {
            Debug.Log("quenched");
            if (gameObject.transform.root != transform)
            {

                //Destroy(this.gameObject);
                //_myLight.SetActive(false);
                //_spriteRenderer.enabled = false;
                //isOnFire = false;
                //GameManager.GameOver();
                StartCoroutine(FlameStop());

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
