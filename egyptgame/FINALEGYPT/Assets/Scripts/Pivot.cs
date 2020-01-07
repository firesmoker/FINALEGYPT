using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivot : MonoBehaviour
{
    public Player2D myPlayer;
    public Vector3 difference;
    public float rotationZ;

    private void Start()
    {
        myPlayer = FindObjectOfType<Player2D>();
    }

    private void FixedUpdate()
    {
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        difference.Normalize();

        if (difference.x < 0.1 && difference.x > -0.1) // Check if mouse is very close to player
        {
                return; // If it is, skip the pivoting and wait for the mouse to be a little further away from the player
        }

        rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        if (rotationZ < -90 || rotationZ > 90)
        {
            if (myPlayer.transform.eulerAngles.y == 0)
            {
                transform.localRotation = Quaternion.Euler(180, 0, -rotationZ);
            }
            else if (myPlayer.transform.eulerAngles.y == 180)
            {
                transform.localRotation = Quaternion.Euler(180, 180, -rotationZ);
            }

            myPlayer.FlipSprite(true);
            transform.localScale = new Vector2(-1f, 1f);
        }
        else if (rotationZ != 0)
        {
            myPlayer.FlipSprite(false);
            transform.localScale = new Vector2(1f, 1f);
        }
    }
}
