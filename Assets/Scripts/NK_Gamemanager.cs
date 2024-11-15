using Photon.Realtime;
using UnityEngine;

using Photon.Pun;

using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class NK_Gamemanager : MonoBehaviourPunCallbacks //
{
    //[SerializeField]
    //private GameObject NK_Trash = null;



    [SerializeField]
    private GameObject playerPrefab = null;

    //�� Ŭ���̾�Ʈ ���� ������ �÷��̾� ���� ������Ʈ�� �迭�� ������
    private GameObject[] playerGoList = new GameObject[4];

    private Player playerCtrl = null;

    private void Start()
    {
        if (playerPrefab != null)
        {
            DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool; //
            if (!pool.ResourceCache.ContainsKey(playerPrefab.name)) //Ű�� ���ٸ�
                pool.ResourceCache.Add(playerPrefab.name, playerPrefab); //(��ųʸ�) Ű �߰�

            Invoke("SpawnPlayer", 0.5f);

            //GameObject go = PhotonNetwork.Instantiate(
            //    playerPrefab.name,
            //    new Vector3(
            //        Random.Range(-10.0f, 10.0f),
            //        0.0f,
            //        Random.Range(-10.0f, 10.0f)),
            //    Quaternion.identity,
            //    0);
            //go.GetComponent<PUNPlayerCtrl>().SetMaterial(PhotonNetwork.CurrentRoom.PlayerCount);
        }
    }

    private void Update()
    {
       
    }
    //PhotonNetwork.LeaveRoom �Լ��� ȣ��Ǹ� ȣ��
    public override void OnLeftRoom()
    {
        Debug.Log("Left Room");

        SceneManager.LoadScene("20241111_PhotonLauncher");
    }
    //�÷��̾ ������ �� ȣ��Ǵ� �Լ�
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player otherPlayer)//namespace�� �߿伺
    {
        Debug.LogFormat("Player Entered Room: {0}", otherPlayer.NickName);

        //������ �����ϸ� ��ü Ŭ���̾�Ʈ���� �Լ� ȣ��
        //photonView.RPC("ApplyPlayerList", RpcTarget.All);

        // �÷��̾� ���� ����ȭ (���� ������ �÷��̾�� ���� �ε����� ����ȭ)
        int colorIndex = PhotonNetwork.CurrentRoom.PlayerCount - 1; // �÷��̾� ������ �´� ���� �ε���
        photonView.RPC("SyncPlayerColor", RpcTarget.AllBuffered, otherPlayer.ActorNumber, colorIndex);
    }

    [PunRPC] //PhotonServerSettings�� RPC List�� �ڵ����� ����ȭ
    public void ApplyPlayerList()
    {
        //���� �濡 ������ �ִ� �÷��̾��� ��
        Debug.LogError("CurrentRoom PlayerCount : " + PhotonNetwork.CurrentRoom.PlayerCount);

        //���� �÷��̾��� ����� ��� Ŭ���̾�Ʈ�� ������ ������
        //������ ���ϴ�
        //���� �����Ǿ� �ִ� ��� ����� ��������
        //PhotonView[] photonViews = FindObjectsOfType<PhotonView>(); //������ �� ��
        // ->
        PhotonView[] photonViews = FindObjectsByType<PhotonView>(FindObjectsSortMode.None);


        //�Ź� �������� �Ͽ� �÷��̾� ���ӿ�����Ʈ ����Ʈ�� �ʱ�ȭ
        System.Array.Clear(playerGoList, 0, playerGoList.Length);

        //���� �����Ǿ� �ִ� ����� ��ü��
        //�������� �÷��̾���� ���ͳѹ��� ����,
        //���ͳѹ��� �������� �÷��̾� ���ӿ�����Ʈ �迭�� ä��
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; ++i)//���� �÷��̾��� �� ��ŭ ����
        {
            //�÷��̾� Ű ���� ������� �������� 1�� ����
            //Ű��  0�� �ƴ� 1���� ����
            int key = i + 1;
            for (int j = 0; j < photonViews.Length; ++j)
            {
                //���� PhotonNetwork.Instantiate�� ���ؼ� ������ ����䰡 �ƴ϶�� �ѱ�
                if (photonViews[j].isRuntimeInstantiated == false) continue;
                //���� ���� Ű ���� ��ųʸ� ���� �������� �ʴ´ٸ� �ѱ�
                if (PhotonNetwork.CurrentRoom.Players.ContainsKey(key) == false) continue;

                //������� ���ͳѹ�
                int viewNum = photonViews[j].Owner.ActorNumber;
                //�������� �÷��̾��� ���ͳѹ�
                int playerNum = PhotonNetwork.CurrentRoom.Players[key].ActorNumber;

                //���ͳѹ��� ���� ������Ʈ�� �ִٸ�,
                if (viewNum == playerNum)
                {
                    //���� ���ӿ�����Ʈ�� �迭�� �߰�
                    playerGoList[playerNum - 1] = photonViews[j].gameObject;
                    //���ӿ�����Ʈ �̸��� �˾ƺ��� ���� ����
                    playerGoList[playerNum - 1].name = "player_" + photonViews[j].Owner.NickName;
                }
            }
        }

        //�����
        PrintPlayerList();
    }

    private void PrintPlayerList()
    {
        foreach (GameObject go in playerGoList)
        {
            if (go != null)
            {
                Debug.LogError(go.name); //����� : ����׸��� ����, ����׸��� Debug.Log�� ������ ����, ������ ����� ����
                                         //������ ������ �����â�� ��������
            }
        }
    }
    //�÷��̾ ���� �� ȣ��Ǵ� �Լ�
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.LogFormat("Player Left Room: {0}", otherPlayer.NickName);
    }

    public void LeaveRoom()
    {
        Debug.Log("Leave Room");

        PhotonNetwork.LeaveRoom();
    }

    private void SpawnPlayer()
    {
        GameObject go = PhotonNetwork.Instantiate( //PhotonNetwork.Instantiate - ���� ������ ���� Room�� ��� �÷��̾�� ��������� ��
                playerPrefab.name, //���ϸ��� ����
                new Vector3(
                    Random.Range(-10.0f, 10.0f),
                    0.0f,
                    Random.Range(-10.0f, 10.0f)),
                Quaternion.identity,
                0); //�׷��ȣ = ��Ƽ ����
        playerCtrl = go.GetComponent<Player>();
        playerCtrl.SetMaterial(PhotonNetwork.CurrentRoom.PlayerCount); //�ٸ� ����鿡�� �����ϴ� �ڵ尡 �ƴ�, �ٸ� ����� ������ �ٲ����� ����

        //������ �����ϸ� ��ü Ŭ���̾�Ʈ���� �Լ� ȣ��
        //photonView
        //View ID : �����ϱ� ���� ��ȣ - Ŭ���̾�Ʈ���� �޸��ּҰ� �ٸ��Ƿ� ��Ʈ��ũ���� �����Ϸ��� ��ȣ�� �ο���
        //Observed Components : ������ ������ ���ΰ�
        //Remote Procedure Call - �������� �Լ��� ȣ�� -> ApplyPlayerList : �÷��̾� ��� ����ȭ�� ���� ���� �޼���
        //RpcTarget.All - �������� ��� �е�(�� ����)�� �� �޼��带 ȣ���ϼ���
        //RpcTarget.AllBuffered - ���ۿ� ����, ���Ŀ� ���� ����鵵 ȣ���ض�
        //         .MasterClient - host�� ȣ��,  ��� �� �ٽ� �˷��ִ� ��Ŀ��� ���
        //         .others - ���� �¾����� �ٸ� �̵鿡�� ����ȭ��Ŵ
        photonView.RPC("ApplyPlayerList", RpcTarget.All);
    }
}











