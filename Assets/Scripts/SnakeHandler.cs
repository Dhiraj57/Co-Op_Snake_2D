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
    private int snakeSize;
    private List<Vector2Int> snakeMovePositionList;
    private List<SnakeBodyPart> snakeBodyPartList;

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

        snakeMovePositionList = new List<Vector2Int>();
        snakeBodyPartList = new List<SnakeBodyPart>();
        snakeSize = 0;
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
            moveTimer -= moveTimerMax;

            snakeMovePositionList.Insert(0, gridPosition);
            gridPosition += moveDirection;

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

            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngle(moveDirection) - 90);         

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
            Vector3 snakeBodyPosition = new Vector3(snakeMovePositionList[i].x, snakeMovePositionList[i].y);
            snakeBodyPartList[i].SetGridPostion(snakeMovePositionList[i]);
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

        gridPositionList.AddRange(snakeMovePositionList);
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
        private Vector2Int gridPosition;
        private Transform transform;

        public SnakeBodyPart(int bodyIndex)
        {
            GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.Instance.SnakeBodySprite;
            //snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
            transform = snakeBodyGameObject.transform;
        }

        public void SetGridPostion(Vector2Int grid)
        {
            gridPosition = grid ;
            transform.position = new Vector3(gridPosition.x , gridPosition.y );
        }

    }

}
