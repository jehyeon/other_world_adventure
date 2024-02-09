using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Vector3 defaultPosition = new Vector3(0f, 0f, -10f);
    [SerializeField]
    private float cameraSpeed = 1f;

    private float h;
    private float v;
    private Vector3 dir;

    private void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            dir = new Vector3(h, v, 0);
            transform.position += dir.normalized * Time.deltaTime * cameraSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = defaultPosition;
        }
    }
}
