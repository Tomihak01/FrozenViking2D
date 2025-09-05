using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCharacter : MonoBehaviour
{
    [SerializeField]
    public float speed;



    void Start()
    {
        if (GameManager.manager.currentLevel != "")
        {


            transform.position = GameObject.Find(GameManager.manager.currentLevel).transform.GetChild(0).transform.position;
            GameObject.Find(GameManager.manager.currentLevel).GetComponent<LoadLevel>().Cleared(true);
        }
    }


    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float verticalMove = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(horizontalMove, verticalMove, 0);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LevelTrigger"))
        {
            GameManager.manager.currentLevel = collision.gameObject.name;
            SceneManager.LoadScene(collision.GetComponent<LoadLevel>().levelToLoad);
        }
    }
}
//LOL. Jan was here! :)
