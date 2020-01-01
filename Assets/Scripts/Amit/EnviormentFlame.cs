using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class EnviormentFlame : MonoBehaviour
{
    //BoxCollider2D myFlame;

    [SerializeField] bool CanBurn = false;
    bool isOnFire = true;

    [SerializeField] private GameObject _myLight;
    private Light2D _lightComponent;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _flickerSpeed = 0.1f;
    [SerializeField] private float _increaseSpeed = 0.1f;
    [SerializeField] private float _maxIntesity = 1.5f;
    [SerializeField] private float _intesityIncrement = 1.5f;
    //[SerializeField] private GameObject _myLight;
    //[SerializeField] private SpriteRenderer _spriteRenderer;

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
            StartCoroutine(IncreasingFlame(_lightComponent));
        else
            Debug.Log("light component is null");
    }

    IEnumerator IncreasingFlame(Light2D light)
    {
        bool finishedIncreasing = false;
        light.intensity = 0.1f;
        while (light.intensity < _maxIntesity)
        {
            yield return new WaitForSeconds(_increaseSpeed);
            light.intensity += _intesityIncrement;
        }
        if(light.intensity >= _maxIntesity)
            finishedIncreasing = true;
        while (finishedIncreasing)
        {
            yield return new WaitForSeconds(_flickerSpeed);
            light.intensity = Random.Range(1.35f, 1.55f);
        }

    }

    IEnumerator FlickerFlame(Light2D light)
    {
        while (true)
        {
            yield return new WaitForSeconds(_flickerSpeed);
            light.intensity = Random.Range(1.35f, 1.55f);
        }

    }

    IEnumerator FlameStart()
    {
        yield return new WaitForSeconds(0.2f);
        if (!isOnFire)
        {
            isOnFire = true;
            _myLight.SetActive(true);
            _spriteRenderer.enabled = true;
        }
    }

   // IEnumerator FlameStart()
   // {
   //     yield return new WaitForSeconds(0.2f);
   //     CanBurn = true;
   //     isOnFire = true;
   //     //_myLight.SetActive(true);
   //     //_spriteRenderer.enabled = true;
   // }

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
            if (this.tag == "Flame")
            {
                //GameManager.GameOver();
                Flammable parentFlammable = GetComponentInParent<Flammable>();
                if (parentFlammable != null)
                    parentFlammable.IsOn = false;
                Destroy(this.gameObject);
                //_myLight.SetActive(false);
                //_spriteRenderer.enabled = false;
                //isOnFire = false;
                //if (_myLight != null)
                //    _myLight.SetActive(false);
                //StartCoroutine(FlameStop());
                //Debug.Log("startedcortouine");

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
                Debug.Log("flammable is off");
                flammable.FlameOn();
            }
            else
            {
                Debug.Log("flammable is on");
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
