using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public readonly int maxItems; 
    public GameBehavior gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(transform.parent.gameObject);
            Debug.Log("Item collected!");
        }
        gameManager.Items += 1;
        gameManager.PrintLootReport();
    }
}