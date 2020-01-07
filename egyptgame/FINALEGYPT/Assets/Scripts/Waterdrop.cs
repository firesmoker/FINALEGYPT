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
    private FMODUnity.StudioEventEmitter _eventEmitter;
    public bool KeepDropping = true;
    [SerializeField] float dropStartDelay = 0.5f;

    // Update is called once per frame
    void Start()
    {
        _eventEmitter = GetComponent<FMODUnity.StudioEventEmitter>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(dropWater());
    }

    IEnumerator dropWater()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(dropStartDelay);
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
        _eventEmitter.Play();
        //audioSource.PlayOneShot(dropSound);
    }
}
