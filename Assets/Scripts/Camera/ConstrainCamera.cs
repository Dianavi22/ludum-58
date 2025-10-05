using UnityEngine;

public class ConstrainCamera : MonoBehaviour
{
    [SerializeField] private Transform _bottomLeft;
    [SerializeField] private Transform _topRight;
    [SerializeField] private Transform _target;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.size = OrthographicBounds().size;
    }

    private void FixedUpdate()
    {
        Vector3 target = new(_target.position.x, _target.position.y, transform.position.z);
        _rigidbody.MovePosition(Vector3.Lerp(transform.position, target, 0.05f));
    }

    public static Bounds OrthographicBounds()
    {
        float screenAspect = (float)Screen.width / Screen.height;
        float cameraHeight = Camera.main.orthographicSize * 2;
        Bounds bounds = new Bounds(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0), new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }
}
