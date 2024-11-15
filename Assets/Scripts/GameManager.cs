using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using System.Threading;

public class GameManager : MonoBehaviour
{
    //public static GameManager instance;

    private float PtimeCount;
    private float StimeCount;

    private bool isplaying = false;

    public NK_UI_Cntdwn NKUI;

    [SerializeField]
    private NK_UIManager UIMG;
    private GameObject playtimer;

    public GameObject gameover;
    public Button lobby_BT;

    [SerializeField]
    private NK_UI_Cntdwn[] cntDwns= null; 
    //0.PlayTimer , 1.Cntdown
    [SerializeField]
    private GameObject Trash = null;
    [SerializeField]
    private List<GameObject> TrashList = null;

    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //}

        // NK_UI_Cntdwn.Instance.CountDownStart();

        //kjjk.CountDownStart();
    }



    private void Start()
    {
        CreateItem();
        NKUI.CountDownStart();
        //cntDwns = UIMG.GetComponentsInChildren<NK_UI_Cntdwn>();
        //PtimeCount = cntDwns[0].curCnt;
        //StimeCount = cntDwns[1].curCnt;
    }

    private void Update()
    {

        if (!isplaying && NKUI.curCnt < 0f)
        {
            NKUI.Play30();
            isplaying = true;
        }
        if (isplaying && NKUI.curCnt < 0f)
        {
            NKUI.CountDownEnd();
        }
    }


    public void StartGame()
    {
        SceneManager.LoadScene("C-1");
    }

    public void EndGame()
    {
       // Time.timeScale = 0;
        //gameover.SetActive(true);
       // lobby_BT.gameObject.SetActive(true);
        GoLobby();
    }

    public void GoLobby()
    {
       // Time.timeScale = 1;
       // gameObject.SetActive(false);
        SceneManager.LoadScene("B.Lobby");
    }

    public void CreateItem()
    {
        for (int i = 0; i < 30; ++i)
        {
            Vector3 randomT = new Vector3(
                Random.Range(-12, 12),
                0.08f,
                Random.Range(-12, 12)
                );

            GameObject TGo = Instantiate(Trash, randomT, Quaternion.identity);
            TrashList.Add(TGo);
        }

    }
}