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

    private void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        gDown = Input.GetKeyDown(KeyCode.Space);

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