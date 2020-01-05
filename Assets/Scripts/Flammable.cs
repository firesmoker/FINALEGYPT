using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour
{

    [SerializeField] GameObject flamePrefab;
    [SerializeField] bool CanDie = false;
    bool flameCreated = false;
    [SerializeField] Vector3 offset;
    public bool IsOn = false;
    GameObject flame;
    public bool flameOnGameStart = false;
    SpriteRenderer _spriteRenderer;
    [SerializeField] float _fadeRate = 0.1f;
    [SerializeField] private bool _oneTimeUse = false;
    [SerializeField] float _flameScaleOffset = 2;
    bool _isFading = false;
    public bool isFading
    {
        get
        {
            return _isFading;
        }
    }
    public bool oneTimeUse
    {
        get
        {
            return _oneTimeUse;
        }
    }



    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (flameOnGameStart)
            FlameOn();
;    }

    public void FlameOn()
    {
        if (!IsOn && !flameCreated)
        {
            IsOn = true;
            flameCreated = true;
            flame = Instantiate(
                flamePrefab,
                transform.position + offset,
                Quaternion.identity, this.gameObject.transform);
            flame.transform.localScale = flame.transform.localScale * _flameScaleOffset;
            if (CanDie)
            {
                StartCoroutine(KillAfterTime(flame));
            }
            
        }
        else if (!IsOn && flameCreated)
        {
            if (flame != null)
            {
                FlameV2 flameComponenet = GetComponentInChildren<FlameV2>();
                flameComponenet.FlameStart();
            }
            else
                Debug.Log("myflame isnull");
        }
        
    }


    public void StartFadeToDeath()
    {
        StartCoroutine(FadeToDeath());
    }

    public IEnumerator FadeToDeath()
    {
        if(_spriteRenderer != null)
        {
            _isFading = true;
            Debug.Log("fadetodeath good" + transform.name);
            Color color = _spriteRenderer.color;
            while (color.a > 0)
            {
                yield return new WaitForSeconds(_fadeRate);
                color.a -= 0.1f;
                _spriteRenderer.color = color;
            }
            Debug.Log("Destroying" + transform.name);
            Destroy(gameObject);
        }
        else
        {
            _isFading = true;
            Debug.Log("fadetodeath NULL SPRITERENDERER" + transform.name);
            //float i = 1;
            //Debug.Log("entering while" + transform.name);
            yield return new WaitForSeconds(_fadeRate*10);
            //Debug.Log(transform.name + " i " + i);
            Debug.Log("Destroying" + transform.name);
            Destroy(gameObject);
        }

    }

    IEnumerator KillAfterTime(GameObject flame)
    {
        IsOn = false;
        yield return new WaitForSeconds(3);
        Destroy(flame);
    }
}
