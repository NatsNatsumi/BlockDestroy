using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SEPlayer : MonoBehaviour
{
    public AudioSource bgmAudioSource;

    public AudioSource audioSource;
    public AudioClip clickSE;
    
    public void PlayClickSE()
    {
        StartCoroutine(PlaySE()); //←呼び出しはこっち
    }
    private IEnumerator PlaySE() //←戻り値がIEnumerator
    {
        Debug.Log("ボタンおされた");

        if (bgmAudioSource != null)
        {
            bgmAudioSource.Stop();
        }

        audioSource.PlayOneShot(clickSE);
        yield return new WaitForSeconds(3f);//音が終わるまで待つ
        SceneManager.LoadScene("game");        
    }
}
