
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviourPun
{
    public int score = 0;

    [SerializeField] private Color[] colors = null;


    private void Start()
    {

        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;

        // 색상 동기화 (자기 자신이 맡은 색상을 다른 플레이어에게 전달)
        if (photonView.IsMine) //본인만 Ture
        {
            // 자기 자신이 가진 색상의 인덱스를 다른 모든 클라이언트에게 전달
            int colorIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1; // 0부터 시작하는 배열 인덱스
            photonView.RPC("SyncPlayerColor", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.ActorNumber, colorIndex);
        }
    }

    // 다른 플레이어에게 색상을 동기화하는 RPC
    [PunRPC]
    public void SyncPlayerColor(int actorNumber, int colorIndex)
    {
        // RPC를 통해 전달된 색상 인덱스를 이용해 해당 플레이어의 색상을 업데이트
        if (photonView.Owner.ActorNumber == actorNumber)
        {
            if (colorIndex >= 0 && colorIndex < colors.Length)
            {
                this.GetComponent<MeshRenderer>().material.color = colors[colorIndex];
            }
        }
    }

    private void Update()
    {
        if (!photonView.IsMine) return;

    }

    public void SetMaterial(int _playerNum)
    {
        Debug.LogError(_playerNum + " : " + colors.Length);
        if (_playerNum > colors.Length) return;

        this.GetComponent<MeshRenderer>().material.color = colors[_playerNum - 1];
    }



    [PunRPC]
    public void ApplyHp()
    {
        Debug.LogErrorFormat("{0} Hp", PhotonNetwork.NickName);

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
        //Debug.Log($"{playerName} Score: {score}");
    }

}
