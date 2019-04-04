using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float health = 0.0f;

    public AudioClip hurt;

    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfDead();
    }

    private void CheckIfDead()
    {
        if(health > 0.0) {
            return;
        } else {
            KillThisObject();
        }
    }

    public void KillThisObject()
    {
        AudioSource.PlayClipAtPoint(hurt, transform.position);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bullet")
        {
            AudioSource.PlayClipAtPoint(hurt, transform.position);

            Hurt();
        }
    }

    private void Hurt()
    {
        health -= 1.0f;
    }

    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    //if(other.gameObject.CompareTag("PlayChar"))
    //    //{
    //    //    //Player test = other.gameObject.GetComponent<Player>();
    //    //    //Destroy(test.gameObject); //Shits broken but good to know
    //    //}
    //}
}
