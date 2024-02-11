using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] AudioClip destroyedSFX;
    [SerializeField] ParticleSystem destroyedVFX;
    GameManager gm;
    // Start is called before the first frame update

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        gm.AddEnemy();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > 2)
        {
            Die();
        }
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(destroyedSFX, transform.position, 1f);
        ParticleSystem VFX = Instantiate(destroyedVFX, transform.position, transform.rotation);
        Destroy(VFX.gameObject, 2f);
        gm.SubtractEnemy();
        Destroy(gameObject);
    }
    }
