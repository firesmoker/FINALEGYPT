﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlatformBody" && other != null)
        {
            Debug.Log("ouch");
            GetComponentInParent<Player>().ResetY();
        }
    }
}
