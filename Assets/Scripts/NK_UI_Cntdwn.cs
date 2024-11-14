using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_UI_Cntdwn : MonoBehaviour
{
    public const float maxCnt = 6f;
    public float curCnt = 0f;
    public string strCnt = null; 

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))    //디버깅용 업데이트 R키 누르면 카운트 다운
        {
            CountDown();
        }
    }
    public void CountDown()
    {
        curCnt = maxCnt;
        StartCoroutine("Countdown5");
    }
    IEnumerator Countdown5()//5초 카운트 다운하는 퍼블릭 함수
    {
        while (curCnt >=0)
        {
            curCnt -= Time.deltaTime; //6에서 매번 델타타임을 뺌
            strCnt = ((int)curCnt).ToString(); // 문자열 cnt에 curCnt를 정수형으로 바꿔서 넣음 
            yield return null;
        }
    }
}
