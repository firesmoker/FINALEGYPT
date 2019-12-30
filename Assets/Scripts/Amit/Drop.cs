using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    Animator myAnimator;
    Rigidbody2D myRigidbody2D;
    public AudioClip dropSound;
    private AudioSource audio;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        myAnimator.SetBool("Hit", true);
        myRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;

    }

    public void Kill()
    {
        myRigidbody2D.constraints = RigidbodyConstraints2D.None;
        myAnimator.SetBool("Hit", false);
        Destroy(this.gameObject);
    }

    public void PlayDropSound()
    {
        audio.PlayOneShot(dropSound);
    }

}
