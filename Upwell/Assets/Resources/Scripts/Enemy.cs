using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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
        StartCoroutine(Attack());
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

    private void OnBecameInvisible()
    {
        if (deleteCoroutine != null)
        {
            StopCoroutine(deleteCoroutine);
        }
        if (this != null && this.isActiveAndEnabled)
        {
            deleteCoroutine = StartCoroutine(WaitThenDelete());
        }
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
        if (this != null && !GetComponent<Renderer>().isVisible)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
