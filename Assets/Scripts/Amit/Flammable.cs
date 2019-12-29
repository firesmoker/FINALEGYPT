using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour
{

    [SerializeField] GameObject flamePrefab;

    bool IsOn = false;
    bool CanDie = false;

    public void FlameOn()
    {
        if (!IsOn)
        {
            IsOn = true;
            GameObject Flame = Instantiate(flamePrefab, transform.position, Quaternion.identity) as GameObject;
        }
        
    }
}
