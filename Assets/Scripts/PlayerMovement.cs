using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;

    [SerializeField] List<GameObject> NPC;
    [SerializeField] GameObject dialoge;


    Vector2 movement;

    [SerializeField] bool sitting = false;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //mousePos = cam.ScreenToWorldPoint(Input.mousePosition); // Where the mouse is on screen


        // Set the values to the animator parameters
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        
        if (sitting)
            moveSpeed = 0f;
        else
            moveSpeed = 3f;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool("Sit", false);
            sitting = false;
        }

        if (Input.GetMouseButtonDown(0))
            dialoge.SetActive(true);
    }

    private void FixedUpdate()
    {
        movement.Normalize(); // Normalize the vector so when 2 input keys are pressed at the same time the speed doesn't double
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CanSit")
        {
            sitting = true;
            animator.SetBool("Sit", true);
            transform.position = new Vector3(1.32176638f, 0.960000038f, -0.0119729787f);
            foreach (GameObject item in NPC)
            {
                item.gameObject.SetActive(true);
            }

        }
    }
}
