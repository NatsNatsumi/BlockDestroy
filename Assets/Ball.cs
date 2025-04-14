using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody myRigid;
    public GameManager myManager;

    [System.Obsolete]
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();

        // 初速を与える（正規化して一定のスピードに）
        Vector3 dir = (transform.forward * 0.8f + transform.right * 0.6f).normalized;

        myRigid.velocity = dir * speed;

        // dragを0にして減速しないように
        myRigid.drag = 0f;
        myRigid.angularDrag = 0f;
    }

    [System.Obsolete]
    void FixedUpdate()
    {
        Vector3 vel = myRigid.velocity;

        // ハマり防止（縦横完全にはまったとき）
        if (Mathf.Abs(vel.x) < 0.1f && Mathf.Abs(vel.z) > 0.9f)
        {
            vel.x = 0.3f * Mathf.Sign(Random.Range(-1f, 1f));
        }
        else if (Mathf.Abs(vel.z) < 0.1f && Mathf.Abs(vel.x) > 0.9f)
        {
            vel.z = 0.3f * Mathf.Sign(Random.Range(-1f, 1f));
        }

        // 💥 Z軸方向が浅すぎたらブースト（上下方向強調）
        float zLimit = 0.4f;
        if (Mathf.Abs(vel.z) < zLimit)
        {
            vel.z = zLimit * Mathf.Sign(vel.z != 0 ? vel.z : Random.Range(-1f, 1f));
        }

        // 💥 X軸方向も浅すぎたらブースト（左右方向強調）
        float xLimit = 0.6f;
        if (Mathf.Abs(vel.x) < xLimit)
        {
            vel.x = xLimit * Mathf.Sign(vel.x != 0 ? vel.x : Random.Range(-1f, 1f));
        }

        // 再正規化して速度統一
        myRigid.velocity = vel.normalized * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            Destroy(gameObject);
            myManager.GameOver();
        }
    }
}
