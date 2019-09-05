using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float screenHeightInUnits = 12f;

    bool hasStarted;
    Vector2 currentVelocity;
    bool gamePaused;

    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;
    GameSession gameStatus;

    // Start is called before the first frame update
    void Start()
    {
        hasStarted = false;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        gameStatus = FindObjectOfType<GameSession>();
        currentVelocity = myRigidBody2D.velocity;
        gamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
            Physics2D.IgnoreLayerCollision(8, 9, true);
        }
        else if (hasStarted)
        {
            PauseBall();
            if (gamePaused)
            {
                Physics2D.IgnoreLayerCollision(8, 10, true);
            }
            else
            {
                Physics2D.IgnoreLayerCollision(8, 10, false);
                ReloadBall();
            }
            Physics2D.IgnoreLayerCollision(8, 9, false);
        }
    }

    private void PauseBall()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (gamePaused)
            {
                myRigidBody2D.velocity = currentVelocity;
                gamePaused = !gamePaused;
            }
            else
            {
                currentVelocity = myRigidBody2D.velocity;
                myRigidBody2D.velocity = new Vector2(0, 0);
                gamePaused = !gamePaused;
                gameStatus.PausePanelty();
            }
        }
    }

    private void ReloadBall()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            LockBallToPaddle();
            gameStatus.ReloadPanelty();
            hasStarted = false;
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
            hasStarted = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 lockPosition = new Vector2(GetXPos(), GetYPos());
        transform.position = lockPosition;
    }

    public bool HasPaused()
    {
        return gamePaused;
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
