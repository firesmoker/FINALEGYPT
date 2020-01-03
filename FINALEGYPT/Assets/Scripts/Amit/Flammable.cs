﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour
{

    [SerializeField] GameObject flamePrefab;
    [SerializeField] bool CanDie = false;
    Flammable myFlammable;
    [SerializeField] Vector3 offset;

    public bool IsOn = false;
    

    private void Start()
    {
        
    }

    public void FlameOn()
    {
        if (!IsOn)
        {
            IsOn = true;
            GameObject Flame = Instantiate(
                flamePrefab,
                transform.position + offset,
                Quaternion.identity, this.gameObject.transform);
            //Vector3 newSize = Flame.transform.localScale*2;
            Flame.transform.localScale = Flame.transform.localScale * 2;
            if (CanDie)
            {
                StartCoroutine(KillAfterTime(Flame));
            }
            
        }
        
    }

    IEnumerator KillAfterTime(GameObject flame)
    {
        IsOn = false;
        yield return new WaitForSeconds(3);
        Destroy(flame);
        //Destroy(gameObject);
    }
}
