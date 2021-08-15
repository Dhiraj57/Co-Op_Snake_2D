using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHandler : MonoBehaviour
{
    private enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    private enum State
    {
        Alive,
        Dead
    }

    private State state;
    private Vector2Int gridPosition;
    private Direction moveDirection;
    private float moveTimer;
    private float moveTimerMax;
    private FoodSpawner foodSpawner;
    private int snakeSize;
    private List<SnakeMovePosition> snakeMovePositionList;
    private List<SnakeBodyPart> snakeBodyPartList;

    public void Setup(FoodSpawner foodSpawner)
    {
        this.foodSpawner = foodSpawner;
    }

    private void Awake()
    {
        gridPosition = new Vector2Int(10, 10);
        moveTimerMax = 0.2f;
        moveTimer = moveTimerMax;
        moveDirection = Direction.Right;

        snakeMovePositionList = new List<SnakeMovePosition>();
        snakeBodyPartList = new List<SnakeBodyPart>();
        snakeSize = 0;

        state = State.Alive;
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

            gridPosition = foodSpawner.ValidateGridPosition(gridPosition);

            bool snakeAteFood = foodSpawner.EatFood(gridPosition);
            if (snakeAteFood)
            {
                snakeSize++;
                CreateSnakeBody();
            }
            
            if(snakeMovePositionList.Count >= snakeSize + 1)
            {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count -1);
            }

            foreach (SnakeBodyPart snakeBodyPart in snakeBodyPartList)
            {
                Vector2Int snakeBodyPartGridPosition =  snakeBodyPart.GetGridPosition();
                if(gridPosition == snakeBodyPartGridPosition)
                {
                    state = State.Dead;               
                }
            }

            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngle(moveDirectionVector) - 90);         

            UpdateSnakeBodyParts();    
        }   
    }

    private void CreateSnakeBody()
    {
        snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPartList.Count + 1));
    }

    private void UpdateSnakeBodyParts()
    {
        for (int i = 0; i < snakeBodyPartList.Count; i++)
        {
            //Vector3 snakeBodyPosition = new Vector3(snakeMovePositionList[i].x, snakeMovePositionList[i].y);
            snakeBodyPartList[i].SetSnakeMovePostion(snakeMovePositionList[i]);
        }
    }

    //public Vector2Int GetGridPosition()
    //{
     //   return gridPosition;
    //}

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

        //gridPositionList.AddRange(snakeMovePositionList);
        return gridPositionList;
    }

    //private void CreateSnakeBody()
   // {
     //   GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
      //  snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.Instance.SnakeBodySprite;
      //  snakeBodyTransformList.Add(snakeBodyGameObject.transform);
       // snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -snakeBodyTransformList.Count;
    //}

    private class SnakeBodyPart
    {
        private SnakeMovePosition movePosition;
        private Transform transform;

        public SnakeBodyPart(int bodyIndex)
        {
            GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.Instance.SnakeBodySprite;
            //snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
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

}
