using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.Antlr3.Runtime.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [SerializeField]
    private NK_UIManager UIMG;
    private GameObject playtimer;

    public GameObject gameover;
    public Button lobby_BT;


    [SerializeField]
    private GameObject Trash = null;
    [SerializeField]
    private List<GameObject> TrashList = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }



    private void Start()
    {
        CreateItem();

        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("C-1");
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        gameover.SetActive(true);
        lobby_BT.gameObject.SetActive(true);
        GoLobby();
    }

    public void GoLobby()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
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

            GameObject hGo = Instantiate(Trash, randomT, Quaternion.identity);

            TrashList.Add(hGo);
        }

    }
}