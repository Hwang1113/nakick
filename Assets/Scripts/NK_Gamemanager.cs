using UnityEngine;

public class NK_Gamemanager : MonoBehaviour
{
    [SerializeField]
    private GameObject NK_Trash = null;
    
    [SerializeField]
    private NK_Player[] players;

    private void Start()
    {
        // ��� �÷��̾� �ʱ�ȭ
        //players = FindObjectsOfType<Player>();
    }

    private void Update()
    {
       
    }
}
