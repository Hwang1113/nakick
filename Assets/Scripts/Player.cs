using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;

    Vector3 moveVec;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
}
