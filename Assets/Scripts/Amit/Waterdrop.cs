using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterdrop : MonoBehaviour
{
    [SerializeField] float dropTimer = 3f;
    //[SerializeField] float tileOffset = 5f;
    [SerializeField] GameObject dropPrefab;
    public AudioClip dropSound;
    private AudioSource audio;

    public bool KeepDropping = true;

    // Update is called once per frame
    void Start()
    {
        audio = GetComponent<AudioSource>();
        StartCoroutine(dropWater());
    }

    IEnumerator dropWater()
    {
        while (KeepDropping == true)
        {
            PlayDropSound();
            GameObject waterDrop = Instantiate(
                dropPrefab,
                transform.position + new Vector3(0, -1.3f, 0),
                Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(dropTimer);
            GameObject.Destroy(waterDrop);
        }

    }
    public void PlayDropSound()
    {
        audio.PlayOneShot(dropSound);
    }
}
