using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterdrop : MonoBehaviour
{
    [SerializeField] float dropTimer = 3f;
    [SerializeField] float tileOffset = 1.5f;
    [SerializeField] GameObject dropPrefab;

    public bool KeepDropping = true;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(dropWater());
    }

    IEnumerator dropWater()
    {
        while (KeepDropping == true)
        {
            
            GameObject waterDrop = Instantiate(
                dropPrefab,
                transform.position - new Vector3(0, tileOffset, 0),
                Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(dropTimer);
            GameObject.Destroy(waterDrop);
        }

    }
}
