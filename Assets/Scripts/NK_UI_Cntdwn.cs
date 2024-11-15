using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NK_UI_Cntdwn : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI TM_Pro = null;
    public const float maxCnt = 6f; // 카운트 다운 최대시간
    public float curCnt = 0f; //현재시간 담을 변수
    public string strCnt = null; // 카운트 문자열로 반환후 보내기 위한 빈 문자열
    public float playTime = 0;
    public const float maxPlayTime = 30f;



    private void Start()
    {
        TM_Pro = transform.GetComponent<TextMeshProUGUI>();
        TM_Pro.enabled = false;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))    //디버깅용 업데이트 R키 누르면 시작 카운트 다운 
        {
            CountDownStart();
        }       
        if(Input.GetKeyDown(KeyCode.E))    //디버깅용 업데이트 R키 누르면 엔딩 카운트 다운 
        {
            CountDownEnd();
        }
        if (Input.GetKeyDown(KeyCode.Z))    //디버깅용 업데이트 Z키 누르면 카운트 다운 일시정지
        {
            CountStop();
        }       
        if (Input.GetKeyDown(KeyCode.Q))    //디버깅용 업데이트 Q키 누르면 플레이 시간 30초 카운트 다운
        {
            Play30();
        }
    }

    /// //////////////////////////////////////////////////////////

    public void CountDownStart()//5초 카운트 다운하는 퍼블릭 함수 (정확히는 실수 6에서델타타임을 뺀 정수 )5부터 ~ 1까지는 정수 숫자가 나오고 1~0은 Start!
    {
        CountStop();
        TM_Pro.enabled = true;
        curCnt = (int)maxCnt; // 카운트 초 초기화 여기서
        TM_Pro.text = null; // 문자열 초기화
        StartCoroutine("CountdownStart5");
    }
    IEnumerator CountdownStart5() 
    {
        while (curCnt >=0)
        {
            curCnt -= Time.deltaTime; //6에서 매번 델타타임을 뺌
            strCnt = ((int)curCnt).ToString(); // 문자열 cnt에 curCnt를 정수형으로 바꿔서 넣음 
            if(curCnt >= 1)
            TM_Pro.text = (strCnt);
            if(curCnt <1)
                TM_Pro.text = "Start!";
            yield return null;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////

    public void CountDownEnd()//("Go to lobby on\n"+5)초 카운트 다운하는 퍼블릭 함수 (정확히는 실수 6에서델타타임을 뺀 정수 )5부터 ~ 0까지 
    {
        CountStop();
        TM_Pro.enabled = true;
        curCnt = maxCnt; // 카운트 초 초기화 여기서
        TM_Pro.text = null; // 문자열 초기화
        StartCoroutine("CountdownEnding5");
    }

    IEnumerator CountdownEnding5() 
    {
        while (curCnt >=0)
        {
            curCnt -= Time.deltaTime; //6에서 매번 델타타임을 뺌
            strCnt = ((int)curCnt).ToString(); // 문자열 cnt에 curCnt를 정수형으로 바꿔서 넣음 
            TM_Pro.text = ("Go to lobby on\n" + strCnt);
            if(curCnt<0)
                TM_Pro.text = null; // 문자열 초기화
            yield return null;
        }
    }
    //=================================    
    public void Play30()
    {
        CountStop();
        TM_Pro.enabled = true;
        curCnt = maxPlayTime; // 카운트 초 초기화 여기서
        TM_Pro.text = null; // 문자열 초기화
        StartCoroutine("Countdown30");
    }
    IEnumerator Countdown30()
    {
        while (curCnt >= 0)
        {
            curCnt -= Time.deltaTime; //6에서 매번 델타타임을 뺌
            strCnt = curCnt.ToString("N2"); // 문자열 cnt에 curCnt를 정수형으로 바꿔서 넣음 
            TM_Pro.text = (strCnt);
            if (curCnt < 0)
                TM_Pro.text = null; // 문자열 초기화
            yield return null;
        }
    }
    //=================================


    public void CountStop() //일시정지만 카운트는 초기화 되지 않음, 카운트 다운 UI 안보이게
    {
        TM_Pro.enabled = false;
        StopCoroutine("CountdownStart5");
        StopCoroutine("CountdownEnding5"); 
        StopCoroutine("Countdown30");
    }
}
