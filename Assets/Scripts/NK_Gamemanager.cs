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

    //각 클라이언트 마다 생성된 플레이어 게임 오브젝트를 배열로 관리함
    private GameObject[] playerGoList = new GameObject[4];

    private Player playerCtrl = null;

    private void Start()
    {
        if (playerPrefab != null)
        {
            DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool; //
            if (!pool.ResourceCache.ContainsKey(playerPrefab.name)) //키가 없다면
                pool.ResourceCache.Add(playerPrefab.name, playerPrefab); //(딕셔너리) 키 추가

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
    //PhotonNetwork.LeaveRoom 함수가 호출되면 호출
    public override void OnLeftRoom()
    {
        Debug.Log("Left Room");

        SceneManager.LoadScene("20241111_PhotonLauncher");
    }
    //플레이어가 입장할 때 호출되는 함수
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player otherPlayer)//namespace의 중요성
    {
        Debug.LogFormat("Player Entered Room: {0}", otherPlayer.NickName);

        //누군가 접속하면 전체 클라이언트에서 함수 호출
        //photonView.RPC("ApplyPlayerList", RpcTarget.All);

        // 플레이어 색상 동기화 (새로 입장한 플레이어에게 색상 인덱스를 동기화)
        int colorIndex = PhotonNetwork.CurrentRoom.PlayerCount - 1; // 플레이어 순서에 맞는 색상 인덱스
        photonView.RPC("SyncPlayerColor", RpcTarget.AllBuffered, otherPlayer.ActorNumber, colorIndex);
    }

    [PunRPC] //PhotonServerSettings의 RPC List에 자동으로 동기화
    public void ApplyPlayerList()
    {
        //현재 방에 접속해 있는 플레이어의 수
        Debug.LogError("CurrentRoom PlayerCount : " + PhotonNetwork.CurrentRoom.PlayerCount);

        //현재 플레이어의 목록을 모든 클라이언트가 가지고 있으면
        //관리가 편하다
        //현재 생성되어 있는 모든 포톤뷰 가져오기
        //PhotonView[] photonViews = FindObjectsOfType<PhotonView>(); //지금은 못 씀
        // ->
        PhotonView[] photonViews = FindObjectsByType<PhotonView>(FindObjectsSortMode.None);


        //매번 재정렬을 하여 플레이어 게임오브젝트 리스트를 초기화
        System.Array.Clear(playerGoList, 0, playerGoList.Length);

        //현재 생성되어 있는 포톤부 전체와
        //접속중인 플레이어들의 액터넘버를 비교해,
        //액터넘버를 기준으로 플레이어 게임오브젝트 배열을 채움
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; ++i)//현재 플레이어의 수 만큼 돈다
        {
            //플레이어 키 값은 만들어진 순서부터 1로 시작
            //키는  0이 아닌 1부터 시작
            int key = i + 1;
            for (int j = 0; j < photonViews.Length; ++j)
            {
                //만약 PhotonNetwork.Instantiate를 통해서 생성된 포톤뷰가 아니라면 넘김
                if (photonViews[j].isRuntimeInstantiated == false) continue;
                //만약 현재 키 값이 딕셔너리 내에 존재하지 않는다면 넘김
                if (PhotonNetwork.CurrentRoom.Players.ContainsKey(key) == false) continue;

                //포톤뷰의 액터넘버
                int viewNum = photonViews[j].Owner.ActorNumber;
                //접속중인 플레이어의 액터넘버
                int playerNum = PhotonNetwork.CurrentRoom.Players[key].ActorNumber;

                //액터넘버가 같은 오브젝트가 있다면,
                if (viewNum == playerNum)
                {
                    //실제 게임오브젝트를 배열에 추가
                    playerGoList[playerNum - 1] = photonViews[j].gameObject;
                    //게임오브젝트 이름도 알아보기 쉽게 변경
                    playerGoList[playerNum - 1].name = "player_" + photonViews[j].Owner.NickName;
                }
            }
        }

        //디버그
        PrintPlayerList();
    }

    private void PrintPlayerList()
    {
        foreach (GameObject go in playerGoList)
        {
            if (go != null)
            {
                Debug.LogError(go.name); //빌드시 : 디버그모드로 빌드, 디버그모드는 Debug.Log가 나오지 않음, 에러로 띄워야 나옴
                                         //보통은 별도의 디버그창을 만들어야함
            }
        }
    }
    //플레이어가 나갈 때 호출되는 함수
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
        GameObject go = PhotonNetwork.Instantiate( //PhotonNetwork.Instantiate - 나의 정보를 같은 Room의 모든 플레이어에게 만들어지게 함
                playerPrefab.name, //파일명을 넣음
                new Vector3(
                    Random.Range(-10.0f, 10.0f),
                    0.0f,
                    Random.Range(-10.0f, 10.0f)),
                Quaternion.identity,
                0); //그룹번호 = 파티 관리
        playerCtrl = go.GetComponent<Player>();
        playerCtrl.SetMaterial(PhotonNetwork.CurrentRoom.PlayerCount); //다른 사람들에게 동작하는 코드가 아님, 다른 사람의 색깔을 바꿔주지 못함

        //누군가 접속하면 전체 클라이언트에서 함수 호출
        //photonView
        //View ID : 관리하기 위한 번호 - 클라이언트별로 메모리주소가 다르므로 네트워크에서 관리하려면 번호를 부여함
        //Observed Components : 무엇을 관리할 것인가
        //Remote Procedure Call - 원격으로 함수를 호출 -> ApplyPlayerList : 플레이어 목록 동기화를 위해 만든 메서드
        //RpcTarget.All - 접속중인 모든 분들(나 포함)은 이 메서드를 호출하세요
        //RpcTarget.AllBuffered - 버퍼에 저장, 이후에 오는 사람들도 호출해라
        //         .MasterClient - host만 호출,  계산 후 다시 알려주는 방식에서 사용
        //         .others - 내가 맞았을때 다른 이들에게 동기화시킴
        photonView.RPC("ApplyPlayerList", RpcTarget.All);
    }
}











