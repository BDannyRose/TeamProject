using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool Damage;
    public float slow;
    public LayerMask dangerLayer;
    private Rigidbody2D rb;

    public void Start()
    {
        rb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Damage = GameObject.FindWithTag("Player").GetComponent<Collider2D>().IsTouchingLayers(dangerLayer.value);
        if (Damage)
        {
            Die(gameObject);
            Vector3 mov = new Vector3(-1.0f, 0.0f, 0.0f);
            if (rb.velocity.magnitude > 30) {
                rb.AddForce(mov * slow);
            }
        }
    }

    private void MovementLogic()
    {
        
    }

    private void Die(GameObject obj) 
    {
        Destroy(obj);
    }
}
