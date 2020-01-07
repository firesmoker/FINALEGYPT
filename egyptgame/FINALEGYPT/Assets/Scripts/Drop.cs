using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    Animator myAnimator;
    Rigidbody2D myRigidbody2D;
    private FMODUnity.StudioEventEmitter _eventEmitter;

    private void Start()
    {
        _eventEmitter = GetComponent<FMODUnity.StudioEventEmitter>();
        myAnimator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        myAnimator.SetBool("Hit", true);
        _eventEmitter.Play();
        //FMODUnity.RuntimeManager.PlayOneShot();
        myRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;

    }

    public void Kill()
    {
        myRigidbody2D.constraints = RigidbodyConstraints2D.None;
        myAnimator.SetBool("Hit", false);
        Destroy(this.gameObject);
    }
}
