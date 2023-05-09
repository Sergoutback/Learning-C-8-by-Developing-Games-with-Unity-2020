using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Vector3 camOffset = new(0f, 1.2f, -2.6f);

    private Transform target;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        transform.position = target.TransformPoint(camOffset);
        transform.LookAt(target);
    }
}