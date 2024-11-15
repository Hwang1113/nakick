using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NK_UIManager : MonoBehaviour
{
    private const string V = "Score:0";
    [SerializeField]
    private Button[] buttons = null;
    //����Ʈ ���� w,s,a,d,space
    
    [SerializeField]
    TextMeshProUGUI scoreTxt = null;
    //���� �ؽ�Ʈ, ���ڿ��� 'Score:'�� �����ؾ���
    [SerializeField]
    TextMeshProUGUI playTimer = null;
    [SerializeField]
    TextMeshProUGUI countDown = null;
 
    private int score = 0;
    //Ű���带 ������ UI ��ư�� �����ٴ°� ǥ���ϱ�(�̱���)
    //public void KeyDownUI(KeyCode _Key) 
    //{
        
    //}    
    //public void KeyUpUI(KeyCode _Key)
    //{

    //}
    public void ResetScoreUI() //����0 ���� ���� �Լ�
    {
        scoreTxt.text = V;
     }
    public void ScorePlusOneUI() // �������� +1 �Լ� ���ӸŴ������� �Ű������� �����ִ°� ������ �����ϰ� �θ��� ���� ���� �Լ� �Ʒ� �Լ��� �ᵵ��
    {
        score++;
        scoreTxt.text = ("Score:" + score);
    }
    public void ScoreTxtUI(int _score) // ������ ���� ǥ��
    {
        score = _score;
        scoreTxt.text = ("Score:" + _score);
    }
}
