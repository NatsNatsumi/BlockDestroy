using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private Text scoreText;
    public int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = GetComponentInChildren<Text>();
        scoreText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }
}
