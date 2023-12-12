using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using TMPro;
public class move : MonoBehaviour
{
    private TextMeshProUGUI texti;

    private TextMeshProUGUI texti2;

    public float speed = 3.0f;

    public int maxHealth = 5;

    public GameObject projectilePrefab;

    public AudioClip throwSound;
    public AudioClip hitSound;

    public int health { get { return currentHealth; } }
    int currentHealth;

    public float timeInvincible = 0.3f;
    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    AudioSource audioSource;

    public float jumpPower = 1f;

    private BoxCollider2D coll;

    [SerializeField] private LayerMask canJump;






    void Start()
    {
        texti = GameObject.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        texti2 = GameObject.Find("Text (TMP)2").GetComponent<TextMeshProUGUI>();

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();

        coll = GetComponent<BoxCollider2D>();

        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }


    void Update()
    {


        // Líf og stig
        texti.text = "Stig " + rememberMe.stig;

        texti2.text = "Líf " + currentHealth;


        // hreifir carecter
        horizontal = Input.GetAxis("Horizontal");
        rigidbody2d.velocity = new Vector2(horizontal * speed, rigidbody2d.velocity.y);
        // hopar 
        if (Input.GetKeyDown(KeyCode.Space) && InGround())
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpPower);
        }





        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        // anemaitar laba í át
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {

            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }



        // árás
        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {

            if (isInvincible)
                return;
            // gerir þig isInvincible og animatar Hit og gerir hlóð
            isInvincible = true;
            invincibleTimer = timeInvincible;
            animator.SetTrigger("Hit");
            PlaySound(hitSound);
            if (currentHealth == 0) { SceneManager.LoadScene(2); }
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);

    }
    void Launch()
    {
        // Launcar Projectile
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 500);
        // animation
        animator.SetTrigger("Launch");
        // hlóð
        PlaySound(throwSound);
    }
    private bool InGround()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 1f, canJump);
    }

}
    

