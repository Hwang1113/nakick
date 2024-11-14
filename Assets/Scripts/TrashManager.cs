using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Trash = null;
    [SerializeField]
    private List<GameObject> TrashList = null;


    private void Awake()
    {
        TrashList = new List<GameObject>();

    }

    private void Start()
    {
        CreateItem();
    }

    private void CreateItem()
    {
        for (int i = 0; i <30; ++i)
        {
            Vector3 randomT = new Vector3(
                Random.Range(-12, 12),
                0.08f,
                Random.Range(-12, 12)
                );

            GameObject hGo = Instantiate(Trash, randomT, Quaternion.identity);

            TrashList.Add(hGo);
        }


    }
}
