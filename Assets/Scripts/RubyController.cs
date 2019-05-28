using UnityEngine;

public class RubyController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float speed = 3.0f;
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;
    public int Health { get { return currentHealth; } }
    int currentHealth;
    bool isInvincible;
    float invincibleTimer;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    Rigidbody2D rigidbody2d;
    AudioSource audioSource;
    public AudioClip throwCog;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        currentHealth = maxHealth;
        // QualitySettings.vSyncCount=0;
        // Application.targetFrameRate=60;
        audioSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertiacl = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertiacl);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        Vector2 position = rigidbody2d.position;

        position = position + move * speed * Time.deltaTime;

        rigidbody2d.MovePosition(position);
        // Vector2 position = rigidbody2d.position;
        // position.x = position.x + speed * horizontal * Time.deltaTime;
        // position.y = position.y + speed * vertiacl * Time.deltaTime;
        // rigidbody2d.MovePosition(position);
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        // transform.position=position;
        if(Input.GetKeyDown(KeyCode.C))
        {
            Lanuch();
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit =Physics2D.Raycast(rigidbody2d.position+Vector2.up*0.2f,lookDirection,1.5f,LayerMask.GetMask("NPC"));
            if(hit.collider!=null)
            {
                NonPlayerCharacter NPC=hit.collider.GetComponent<NonPlayerCharacter>();
                if(NPC!=null)
                {
                    NPC.DisplayDialog();
                }
            }
        }
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if (isInvincible)
                return;
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetVlaue(currentHealth / (float)maxHealth);
    }
    void Lanuch()
    {
        GameObject projectileobject=Instantiate(projectilePrefab,rigidbody2d.position+Vector2.up*0.5f,Quaternion.identity);
        Projectile projectile=projectilePrefab.GetComponent<Projectile>();
        projectile.Lanuch(lookDirection,300f);
        animator.SetTrigger("Lanuch");
        PlaySound(throwCog);
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
