using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CharacterControl : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public Animator animator;
    public Rigidbody2D rb2D;
    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;
    public bool grounded;

    public bool hasAxe;


    public GameObject boneFire;
    public float woodAmount;
    public TextMeshProUGUI woodAmountText;

    public Image filler;
    public float counter;
    public float maxCounter;

    void Start()
    {

        //animator = GetComponent<Animator>();
        //rb2D = GetComponent<Rigidbody2D>();
        GameManager.manager.historyHealth = GameManager.manager.health;
        GameManager.manager.historyMaxHealth = GameManager.manager.maxHealth;
        GameManager.manager.historypreviousHealth = GameManager.manager.previousHealth;
    }

    void Update()
    {


        if (Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundCheckLayer))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            if (woodAmount >= 3)
            {
                woodAmount -= 3;
                woodAmountText.text = woodAmount.ToString();
                Instantiate(boneFire, transform.position + new Vector3(3.1f * transform.localScale.x, -0.5f, 0), Quaternion.identity);
            }

        }



        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
        if (Input.GetButtonDown("Jump") && grounded == true)
        {
            rb2D.linearVelocity = new Vector2(0, jumpForce);
            animator.SetTrigger("Jump");
        }


        if (counter > maxCounter)
        {
            GameManager.manager.previousHealth = GameManager.manager.health;
            counter = 0;
        }
        else
        {
            counter += Time.deltaTime;
        }


        filler.fillAmount = Mathf.Lerp(GameManager.manager.previousHealth / GameManager.manager.maxHealth, GameManager.manager.health / GameManager.manager.maxHealth, counter / maxCounter);




        if (gameObject.transform.position.y < -10)
        {
            //GameManager.manager.currentLevel = GameManager.manager.previousLevel;
            //SceneManager.LoadScene("Map");
            Die();
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage(20);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AddHealth"))
        {
            Destroy(collision.gameObject);
            heal(60);
        }

        if (collision.CompareTag("AddMaxHealth"))
        {
            Destroy(collision.gameObject);
            AddMaxHealth(40);
        }

        if (collision.CompareTag("LevelEnd"))
        {
            if (hasAxe == true)
            {
                GameManager.manager.previousLevel = GameManager.manager.currentLevel;
                SceneManager.LoadScene("Map");
            }

        }
        if (collision.CompareTag("wood"))
        {
            Destroy(collision.gameObject);
            woodAmount++;
            woodAmountText.text = woodAmount.ToString();
        }
        if (collision.CompareTag("Axe"))
        {
            Destroy(collision.gameObject);
            hasAxe = true;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Bonefire"))
        {
            heal(Time.deltaTime * 10);
        }

    }


    void AddMaxHealth(float addMaxHealthAmount)
    {
        GameManager.manager.maxHealth += addMaxHealthAmount;
    }

    void heal(float healamount)
    {
        GameManager.manager.previousHealth = filler.fillAmount * GameManager.manager.maxHealth;
        counter = 0;
        GameManager.manager.health += healamount;
        if (GameManager.manager.health > GameManager.manager.maxHealth)
        {
            GameManager.manager.health = GameManager.manager.maxHealth;
        }

        //health = Mathf.Clamp(health, 0, maxHealth);

    }


    void TakeDamage(float dmg)
    {
        GameManager.manager.previousHealth = filler.fillAmount * GameManager.manager.maxHealth;
        counter = 0;
        GameManager.manager.health -= dmg;

        if (GameManager.manager.health < 0)
        {
            Die();
        }

    }

    public void Die()
    {
        GameManager.manager.currentLevel = GameManager.manager.previousLevel;
        GameManager.manager.health = GameManager.manager.historyHealth;
        GameManager.manager.maxHealth = GameManager.manager.historyMaxHealth;
        GameManager.manager.previousHealth = GameManager.manager.historypreviousHealth;
        SceneManager.LoadScene("Map");
    }



}
