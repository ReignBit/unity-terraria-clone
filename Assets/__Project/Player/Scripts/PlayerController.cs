using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]
    [Range(1,20f)]
    float normalSpeed, sprintSpeed;
    float moveSpeed;

    [SerializeField]
    [Range(1f,20f)]
    float jumpForce = 50f;
    float floorSenseRange = 1.7f;


    [SerializeField]
    bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = GetGroundedStatus();

        Vector2 delta = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
           delta.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            delta.x = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            moveSpeed = sprintSpeed;
        }
        else{
            moveSpeed = normalSpeed;
        }


        rb.position += new Vector2(
            delta.x, delta.y
        ) * moveSpeed * Time.deltaTime;
    }

    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    bool GetGroundedStatus()
    {
        Debug.DrawRay(transform.position, Vector3.down * floorSenseRange, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, floorSenseRange);

        if (hit.collider)
        {
            return true;
        }
        moveSpeed = normalSpeed - 2f;
        return false;
    }
}
