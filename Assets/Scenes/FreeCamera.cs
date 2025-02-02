using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    public float moveSpeed = 5.0f;      // カメラ移動速度
    public float rotationSpeed = 4.0f; // カメラ回転速度
    public float zoomSpeed = 4.0f;     // ズーム速度
    public float minZoom = 2.0f;       // 最小ズーム距離
    public float maxZoom = 10.0f;      // 最大ズーム距離

    public Collider movementBounds;

    private float rotationX = 0f;
    private float rotationY = 0f;
    private float currentZoom = 5.0f;

    void Start()
    {
        if (movementBounds == null)
        {
            Debug.LogWarning("Movement bounds (コライダー) が設定されていません！");
        }
        currentZoom = transform.position.magnitude;
        Vector3 euler = transform.rotation.eulerAngles;
        rotationX = euler.x;
        rotationY = euler.y;
    }

    void Update()
    {
        MoveCamera();
        RotateCamera();
        ZoomCamera();
        ClampPositionToBounds();
    }

    void MoveCamera()
    {
        float moveX = Input.GetAxis("Horizontal"); // A/Dキー
        float moveZ = Input.GetAxis("Vertical");   // W/Sキー
        float moveY = 0;

        if (Input.GetKey(KeyCode.E)) moveY = 1; // Eキーで上昇
        if (Input.GetKey(KeyCode.Q)) moveY = -1; // Qキーで下降

        Vector3 move = (transform.right * moveX) + (transform.forward * moveZ) + (Vector3.up * moveY);
        transform.position += move * moveSpeed * Time.deltaTime;
    }

    void RotateCamera()
    {
        if (Input.GetMouseButton(1)) // 右クリックを押している間、視点回転
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            rotationX -= mouseY;
            rotationY += mouseX;

            rotationX = Mathf.Clamp(rotationX, -90f, 90f); // 上下の回転制限

            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        }
    }

    void ZoomCamera()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scroll * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // カメラを前方にズーム（Y座標を固定）
        transform.position += transform.forward * scroll * zoomSpeed;
    }

    void ClampPositionToBounds()
    {
        if (movementBounds == null) return;

        // カメラの現在位置
        Vector3 clampedPosition = transform.position;

        // BoxCollider の範囲内に制限
        Bounds bounds = movementBounds.bounds;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, bounds.min.x, bounds.max.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, bounds.min.y, bounds.max.y);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, bounds.min.z, bounds.max.z);

        // もし制限範囲を超えていたら、戻す
        transform.position = clampedPosition;
    }
}
