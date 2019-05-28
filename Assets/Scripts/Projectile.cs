using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Z))
        // Lanuch(Vector2.up,4f);
        if (transform.position.magnitude > 1000f)
            Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Projectile Collision with" + other.gameObject);
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null)
            e.Fix();
        Destroy(gameObject);
    }
    public void Lanuch(Vector2 direction, float force)
    {
        rigidbody2D.AddForce(direction * force);
    }

}
