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
    public bool burnsOthers = false;

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
        else
            Debug.Log("light component is null");
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
                FlameStop();
            }

            if (collision.gameObject.CompareTag("Flammable") && state == State.on && burnsOthers)
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (burnsOthers && collision.CompareTag("Flammable"))
        {
            Debug.Log("touching flamable");
            Flammable flammable = collision.gameObject.GetComponent<Flammable>();
            if (!flammable.IsOn)
            {
                flammable.FlameOn();
            }
        }

    }

    IEnumerator IncreasingFlame(Light2D light)
    {
        Color color = _spriteRenderer.color;
        color.a = 0f;
        _spriteRenderer.color = color;
        bool finishedIncreasing = false;
        light.intensity = 0.1f;
        while (light.intensity < _maxIntesity)
        {
            yield return new WaitForSeconds(_increaseSpeed);
            light.intensity += _intesityIncrement;
            color.a += 0.05f;
            _spriteRenderer.color = color;
        }
        if (light.intensity >= _maxIntesity)
        {
            finishedIncreasing = true;
            color.a = 1;
            _spriteRenderer.color = color;
        }
            
        while (finishedIncreasing && state == State.on)
        {
            burnsOthers = true;
            yield return new WaitForSeconds(_flickerSpeed);
            light.intensity = Random.Range(1.35f, 1.55f);
        }

    }

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

    public void FlameStop()
    {
        if (state != State.off)
        {
            burnsOthers = false;
            if (myFlammable.IsOn)
                myFlammable.IsOn = false;
            state = State.off;
            _myLight.SetActive(false);
            _spriteRenderer.enabled = false;
        }
    }


}
