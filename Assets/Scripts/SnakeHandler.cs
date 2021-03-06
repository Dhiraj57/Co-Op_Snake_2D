using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum State
{
    Alive,
    Dead
}

public class SnakeHandler : MonoBehaviour
{
    private enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    public State state;

    private Vector2Int gridPosition;
    private Direction moveDirection;

    private float moveTimer;
    [HideInInspector] public float moveTimerMax;
   
    private int snakeSize;
    private int width;
    private int height;

    [HideInInspector]public bool deadFood = false;
    [HideInInspector] public bool shield = false;

    [SerializeField] private FoodSpawner foodSpawner;
    [SerializeField] private PowerUPHandler powerUp;
    [SerializeField] private GameOverWindow gameOver;

    private List<SnakeMovePosition> snakeMovePositionList;
    private List<SnakeBodyPart> snakeBodyPartList;

    private void Awake()
    {
        Time.timeScale = 1f;

        width = 20;
        height = 20;

        gridPosition = new Vector2Int(10, 10);
        
        moveTimerMax = 0.2f;
        moveTimer = moveTimerMax;
        moveDirection = Direction.Right;

        snakeMovePositionList = new List<SnakeMovePosition>();
        snakeBodyPartList = new List<SnakeBodyPart>();
        snakeSize = 0;
        
        state = State.Alive;

        gameOver.DisableUI();
    }

    private void Update()
    {
        switch(state)
        {
            case State.Alive:
                PlayerInput();
                PlayerMovement();
                break;

            case State.Dead:
                break;
        }
    }

    public void ResetPlayer()
    {
        Awake();
    }

    private void PlayerInput()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(moveDirection != Direction.Down)
            {
                moveDirection = Direction.Up;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (moveDirection != Direction.Up)
            {
                moveDirection = Direction.Down;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (moveDirection != Direction.Right)
            {
                moveDirection = Direction.Left;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (moveDirection != Direction.Left)
            {
                moveDirection = Direction.Right;
            }
        }
    }

    private void PlayerMovement()
    {
        moveTimer += Time.deltaTime;
        if(moveTimer >= moveTimerMax)
        {      
            moveTimer -= moveTimerMax;

            SnakeMovePosition previousSnakeMovePosition = null;
            if(snakeMovePositionList.Count > 0)
            {
                previousSnakeMovePosition = snakeMovePositionList[0];
            }

            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition, gridPosition, moveDirection);
            snakeMovePositionList.Insert(0, snakeMovePosition);

            Vector2Int moveDirectionVector;
            switch (moveDirection)
            {
                default:
                case Direction.Right: moveDirectionVector = new Vector2Int(1, 0); break;
                case Direction.Left: moveDirectionVector = new Vector2Int(-1, 0); break;
                case Direction.Up: moveDirectionVector = new Vector2Int(0, 1); break;
                case Direction.Down: moveDirectionVector = new Vector2Int(0, -1); break;
            }

            gridPosition += moveDirectionVector;
            gridPosition = ValidateGridPosition(gridPosition);

            bool snakeAteFood = foodSpawner.EatFood(gridPosition);
            if (snakeAteFood)
            {
                SoundManager.Instance.Play(SoundManager.Sounds.Pickup);
                if (deadFood)
                {  
                    if(snakeSize > 0)
                    {
                        RemoveSnakeBody();
                        snakeSize--;
                    }
                    
                }
                else
                {
                    snakeSize++;
                    CreateSnakeBody();
                }          
            }

            bool snakePowerUp = powerUp.SnakePowerUp(gridPosition);
            
            if(snakeMovePositionList.Count >= snakeSize + 1)
            {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count -1);
            }

            foreach (SnakeBodyPart snakeBodyPart in snakeBodyPartList)
            {
                Vector2Int snakeBodyPartGridPosition =  snakeBodyPart.GetGridPosition();
                if(gridPosition == snakeBodyPartGridPosition)
                {
                    if(!shield)
                    {
                        //SoundManager.Instance.Play(SoundManager.Sounds.PlayerDeath);
                        state = State.Dead;
                        gameOver.GameOver();
                    }                   
                }
            }

            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngle(moveDirectionVector) - 90);         

            UpdateSnakeBodyParts();    
        }   
    }

    public Vector2Int ValidateGridPosition(Vector2Int gridPosition)
    {
        if (gridPosition.x < 0)
        {
            gridPosition.x = width ;
        }
        if (gridPosition.x > width )
        {
            gridPosition.x = 0;
        }
        if (gridPosition.y < 0)
        {
            gridPosition.y = height ;
        }
        if (gridPosition.y > height )
        {
            gridPosition.y = 0;
        }
        return gridPosition;
    }

    private void CreateSnakeBody()
    {
        snakeBodyPartList.Add(new SnakeBodyPart());
    }

    private void RemoveSnakeBody()
    {
        Destroy(snakeBodyPartList[snakeBodyPartList.Count - 1].snakeBodyGameObject);
        snakeBodyPartList.RemoveAt(snakeBodyPartList.Count - 1);
    }

    private void UpdateSnakeBodyParts()
    {
        for (int i = 0; i < snakeBodyPartList.Count; i++)
        {
            snakeBodyPartList[i].SetSnakeMovePostion(snakeMovePositionList[i]);
        }
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

    public List<Vector2Int> GetSnakeGridPositionList()
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition };
        foreach(SnakeMovePosition snakeMovePosition in snakeMovePositionList)
        {
            gridPositionList.Add(snakeMovePosition.GetGridPosition());
        }

        return gridPositionList;
    }

    private class SnakeBodyPart
    {
        private SnakeMovePosition movePosition;
        private Transform transform;
        public GameObject snakeBodyGameObject;

        public SnakeBodyPart()
        {
            snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.Instance.SnakeBodySprite;
            transform = snakeBodyGameObject.transform;
        }

        public void SetSnakeMovePostion(SnakeMovePosition snakeMovePosition)
        {
            this.movePosition = snakeMovePosition ;
            transform.position = new Vector3(movePosition.GetGridPosition().x , movePosition.GetGridPosition().y );

            float angle;
            switch (snakeMovePosition.GetDirection())
            {
                default:
                case Direction.Up:
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 0; break;
                        case Direction.Left:
                            angle = 0 + 45; break;
                        case Direction.Right:
                            angle = 0 - 45; break;
                    }
                    break;

                case Direction.Down:
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 180; break;
                        case Direction.Left:
                            angle = 180 + 45; break;
                        case Direction.Right:
                            angle = 180 - 45; break;
                    }
                    break;

                case Direction.Left:
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = -90; break;
                        case Direction.Down:
                            angle = -45; break;
                        case Direction.Up:
                            angle = 45; break;
                    }
                    break;

                case Direction.Right:
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 90; break;
                        case Direction.Down:
                            angle = 45; break;
                        case Direction.Up:
                            angle = -45; break;
                    }
                    break;
            }

            transform.eulerAngles = new Vector3(0, 0, angle);
        }

        public Vector2Int GetGridPosition()
        {
            if(movePosition != null)
            {
                return movePosition.GetGridPosition();
            }
           
            else return new Vector2Int(0, 0);      
        }

    }

    private class SnakeMovePosition
    {
        private SnakeMovePosition previouSnakeMovePosition;
        private Vector2Int gridPosition;
        private Direction direction;

        public SnakeMovePosition(SnakeMovePosition previouSnakeMovePosition, Vector2Int grid, Direction dir)
        {
            this.previouSnakeMovePosition = previouSnakeMovePosition;
            gridPosition = grid;
            direction = dir;
        }

        public Vector2Int GetGridPosition()
        {
            return gridPosition;
        }

        public Direction GetDirection()
        {
            return direction;
        }

        public Direction GetPreviousDirection()
        {
            if (previouSnakeMovePosition == null) return Direction.Right;
            else return previouSnakeMovePosition.direction;
        }
    }

    public int GetSnakeSize()
    {
        return snakeBodyPartList.Count;
    }

}
