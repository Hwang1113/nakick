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
        if(Input.GetKeyDown(KeyCode.R))    //������ ������Ʈ RŰ ������ ī��Ʈ �ٿ�
        {
            CountDown();
        }
    }
    public void CountDown()
    {
        curCnt = maxCnt;
        StartCoroutine("Countdown5");
    }
    IEnumerator Countdown5()//5�� ī��Ʈ �ٿ��ϴ� �ۺ� �Լ�
    {
        while (curCnt >=0)
        {
            curCnt -= Time.deltaTime; //6���� �Ź� ��ŸŸ���� ��
            strCnt = ((int)curCnt).ToString(); // ���ڿ� cnt�� curCnt�� ���������� �ٲ㼭 ���� 
            yield return null;
        }
    }
}
