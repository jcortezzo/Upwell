using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField]
    private float FLAP_FORCE;
    private float MAX_SPEED;

    private Vector3 mousePosition;

    private Rigidbody2D rb;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Flap();
        }
    }

    public void Flap()
    {
        anim.Play("Player_flap");
        Vector2 direction = this.transform.position - mousePosition;
        rb.velocity = Vector2.zero;
        rb.AddForce(direction.normalized * FLAP_FORCE, ForceMode2D.Impulse);
    }
}
