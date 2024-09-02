using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 direction;

    private void Start()
    {
        direction = direction.normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
