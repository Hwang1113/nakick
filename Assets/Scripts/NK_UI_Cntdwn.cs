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
        if(Input.GetKeyDown(KeyCode.R))    //������ ������Ʈ RŰ ������ ī��Ʈ �ٿ�
        {
            CountDown();
        }
        if (Input.GetKeyDown(KeyCode.S))    //������ ������Ʈ RŰ ������ ī��Ʈ �ٿ�
        {
            CountStop();
        }
    }
    public void CountDown()//5�� ī��Ʈ �ٿ��ϴ� �ۺ� �Լ� (��Ȯ���� �Ǽ� 6������ŸŸ���� �� ���� )5���� ~ 1������ ���� ���ڰ� ������ 1~0�� Start!
    {
        TM_Pro.enabled = true;
        StopCoroutine("Countdown5");
        curCnt = maxCnt; // ī��Ʈ �� �ʱ�ȭ ���⼭
        StartCoroutine("Countdown5");
    }
    IEnumerator Countdown5() 
    {
        while (curCnt >=0)
        {
            curCnt -= Time.deltaTime; //6���� �Ź� ��ŸŸ���� ��
            strCnt = ((int)curCnt).ToString(); // ���ڿ� cnt�� curCnt�� ���������� �ٲ㼭 ���� 
            if(curCnt >= 1)
            TM_Pro.text = strCnt;
            if(curCnt <1)
                TM_Pro.text = "Start!";
            yield return null;
        }
    }

    public void CountStop() //�Ͻ������� ī��Ʈ�� �ʱ�ȭ ���� ����
    {
        TM_Pro.enabled = false;
        StopCoroutine("Countdown5");
    }

}
