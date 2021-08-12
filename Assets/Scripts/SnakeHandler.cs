using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHandler : MonoBehaviour
{
    private Vector2Int gridPosition;
    private Vector2Int moveDirection;
    private float moveTimer;
    private float moveTimerMax;
    private FoodSpawner foodSpawner;

    public void Setup(FoodSpawner foodSpawner)
    {
        this.foodSpawner = foodSpawner;
    }

    private void Awake()
    {
        gridPosition = new Vector2Int(10, 10);
        moveTimerMax = 0.3f;
        moveTimer = moveTimerMax;
        moveDirection = new Vector2Int(1, 0);
    }

    private void Update()
    {
        PlayerInput();
        PlayerMovement();
    }

    private void PlayerInput()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(moveDirection.y != -1)
            {
                moveDirection.x = 0;
                moveDirection.y = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (moveDirection.y != 1)
            {
                moveDirection.x = 0;
                moveDirection.y = -1;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (moveDirection.x != 1)
            {
                moveDirection.x = -1;
                moveDirection.y = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (moveDirection.x != -1)
            {
                moveDirection.x = 1;
                moveDirection.y = 0;
            }
        }
    }

    private void PlayerMovement()
    {
        moveTimer += Time.deltaTime;
        if(moveTimer >= moveTimerMax)
        {
            gridPosition += moveDirection;
            moveTimer -= moveTimerMax;
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngle(moveDirection) - 90);
            foodSpawner.EatFood(gridPosition);
        }   
    }

    public Vector2Int GetGridPosition()
    {
        return gridPosition;
    }

    private float GetAngle(Vector2Int dir)
    {
        float direction = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if(direction < 0)
        {
            direction += 360;
        }
        return direction;
    }

}
