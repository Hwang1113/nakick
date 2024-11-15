
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

        // ���� ����ȭ (�ڱ� �ڽ��� ���� ������ �ٸ� �÷��̾�� ����)
        if (photonView.IsMine) //���θ� Ture
        {
            // �ڱ� �ڽ��� ���� ������ �ε����� �ٸ� ��� Ŭ���̾�Ʈ���� ����
            int colorIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1; // 0���� �����ϴ� �迭 �ε���
            photonView.RPC("SyncPlayerColor", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.ActorNumber, colorIndex);
        }
    }

    // �ٸ� �÷��̾�� ������ ����ȭ�ϴ� RPC
    [PunRPC]
    public void SyncPlayerColor(int actorNumber, int colorIndex)
    {
        // RPC�� ���� ���޵� ���� �ε����� �̿��� �ش� �÷��̾��� ������ ������Ʈ
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
        //Debug.Log($"{playerName} Score: {score}");
    }

}
