using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float screenHeightInUnits = 12f;
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;
    [SerializeField] float minY = 1f;
    [SerializeField] float maxY = 11f;

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(transform.position.x, minX, maxX);
        paddlePos.y = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = paddlePos;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
