using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField]
    private float FLAP_FORCE;
    private float MAX_SPEED;
    [SerializeField]
    private float TURN_SPEED = 0.2f;
    [SerializeField]
    private float RESET_SPEED = 1f;

    private Vector3 mousePosition;

    private Rigidbody2D rb;
    private Animator anim;
    private Coroutine rotationCoroutine;
    private Camera gameCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gameCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ScreenWrap();
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Flap();
        }
    }

    public void Flap()
    {
        anim.Play("Player_flap", -1, 0);
        Vector2 direction = this.transform.position - mousePosition;
        rb.velocity = Vector2.zero;
        rb.AddForce(direction.normalized * FLAP_FORCE, ForceMode2D.Impulse);
        
        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
        }
        rotationCoroutine = StartCoroutine(FlapRoutine(Rotate(TURN_SPEED, direction.normalized)));
    }

    public IEnumerator FlapRoutine(IEnumerator flapRoutine)
    {
        yield return flapRoutine;
        yield return Rotate(RESET_SPEED, Vector2.up);
    }

    public IEnumerator Rotate(float duration, Vector2 direction)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, direction));

        for (float t = 0f; t < duration; t += Time.deltaTime / duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t / duration);
            yield return null;
        }
    }

    void ScreenWrap()
    {
        float height = 2f * gameCamera.orthographicSize;
        float width = height * gameCamera.aspect;

        float minXBound = gameCamera.transform.position.x - width / 2f;
        float maxXBound = gameCamera.transform.position.x + width / 2f;

        float minY = gameCamera.transform.position.y - height / 2f;

        if (transform.position.x > maxXBound)
        {
            transform.position = new Vector2(minXBound, transform.position.y);
        }
        else if (transform.position.x < minXBound)
        {
            transform.position = new Vector2(maxXBound, transform.position.y);
        }
    }
}
