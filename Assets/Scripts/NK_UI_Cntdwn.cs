using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NK_UI_Cntdwn : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI TM_Pro = null;
    public const float maxCnt = 6f; // ī��Ʈ �ٿ� �ִ�ð�
    public float curCnt = 0f; //����ð� ���� ����
    public string strCnt = null; // ī��Ʈ ���ڿ��� ��ȯ�� ������ ���� �� ���ڿ�
    public float playTime = 0;
    public const float maxPlayTime = 30f;



    private void Start()
    {
        TM_Pro = transform.GetComponent<TextMeshProUGUI>();
        TM_Pro.enabled = false;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))    //������ ������Ʈ RŰ ������ ���� ī��Ʈ �ٿ� 
        {
            CountDownStart();
        }       
        if(Input.GetKeyDown(KeyCode.E))    //������ ������Ʈ RŰ ������ ���� ī��Ʈ �ٿ� 
        {
            CountDownEnd();
        }
        if (Input.GetKeyDown(KeyCode.Z))    //������ ������Ʈ ZŰ ������ ī��Ʈ �ٿ� �Ͻ�����
        {
            CountStop();
        }       
        if (Input.GetKeyDown(KeyCode.Q))    //������ ������Ʈ QŰ ������ �÷��� �ð� 30�� ī��Ʈ �ٿ�
        {
            Play30();
        }
    }

    /// //////////////////////////////////////////////////////////

    public void CountDownStart()//5�� ī��Ʈ �ٿ��ϴ� �ۺ� �Լ� (��Ȯ���� �Ǽ� 6������ŸŸ���� �� ���� )5���� ~ 1������ ���� ���ڰ� ������ 1~0�� Start!
    {
        CountStop();
        TM_Pro.enabled = true;
        curCnt = (int)maxCnt; // ī��Ʈ �� �ʱ�ȭ ���⼭
        TM_Pro.text = null; // ���ڿ� �ʱ�ȭ
        StartCoroutine("CountdownStart5");
    }
    IEnumerator CountdownStart5() 
    {
        while (curCnt >=0)
        {
            curCnt -= Time.deltaTime; //6���� �Ź� ��ŸŸ���� ��
            strCnt = ((int)curCnt).ToString(); // ���ڿ� cnt�� curCnt�� ���������� �ٲ㼭 ���� 
            if(curCnt >= 1)
            TM_Pro.text = (strCnt);
            if(curCnt <1)
                TM_Pro.text = "Start!";
            yield return null;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////

    public void CountDownEnd()//("Go to lobby on\n"+5)�� ī��Ʈ �ٿ��ϴ� �ۺ� �Լ� (��Ȯ���� �Ǽ� 6������ŸŸ���� �� ���� )5���� ~ 0���� 
    {
        CountStop();
        TM_Pro.enabled = true;
        curCnt = maxCnt; // ī��Ʈ �� �ʱ�ȭ ���⼭
        TM_Pro.text = null; // ���ڿ� �ʱ�ȭ
        StartCoroutine("CountdownEnding5");
    }

    IEnumerator CountdownEnding5() 
    {
        while (curCnt >=0)
        {
            curCnt -= Time.deltaTime; //6���� �Ź� ��ŸŸ���� ��
            strCnt = ((int)curCnt).ToString(); // ���ڿ� cnt�� curCnt�� ���������� �ٲ㼭 ���� 
            TM_Pro.text = ("Go to lobby on\n" + strCnt);
            if(curCnt<0)
                TM_Pro.text = null; // ���ڿ� �ʱ�ȭ
            yield return null;
        }
    }
    //=================================    
    public void Play30()
    {
        CountStop();
        TM_Pro.enabled = true;
        curCnt = maxPlayTime; // ī��Ʈ �� �ʱ�ȭ ���⼭
        TM_Pro.text = null; // ���ڿ� �ʱ�ȭ
        StartCoroutine("Countdown30");
    }
    IEnumerator Countdown30()
    {
        while (curCnt >= 0)
        {
            curCnt -= Time.deltaTime; //6���� �Ź� ��ŸŸ���� ��
            strCnt = curCnt.ToString("N2"); // ���ڿ� cnt�� curCnt�� ���������� �ٲ㼭 ���� 
            TM_Pro.text = (strCnt);
            if (curCnt < 0)
                TM_Pro.text = null; // ���ڿ� �ʱ�ȭ
            yield return null;
        }
    }
    //=================================


    public void CountStop() //�Ͻ������� ī��Ʈ�� �ʱ�ȭ ���� ����, ī��Ʈ �ٿ� UI �Ⱥ��̰�
    {
        TM_Pro.enabled = false;
        StopCoroutine("CountdownStart5");
        StopCoroutine("CountdownEnding5"); 
        StopCoroutine("Countdown30");
    }
}
