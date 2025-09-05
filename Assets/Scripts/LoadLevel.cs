using UnityEngine;

public class LoadLevel : MonoBehaviour
{

    public string levelToLoad;


    public bool cleared;

    void Start()
    {
        if (GameManager.manager.GetType().GetField(levelToLoad).GetValue(GameManager.manager).ToString() == "True")
        {
            Cleared(true);
        }
    }

    void Update()
    {


    }

    public void Cleared(bool isClear)
    {
        if (isClear == true)
        {
            GameManager.manager.GetType().GetField(levelToLoad).SetValue(GameManager.manager, true);
            transform.GetChild(1).gameObject.GetComponent<Renderer>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }

}
