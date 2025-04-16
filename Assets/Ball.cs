using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 8.0f;
    private Rigidbody myRigid;
    public GameManager myManager;

    [System.Obsolete]
    void Start()
{
    myRigid = GetComponent<Rigidbody>();

    // ランダムなX方向（左右に振る）を生成（例：-0.5〜0.5）
    float randomX = Random.Range(-0.3f, 0.7f);

    // Z方向は前方、X方向はランダムでブレさせる
    Vector3 dir = (transform.forward * 0.8f + transform.right * randomX).normalized;

    // 一定のスピードで飛ばす
    myRigid.velocity = dir * speed;

    // dragで減速しないように
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
