using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private new Rigidbody2D rigidbody = null;

    public float bulletSpeed = 20.0f;

    private float startPosX;
    
    private void OnEnable()
    {
        startPosX = transform.position.x;

        Debug.Log(name + " ENABLED");

        rigidbody.velocity = transform.right * bulletSpeed;
    }

    private void OnDisable()
    {
        Debug.Log(name + " DISABLED");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name != "Player") {
            Debug.Log("Collision with");
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if( CheckIfOutOfRange() )
        {

            gameObject.SetActive(false);
        }
    }

    private bool CheckIfOutOfRange()
    {
        if( RangeChecker() )
            return true;
        else
            return false;
    }

    private bool RangeChecker()
    {
        if (transform.position.x < startPosX + 5.0f && transform.position.x > startPosX - 5.0f)
            return false;

        return true;
    }

}
