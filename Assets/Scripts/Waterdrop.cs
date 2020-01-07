using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterdrop : MonoBehaviour
{
    [SerializeField] float dropTimer = 3f;
    //[SerializeField] float tileOffset = 5f;
    [SerializeField] GameObject dropPrefab;
    public AudioClip dropSound;
    public string dropSoundFmod;
    private AudioSource audioSource;

    public bool KeepDropping = true;

    // Update is called once per frame
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(dropWater());
    }

    IEnumerator dropWater()
    {
        yield return new WaitForSeconds(1f);
        while (KeepDropping == true)
        {
            PlayDropSound();
            GameObject waterDrop = Instantiate(
                dropPrefab,
                transform.position + new Vector3(0, -1.3f, 0),
                Quaternion.identity, this.gameObject.transform);
            yield return new WaitForSeconds(dropTimer);
            GameObject.Destroy(waterDrop);
        }

    }
    public void PlayDropSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(dropSoundFmod);
        //audioSource.PlayOneShot(dropSound);
    }
}
