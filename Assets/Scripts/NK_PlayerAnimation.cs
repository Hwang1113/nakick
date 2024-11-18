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

    private BoxCollider playerCollider; // Player�� BoxCollider ����

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        playerCollider = GetComponent<BoxCollider>(); // Player�� BoxCollider�� ã��
    }
    //11.18 --
    //���� �߰ߵ� ���� ����Ű�����鼭 SPACE������ SPACE�� ���� ����Ű�� ��� �������� ��� �ݴ� �ִϸ��̼� ������
    //����Ű�� ������ ���� SPACE�� ������ ���� �ݴ� ����� �۵�����
    //SPACE������ ������ �־ ������ �ణ ������

    private void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        gDown = Input.GetKeyDown(KeyCode.Space); //SPACE�� ������ TRUE �ѹ� (������ �ð����� X) 

        // gDown�� true�� ���� BoxCollider�� Ȱ��ȭ
        if (gDown)
        {
            if (playerCollider != null)
            {
                playerCollider.enabled = true; // BoxCollider Ȱ��ȭ
            }
        }
        else
        {
            if (playerCollider != null)
            {
                playerCollider.enabled = false; // BoxCollider ��Ȱ��ȭ
            }
        }

        // �̵� ó�� ���� gDown�� true�� ��� �̵��� ���´�.
        if (gDown)
        {
            moveVec = Vector3.zero;  // �̵��� ���߱� ���� moveVec�� 0���� ����
        }
        else
        {
            moveVec = new Vector3(hAxis, 0, vAxis).normalized; // �̵� ���� ���
        }

        // gDown�� true�� ���� �̵����� �ʵ��� ó��
        if (!gDown)
        {
            // �̵� ó��
            transform.position += moveVec * speed * Time.deltaTime;
        }

        anim.SetBool("isRun", moveVec != Vector3.zero);
        if (moveVec == Vector3.zero)
        {
            anim.SetBool("isGet", gDown);
        }

        // �÷��̾ �̵��� ���� ȸ���ϵ��� ����
        if (moveVec != Vector3.zero)
        {
            transform.LookAt(transform.position + moveVec);
        }
    }
}