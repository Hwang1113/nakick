using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NK_UI_Cntdwn : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI TM_Pro = null;
    public const float maxCnt = 6f;
    public float curCnt = 0f;
    public string strCnt = null;
    private void Start()
    {
        TM_Pro = transform.GetComponent<TextMeshProUGUI>();
        TM_Pro.enabled = false;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))    //디버깅용 업데이트 R키 누르면 카운트 다운
        {
            CountDown();
        }
        if (Input.GetKeyDown(KeyCode.S))    //디버깅용 업데이트 R키 누르면 카운트 다운
        {
            CountStop();
        }
    }
    public void CountDown()//5초 카운트 다운하는 퍼블릭 함수 (정확히는 실수 6에서델타타임을 뺀 정수 )5부터 ~ 1까지는 정수 숫자가 나오고 1~0은 Start!
    {
        TM_Pro.enabled = true;
        StopCoroutine("Countdown5");
        curCnt = maxCnt; // 카운트 초 초기화 여기서
        StartCoroutine("Countdown5");
    }
    IEnumerator Countdown5() 
    {
        while (curCnt >=0)
        {
            curCnt -= Time.deltaTime; //6에서 매번 델타타임을 뺌
            strCnt = ((int)curCnt).ToString(); // 문자열 cnt에 curCnt를 정수형으로 바꿔서 넣음 
            if(curCnt >= 1)
            TM_Pro.text = strCnt;
            if(curCnt <1)
                TM_Pro.text = "Start!";
            yield return null;
        }
    }

    public void CountStop() //일시정지만 카운트는 초기화 되지 않음
    {
        TM_Pro.enabled = false;
        StopCoroutine("Countdown5");
    }

}
