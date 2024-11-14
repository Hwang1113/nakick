using UnityEngine;

public class Player : MonoBehaviour
{

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
        Move();

        //hAxis = Input.GetAxisRaw("Horizontal");
        //vAxis = Input.GetAxisRaw("Vertical");
        //gDown = Input.GetKeyDown(KeyCode.F);


        //moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        //transform.position += moveVec * speed * Time.deltaTime;

        //anim.SetBool("isRun", moveVec != Vector3.zero);
        //anim.SetBool("isGet", gDown);


        //transform.LookAt(transform.position + moveVec);

    }


    private void Move()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        gDown = Input.GetKeyDown(KeyCode.F);


        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isGet", gDown);


        transform.LookAt(transform.position + moveVec);

    }
}
