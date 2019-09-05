using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GamePLay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI blockText;
    [SerializeField] GameObject blocksObject;
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float screenHeightInUnits = 12f;
    [SerializeField] int maxBlocks = 5;

    Ball myBall;

    private int blocks;
    

    // Start is called before the first frame update
    void Start()
    {
        blocks = maxBlocks;
        blockText.text = "Blocks: " + blocks;
        myBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        GenerateBlocks();
    }

    private void GenerateBlocks()
    {
        if (myBall.HasPaused())
        {
            blockText.text = "PAUSED Blocks: " + blocks;
            MakeBlocks();
            RotateBlocks();
            ResetBlocks();
        }
        else
        {
            blockText.text = "Blocks: " + blocks;
        }
    }

    private void RotateBlocks()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Paddle[] paddles = FindObjectsOfType<Paddle>();
            foreach (Paddle paddle in paddles)
            {
                paddle.transform.Rotate(Vector3.forward * 2);
            }
        }
    }

    private void MakeBlocks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (blocks > 0)
            {
                Vector3 paddleLocation = new Vector3(GetXPos(), GetYPos());
                Instantiate(blocksObject, paddleLocation, transform.rotation);
                blocks--;
            }
        }
    }

    private void ResetBlocks()
    {
        if (Input.GetMouseButtonDown(2))
        {
            Paddle[] paddles = FindObjectsOfType<Paddle>();
            foreach (Paddle paddle in paddles)
            {
                paddle.DestroySelf();
            }
            blocks = maxBlocks;
        }
    }

    private float GetYPos()
    {
        return Input.mousePosition.y / Screen.height * screenHeightInUnits;
    }

    private float GetXPos()
    {
        return Input.mousePosition.x / Screen.width * screenWidthInUnits;
    }
}
