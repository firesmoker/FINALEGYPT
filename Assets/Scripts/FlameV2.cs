using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class FlameV2 : MonoBehaviour
{
    //BoxCollider2D myFlame;

    enum State{on, off}
    enum Type{playerTorch, wallTorch, other}
    [SerializeField] State state;
    [SerializeField] Type type;

    [SerializeField] private GameObject _myLight;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Light2D _lightComponent;
    [SerializeField] private float _flickerSpeed = 0.1f;
    Flammable myFlammable;
    [SerializeField] private float _increaseSpeed = 0.1f;
    [SerializeField] private float _maxIntesity = 1.5f;
    [SerializeField] private float _intesityIncrement = 1.5f;

    void Start()
    {
        myFlammable = GetComponentInParent<Flammable>();
        if (myFlammable == null)
            Debug.LogError("flammable parent is null");
        if (_myLight != null)
        {
            _lightComponent = _myLight.GetComponent<Light2D>();
            Debug.Log("getting light component");
        }
        //if (_lightComponent != null)
        //    StartCoroutine(FlickerFlame(_lightComponent));
        else
            Debug.Log("light component is null");
        //StartCoroutine(FlameStart());
        FlameStart();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != myFlammable)
        {
            Debug.Log("collision between flame and flammable");
            string collisionTag = collision.tag;
            if (collisionTag == "Quench" && state == State.on)
            {
                Debug.Log("quenched");
                if (type == Type.playerTorch)
                    GameManager.GameOver();
                //StartCoroutine(FlameStop());
                FlameStop();
            }

            if (collision.gameObject.CompareTag("Flammable") && state == State.on)
            {
                Debug.Log("touching flamable");
                Flammable flammable = collision.gameObject.GetComponent<Flammable>();
                if (!flammable.IsOn)
                {
                    flammable.FlameOn();
                }
            }
        }
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
        if (light.intensity >= _maxIntesity)
            finishedIncreasing = true;
        while (finishedIncreasing && state == State.on)
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
            light.intensity = Random.Range(1.35f, 1.44f);
        }

    }

    //public IEnumerator FlameStart()
    //{
    //    yield return new WaitForSeconds(0.2f);
    //    if (state != State.on)
    //    {
    //        state = State.on;
    //        _myLight.SetActive(true);
    //        _spriteRenderer.enabled = true;
    //        if (!myFlammable.IsOn)
    //            myFlammable.IsOn = true;
    //        StartCoroutine(IncreasingFlame(_lightComponent));
    //    }
    //}

    public void FlameStart()
    {
        if (state != State.on)
        {
            state = State.on;
            _myLight.SetActive(true);
            _spriteRenderer.enabled = true;
            if (!myFlammable.IsOn)
                myFlammable.IsOn = true;
            StartCoroutine(IncreasingFlame(_lightComponent));
        }
    }

   // IEnumerator FlameStop()
   // {
   //     yield return new WaitForSeconds(0.2f);
   //     if (state != State.off)
   //     {
   //         if (myFlammable.IsOn)
   //             myFlammable.IsOn = false;
   //         state = State.off;
   //         _myLight.SetActive(false);
   //         _spriteRenderer.enabled = false;
   //         
   //     }
   // }

    public void FlameStop()
    {
        if (state != State.off)
        {
            if (myFlammable.IsOn)
                myFlammable.IsOn = false;
            state = State.off;
            _myLight.SetActive(false);
            _spriteRenderer.enabled = false;
        }
    }


}
