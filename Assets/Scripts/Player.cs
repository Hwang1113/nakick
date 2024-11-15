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

    // 트리거 충돌이 발생했을 때 호출되는 함수
    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 "Trash" 태그를 가졌는지 확인
        if (other.CompareTag("Trash"))
        {
            // Trash 오브젝트에서 PlayerClick 메서드를 호출하여 클릭을 전달
            NK_Trash trash = other.GetComponent<NK_Trash>();
            if (trash != null)
            {
                trash.PlayerTouch(this); // Player가 클릭을 전달
            }
        }
    }

    // 플레이어의 스코어 증가
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