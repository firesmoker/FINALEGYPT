using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class childrenCheck : MonoBehaviour
{
    [SerializeField] private bool destroyIfEmpty = true;
    // Update is called once per frame
    void Update()
    {
        if (transform.childCount <= 0 && destroyIfEmpty)
            Destroy(gameObject);
    }
}
