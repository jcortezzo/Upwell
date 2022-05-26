using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera gameCamera;

    // Start is called before the first frame update
    void Start()
    {
        gameCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        float height = 2f * gameCamera.orthographicSize;
        float width = height * gameCamera.aspect;

        Vector3 newPos =
                new Vector3(transform.position.x,
                            GlobalManager.Instance.Player.transform.position.y,
                            transform.position.z);
        transform.position = newPos;
    }
}
