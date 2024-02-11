using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP : MonoBehaviour
{
    [HideInInspector] public bool isAbilityUsed = false;
    [HideInInspector] public bool isReleased = false;
    PlayerTypes playerType;
    Rigidbody2D rb;
    private void Start()
    {
        playerType = GetComponent<Player>().type;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isAbilityUsed == false && isReleased == true)
        {
            isAbilityUsed = true;
            if(playerType == PlayerTypes.SPEED)
            {
                SpeedPowerUP();
            }
            else if(playerType == PlayerTypes.BOMB)
            {
                BombDropPowerUP();
            }
            else if(playerType == PlayerTypes.REVERSE)
            {
                ReversePowerUp();
            }
            else if(playerType == PlayerTypes.TRIPLESHOT)
            {
                TripleshotPowerUp();
            }
       
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            isAbilityUsed = true;//Avoiding using powerup after collision
        }
    }
    private void SpeedPowerUP()
    {
        rb.velocity *= 1.5f;
    }

    private void TripleshotPowerUp()
    {
        GameObject ball2 = Instantiate(gameObject, transform.position, transform.rotation, this.gameObject.transform);
        GameObject ball3 = Instantiate(ball2, transform.position, transform.rotation, ball2.transform);

        //Ball used to stick to the joint when power used immediately after clicking
        ball2.GetComponent<SpringJoint2D>().enabled = false;
        ball3.GetComponent<SpringJoint2D>().enabled = false;

        ball2.transform.position =transform.position - ( transform.up - transform.right);
        ball3.transform.position = ball2.transform.parent.position - (ball2.transform.parent.transform.up - (-ball2.transform.parent.transform.right));

        ball2.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
        ball3.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
    }

    private void BombDropPowerUP()
    {
        float bombDropFactor = 10f;
        rb.velocity += new Vector2(0f,bombDropFactor);
        GameObject bomb = Instantiate(gameObject, transform.position + new Vector3(0f,-0.5f,0f), transform.rotation,transform);
        bomb.GetComponent<SpringJoint2D>().enabled = false;
        bomb.GetComponent<Rigidbody2D>().velocity =new Vector2(0f, -10f);
    }

    private void ReversePowerUp()
    {
        float xVelo = rb.velocity.x;
        //rb.velocity =new Vector2( Mathf.Lerp(xVelo,-xVelo, 15), Mathf.Lerp(yVelo, -yVelo, 1f));
        rb.velocity = new Vector2(-xVelo,rb.velocity.y);
    }
}