using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field:SerializeField]
    public Vector2 EnterScreenPosition { get; set; }

    [SerializeField]
    private float SPEED = 5f;
    [SerializeField]
    private float WAIT_TIME = 1f;
    [SerializeField]
    private float TIME_CAN_BE_OFFSCREEN = 2F;

    private Rigidbody2D rb;
    private Coroutine deleteCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Lifecycle());
    }

    private IEnumerator Lifecycle()
    {
        yield return EnterScreen();
        yield return Attack();
        yield return WaitThenDelete();
    }

    private IEnumerator EnterScreen()
    {
        Vector2 startPosition = this.transform.position;
        Vector2 endPosition = EnterScreenPosition;
        float duration = SPEED / SPEED / 2;
        for (float t = 0f; t < duration; t += Time.deltaTime / duration)
        {
            this.transform.position = Vector2.Lerp(startPosition, endPosition, t / duration);
            yield return null;
        }
        this.transform.position = endPosition;
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(WAIT_TIME);
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        var direction = 
                GlobalManager.Instance.Player.transform.position - this.transform.position;
        rb.velocity = direction.normalized * SPEED;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Destroy self if off screen for TIME_CAN_BE_OFFSCREEN seconds
    /// </summary>
    /// <returns>WaitForSeconds</returns>
    private IEnumerator WaitThenDelete()
    {
        if (this == null || !this.isActiveAndEnabled)
        {
            yield break;
        }
        yield return new WaitForSeconds(TIME_CAN_BE_OFFSCREEN);
        while (true)
        {
            if (this != null && !GetComponent<Renderer>().isVisible)
            {
                Destroy(this.gameObject, 1f);
                break;
            }
            yield return null;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
