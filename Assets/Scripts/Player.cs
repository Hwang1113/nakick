using UnityEngine;

public class Player : MonoBehaviour
{


    //[SerializeField] private float speed = 7f;
    //[SerializeField] private float rotateSpeed = 100f;
    //private Animator anim;
    //private float rx;
    //private float ry;

    //private void Awake()
    //{
    //    anim = GetComponent<Animator>();
    //}

    //private void Update()
    //{
    //    PlayerMove();
    //    LookAround();
    //}

    //private void PlayerMove()
    //{
    //    float horizontal = Input.GetAxisRaw("Horizontal");
    //    float vertical = Input.GetAxisRaw("Vertical");

    //    Vector3 lookForward = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z).normalized;
    //   Vector3 lookRight = new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z).normalized;
    //    Vector3 moveV = lookForward * vertical + lookRight * horizontal;

    //    transform.forward = lookForward;
    //    transform.position += moveV * Time.deltaTime * speed;

    //    anim.SetBool("isRun", moveV != Vector3.zero);
    //}

    //private void LookAround()
    //{
    //    float mouseX = Input.GetAxis("Mouse X");
    //    float mouseY = Input.GetAxis("Mouse Y");

    //    rx += rotateSpeed * mouseY * Time.deltaTime;
    //    ry += rotateSpeed * mouseX * Time.deltaTime;

    //    rx = Mathf.Clamp(rx, -70f, 70f);

    //    Camera.main.transform.eulerAngles = new Vector3(-rx, ry, 0f);
    //}
























    public float speed;
    float hAxis;
    float vAxis;
    bool gDown;


    Vector3 moveVec;
    Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        gDown = Input.GetKeyDown(KeyCode.F);


        //float h = Input.GetAxis("Horizontal");
        //transform.Translate(Vector3.right * h * speed * Time.deltaTime);

        //float v = Input.GetAxis("Vertical");
        //transform.Translate(Vector3.forward * v * speed * Time.deltaTime);


        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isGet", gDown);


        transform.LookAt(transform.position + moveVec);

    }
}
