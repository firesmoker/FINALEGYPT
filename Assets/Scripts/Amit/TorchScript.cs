using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchScript : MonoBehaviour
{
    [SerializeField] GameObject flame;
    [SerializeField] GameObject newFlame;

    [SerializeField] bool HasFlame = false;

    // Start is called before the first frame update
    void Start()
    {
        SetTorchOnFire();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Flammable") && HasFlame)
        {
            collision.gameObject.GetComponent<Flammable>().FlameOn();
        }
        if(collision.gameObject.CompareTag("Flame") && !HasFlame)
        {
            SetTorchOnFire();
        }
    }

    private void SetTorchOnFire()
    {
        newFlame = Instantiate(
            flame,
            transform.position + new Vector3(-0.1f, 0.3f, 0),
            Quaternion.identity) as GameObject;
        newFlame.transform.parent = transform;

        HasFlame = true;
    }

    private void QuenchTorch()
    {
        HasFlame = false;
    }
}
