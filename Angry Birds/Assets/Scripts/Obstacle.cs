using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float health = 50;
    [SerializeField] AudioClip destroyedSFX;
    [SerializeField] ParticleSystem destroyedVFX;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if player is hitting the obstacle the obstacle then damage = speed * mass
        //if other objects are hitting the damage = relative velocity

        if(collision.gameObject.tag =="Player" && collision.relativeVelocity.magnitude * collision.rigidbody.mass > health)
        {
            FX();
            Destroy(gameObject);

        }
        else if(collision.gameObject.tag == "Player" && collision.relativeVelocity.magnitude * collision.rigidbody.mass < health)
        {
            health -= collision.relativeVelocity.magnitude * collision.rigidbody.mass;
            
        }
        else if(collision.relativeVelocity.magnitude > health)
        {
            FX();
            Destroy(gameObject);
        }
        else
        {
            health -= collision.relativeVelocity.magnitude;
        }
            
    }

    private void FX()
    {
        AudioSource.PlayClipAtPoint(destroyedSFX, transform.position);
        ParticleSystem VFX = Instantiate(destroyedVFX, transform.position, transform.rotation);
        VFX.Play();
        Destroy(VFX, 2f);
    }
}
