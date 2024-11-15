using UnityEngine;

public class NK_Gamemanager : MonoBehaviour
{
    [SerializeField]
    private GameObject NK_Trash = null;
    
    [SerializeField]
    private Player[] players;

    private void Start()
    {
        // 모든 플레이어 초기화
        //players = FindObjectsOfType<Player>();
    }

    private void Update()
    {
       
    }
}
