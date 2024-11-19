using UnityEngine;

public class NK_PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private float speed = 0f;
    private float hAxis;
    private float vAxis;

    private bool gDown = false;

    private Vector3 moveVec;
    private Animator anim;

    private BoxCollider playerCollider; // Player의 BoxCollider 변수

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        playerCollider = GetComponent<BoxCollider>(); // Player의 BoxCollider를 찾기
    }
    //11.18 --
    //현재 발견된 버그 방향키누르면서 SPACE누르면 SPACE를 떼고 방향키를 계속 눌렀을때 계속 줍는 애니메이션 실행됌// 11.19 해결 animator isDown 에서 idle로 갈대 isRun조건이 잘못됌 true에서 false로 바꿈
    //방향키를 누르고 떼고 SPACE를 누르고 떼면 줍는 모션이 작동안함 // 이건 선입력 받는 코드 알고리즘을 짜야할듯..
    //SPACE누르고 가만히 있어도 앞으로 약간 전진함 // 해결A랑 동시에 해결됌

    private void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        gDown = Input.GetKeyDown(KeyCode.Space); //SPACE를 누르면 TRUE 한번 (나머지 시간동안 X) 

        // gDown이 true일 때만 BoxCollider를 활성화
        //if (gDown)
        //{
        //    if (playerCollider != null) 
        //    {
        //        playerCollider.enabled = true; // BoxCollider 활성화
        //    }
        //}
        //else
        //{
        //    if (playerCollider != null)
        //    {
        //        playerCollider.enabled = false; // BoxCollider 비활성화
        //    }
        //}
        if (gDown && playerCollider != null) //11.19 코드 수정(코드정리)
        {
                playerCollider.enabled = true; // BoxCollider 활성화
        }
        else if (playerCollider != null)
        {
                playerCollider.enabled = false; // BoxCollider 비활성화
        }

        // 이동 처리 전에 gDown이 true인 경우 이동을 막는다. // 11.19 gDown은 KeyDown을 받기 때문에 GetKey를 따로 만들어야할듯
        if (Input.GetKey(KeyCode.Space) && ) //if(gDown) //Space누르는 동안에 안움직이기는 하나 한번 누르고 때면 움직임
        {
            moveVec = Vector3.zero;  // 이동을 멈추기 위해 moveVec을 0으로 설정
        }
        else
        {
            moveVec = new Vector3(hAxis, 0, vAxis).normalized; // 이동 벡터 계산
        }

        // gDown이 true일 때만 이동하지 않도록 처리
        if (!gDown)
        {
            // 이동 처리
            transform.position += moveVec * speed * Time.deltaTime;
            anim.SetBool("isGet", gDown);//11.19추가(이걸로 아이템을 줍는 모션 후 가만히 있는 문제 해결)
        }

        anim.SetBool("isRun", moveVec != Vector3.zero);
        if (moveVec == Vector3.zero)
        {
            anim.SetBool("isGet", gDown);
        }

        // 플레이어가 이동할 때만 회전하도록 설정 //11.19 주울 때 회전하는걸 확인함
        if (moveVec != Vector3.zero)
        {
            transform.LookAt(transform.position + moveVec);
        }

    }
}