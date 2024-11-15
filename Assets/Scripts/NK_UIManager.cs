using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NK_UIManager : MonoBehaviour
{
    private const string V = "Score:0";
    [SerializeField]
    private Button[] buttons = null;
    //리스트 순서 w,s,a,d,space
    
    [SerializeField]
    TextMeshProUGUI scoreTxt = null;
    //점수 텍스트, 문자열에 'Score:'를 포함해야함
    [SerializeField]
    TextMeshProUGUI playTimer = null;
    [SerializeField]
    TextMeshProUGUI countDown = null;
 
    private int score = 0;
    //키보드를 누르면 UI 버튼을 눌렀다는걸 표시하기(미구현)
    //public void KeyDownUI(KeyCode _Key) 
    //{
        
    //}    
    //public void KeyUpUI(KeyCode _Key)
    //{

    //}
    public void ResetScoreUI() //점수0 으로 리셋 함수
    {
        scoreTxt.text = V;
     }
    public void ScorePlusOneUI() // 현재점수 +1 함수 게임매니져에서 매개변수를 던져주는게 맞지만 간단하게 부르기 위해 만든 함수 아래 함수를 써도됌
    {
        score++;
        scoreTxt.text = ("Score:" + score);
    }
    public void ScoreTxtUI(int _score) // 던져준 점수 표시
    {
        score = _score;
        scoreTxt.text = ("Score:" + _score);
    }
}
