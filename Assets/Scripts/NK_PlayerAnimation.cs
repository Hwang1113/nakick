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

    private void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        gDown = Input.GetKeyDown(KeyCode.Space);

        // gDown이 true일 때만 BoxCollider를 활성화
        if (gDown)
        {
            if (playerCollider != null)
            {
                playerCollider.enabled = true; // BoxCollider 활성화
            }
        }
        else
        {
            if (playerCollider != null)
            {
                playerCollider.enabled = false; // BoxCollider 비활성화
            }
        }

        // 이동 처리 전에 gDown이 true인 경우 이동을 막는다.
        if (gDown)
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
        }

        anim.SetBool("isRun", moveVec != Vector3.zero);
        if (moveVec == Vector3.zero)
        {
            anim.SetBool("isGet", gDown);
        }

        // 플레이어가 이동할 때만 회전하도록 설정
        if (moveVec != Vector3.zero)
        {
            transform.LookAt(transform.position + moveVec);
        }
    }
}