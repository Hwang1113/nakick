using UnityEngine;

public class TrashManager : MonoBehaviour
{
    public GameObject Item;

    private void Start()
    {
        Invoke("Spawnitem", 30);
    }

    private void Spawnitem()
    {
        float randomX = Random.Range(-23f, 19f);
        float randomZ = Random.Range(-23f, 19f);
        if(true)
        {
            GameObject item = (GameObject)Instantiate(Item, new Vector3(randomX, 0f, randomZ), Quaternion.identity);
        }
    }
}
