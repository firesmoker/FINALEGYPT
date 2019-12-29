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

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        myAnimator.SetBool("Hit", true);
        myRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;

    }

    void Kill()
    {
        myRigidbody2D.constraints = RigidbodyConstraints2D.None;
        Destroy(this.gameObject);
    }

}
