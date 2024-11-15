using UnityEngine;
using Photon.Pun;
using System.Runtime.InteropServices;

public class Player : MonoBehaviourPun
{
    public string playerName = "Player";
    public int score = 0;
    private void Update()
    {
        if (!photonView.IsMine) return;


    }

    // Ʈ���� �浹�� �߻����� �� ȣ��Ǵ� �Լ�
    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� "Trash" �±׸� �������� Ȯ��
        if (other.CompareTag("Trash"))
        {
            // Trash ������Ʈ���� PlayerClick �޼��带 ȣ���Ͽ� Ŭ���� ����
            NK_Trash trash = other.GetComponent<NK_Trash>();
            if (trash != null)
            {
                trash.PlayerTouch(this); // Player�� Ŭ���� ����
            }
        }
    }

    // �÷��̾��� ���ھ� ����
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log($"{playerName} Score: {score}");
    }
    [PunRPC]
    public void ApplyIntScore(int _score) 
    {
        score = _score;
    }
}