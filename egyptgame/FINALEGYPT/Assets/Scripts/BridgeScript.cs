using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour
{

    Rigidbody2D myRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Burned");
        if(collision.gameObject.layer == LayerMask.GetMask("Rope"))
        {
            myRigidBody.constraints = RigidbodyConstraints2D.None;
        }
    }
}
