using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Block : MonoBehaviour
{
    [SerializeField] Sprite[] hitSprites;

    Level level;
    GameSession gameStatus;

    [SerializeField] int timesHit = 0;

    private void Start()
    {
        gameStatus = FindObjectOfType<GameSession>();
        CountBreakableBlocks();

    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable" || tag == "Penalty" || tag == "GameOver")
        {
            HandleBlockBreak();
        }
    }

    private void HandleBlockBreak()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestoryBlock();
        }
        else
        {
            ShowNextHitSprites();
        }
    }

    private void ShowNextHitSprites()
    {
        GetComponent<SpriteRenderer>().sprite = hitSprites[timesHit-1];
    }

    private void DestoryBlock()
    {
        Destroy(gameObject);
        if (tag == "Breakable")
        {
            level.BlockDestoyed();
            gameStatus.AddToScore();
        }
        if (tag == "Penalty")
        {
            gameStatus.PenaltyToScore();
        }
        if (tag == "GameOver")
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
