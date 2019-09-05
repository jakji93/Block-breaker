using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlock = 5;
    [SerializeField] int reloadPenalty = 10;
    [SerializeField] int pausePenalty = 2;
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] int score = 0;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        scoreText.text = score.ToString();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = gameSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale = gameSpeed * 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Time.timeScale = gameSpeed * 3;
        }
    }

    public void AddToScore()
    {
        score += pointsPerBlock;
        scoreText.text = score.ToString();
    }

    public void PenaltyToScore()
    {
        score -= pointsPerBlock;
        scoreText.text = score.ToString();
    }

    public void ReloadPanelty()
    {
        score -= reloadPenalty;
        scoreText.text = score.ToString();
    }

    public void PausePanelty()
    {
        score -= pausePenalty;
        scoreText.text = score.ToString();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
