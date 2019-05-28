using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ParticleSystem smokeEffect;
    bool isBroken = true;
    public float speed = 3.0f;
    public bool isVertical;
    public float changeTime = 3.0f;
    Rigidbody2D rigidbody2D;
    Animator animator;
    float timer;
    int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        smokeEffect.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBroken)
            return;
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            // Debug.Log(direction);
            timer = changeTime;
        }
        Vector2 position = rigidbody2D.position;
        if (isVertical)
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
            position.x = position.x + Time.deltaTime * speed * direction;
        }
        rigidbody2D.MovePosition(position);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        isBroken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
        Destroy(smokeEffect.gameObject);
    }
}
