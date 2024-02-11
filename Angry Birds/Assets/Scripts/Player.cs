using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D hook;
    [SerializeField] float releaseTime = 0.1f;
    [SerializeField] float maxDist = 2f;//distance between the hook and player
    [SerializeField] GameObject prevPlayer;
    [SerializeField] GameObject nextPlayer;
    [SerializeField] AudioClip releaseSFX;
    public PlayerTypes type;

    GameManager gm;
    PowerUP powerUp;
    Rigidbody2D rigidBody;
    SpringJoint2D springJoint;
    bool isPressed = false;

    //Adjust frequency of spring joint to increase range of throw

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        //gm.playerCount++;

    }
    void Start()
    {
        powerUp = GetComponent<PowerUP>();
        rigidBody = GetComponent<Rigidbody2D>();
        springJoint = GetComponent<SpringJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(mousePos, hook.transform.position) > maxDist)
            {
                rigidBody.position = hook.position + ((mousePos - hook.position).normalized * maxDist);
            }
            else
            {
                rigidBody.position = mousePos;
            }
        }
        
    }

    private void OnMouseDown()
    {
        isPressed = true;
        rigidBody.isKinematic = true;
    }
    private void OnMouseUp()
    {
        powerUp.isReleased = true;
        isPressed = false;
        rigidBody.freezeRotation = false; // resuming rotation around z after releasing the ball
        rigidBody.isKinematic = false;
        StartCoroutine("Release");
    }

    private IEnumerator Release()
    {
        AudioSource.PlayClipAtPoint(releaseSFX, transform.position);
        if(prevPlayer!= null)
        {
            Destroy(prevPlayer);
        }
        gm.ChangePlayersLeftText();
        yield return new WaitForSeconds(releaseTime);
        springJoint.enabled = false;
        this.enabled = false;//deactivating the script so you cannot mess with the ball after shooting
        yield return new WaitForSeconds(3f);
        if(nextPlayer != null)
        {
            nextPlayer.SetActive(true);
        }
        else if(nextPlayer == null && gm.enemies > 0)
        {
            gm.StartCoroutine("LoseSequence");
        }
    }
}
