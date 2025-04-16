using UnityEngine;

public class BarPushJump : MonoBehaviour
{
    public float pushDistance = 1f;      // Z方向に進む距離
    public float pushDuration = 0.2f;    // 押し出しにかかる時間

    private float pushTimer = 0f;
    private bool isPushing = false;
    private float originalZ;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isPushing)
        {
            isPushing = true;
            pushTimer = 0f;
            originalZ = transform.position.z; // Zの初期位置だけ覚えとく
        }

        if (isPushing)
        {
            pushTimer += Time.deltaTime;
            float progress = pushTimer / pushDuration;

            float zOffset = Mathf.Sin(progress * Mathf.PI) * pushDistance;

            // XとYは現在位置そのまま、Zだけ動かす
            Vector3 currentPos = transform.position;
            transform.position = new Vector3(currentPos.x, currentPos.y, originalZ + zOffset);

            if (progress >= 1f)
            {
                isPushing = false;
                transform.position = new Vector3(currentPos.x, currentPos.y, originalZ);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ジャンプ中だけ反応させたい場合
        if (isPushing && other.CompareTag("Ball"))
        {
            Rigidbody ballRb = other.GetComponent<Rigidbody>();
            if (ballRb != null)
            {
                // Z方向にちょっと加速（3f~5fとか）
                Vector3 force = Vector3.forward * 25f;
                ballRb.AddForce(force, ForceMode.Impulse);
            }
        }
    }

}
