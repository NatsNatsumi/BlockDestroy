using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private GameObject scoreText;
    public GameObject breakEffectPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        scoreText.GetComponent<ScoreManager>().score = scoreText.GetComponent<ScoreManager>().score + 300;
        DestroyBlock();
    }

    public void DestroyBlock()//250419追加_DestroyBlock
    {
        StartCoroutine(DestroyEffect());
    }

    IEnumerator DestroyEffect()//250419追加_DestroyBlock
    {
        // ちょっと縮ませる
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * 0.1f;

        float duration = 0.1f;
        float time = 0f;

        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        // パーティクル生成！
        if (breakEffectPrefab != null)
        {
            Instantiate(breakEffectPrefab, transform.position, Quaternion.identity);
        }

        // 最後にオブジェクト削除
        Destroy(gameObject);
    }
}
