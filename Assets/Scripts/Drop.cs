using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    Animator myAnimator;
    Rigidbody2D myRigidbody2D;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.CompareTag("Flame"))
        //{
        //    FlameV2 flame = collision.GetComponent<FlameV2>();
        //    if (flame!= null)
        //    {
        //    }
        //}
        myAnimator.SetBool("Hit", true);
        myRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;

    }

    public void Kill()
    {
        myRigidbody2D.constraints = RigidbodyConstraints2D.None;
        myAnimator.SetBool("Hit", false);
        Destroy(this.gameObject);
    }
}
