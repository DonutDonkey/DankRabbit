using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    private Enemy parentScript;

    private void Start()
    {
        parentScript = this.transform.parent.GetComponent<Enemy>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("EnemyKillboxCollision");
            parentScript.KillThisObject();
        }
    }
    
}
