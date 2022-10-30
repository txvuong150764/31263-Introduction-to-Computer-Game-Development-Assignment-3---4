using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float movementSpeed = 6;
    public LevelGenerator levelGenerator;

    private enum MovementDirections {Up, Down, Left, Right};

    private MovementDirections lastInput = MovementDirections.Right;

    private MovementDirections currenntInput = MovementDirections.Right;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
        AnimateSprite();

        Debug.Log(lastInput + " " + currenntInput);

        if (CheckCanMove(lastInput))
        {
            Move();
        }
        else
        {
            if (CheckCanMove(currenntInput))
            {
                Move();
            }
            Vector3 tmp = transform.position;
            tmp.x = (float)Math.Round(transform.position.x);
            tmp.y = (float)Math.Round(transform.position.y);
            transform.position = tmp;
        }
    } // end Update()

    private void HandleMovementInput()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            if (CheckCanMove(MovementDirections.Right))
            {
                lastInput = MovementDirections.Right;
                currenntInput = lastInput;
            }
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            if (CheckCanMove(MovementDirections.Left))
            {
                lastInput = MovementDirections.Left;
                currenntInput = lastInput;
            }
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            if (CheckCanMove(MovementDirections.Up))
            {
                lastInput = MovementDirections.Up;
                currenntInput = lastInput;
            }
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            if (CheckCanMove(MovementDirections.Down))
            {
                Debug.Log("Here");
                lastInput = MovementDirections.Down;
                currenntInput= lastInput;
            }
        }
    }

    private void AnimateSprite()
    {
        switch (lastInput)
        {
            case MovementDirections.Right:
                spriteRenderer.flipY = false;
                transform.localEulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
                break;

            case MovementDirections.Up:
                spriteRenderer.flipY = false;
                transform.localEulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 90.0f);
                break;

            case MovementDirections.Left:
                spriteRenderer.flipY = true;
                transform.localEulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 180.0f);
                break;

            case MovementDirections.Down:
                spriteRenderer.flipY = false;
                transform.localEulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 270.0f);
                break;

            default:
                return;
        }
    }

    private bool CheckCanMove(MovementDirections direction)
    {
        int posX = (int)(transform.position.x);
        int posY = (int)(transform.position.y);

        //Debug.Log("PosX: " + posX + " " + "PosY: " + transform.position.y);

        if (direction == MovementDirections.Up)
        {
            if (levelGenerator.levelMap[13 - posY, posX + 13] == 1 || levelGenerator.levelMap[13 - posY, posX + 13] == 2 || levelGenerator.levelMap[13 - posY, posX + 13] == 3 || levelGenerator.levelMap[13 - posY, posX + 13] == 4 || levelGenerator.levelMap[13 - posY, posX + 13] == 7)
            {
                return false;
            }

        }
        else if (direction == MovementDirections.Down)
        {
            Debug.Log(levelGenerator.levelMap[14 - posY, posX + 13] + " " + posY + " " + posX);
            if (levelGenerator.levelMap[14 - posY, posX + 13] == 1 || levelGenerator.levelMap[14 - posY, posX + 13] == 2 || levelGenerator.levelMap[14 - posY, posX + 13] == 3 || levelGenerator.levelMap[14 - posY, posX + 13] == 4 || levelGenerator.levelMap[14 - posY, posX + 13] == 7)
            {
                return false;
            }
        }
        else if (direction == MovementDirections.Left)
        {
            if (levelGenerator.levelMap[14 - posY, posX + 12] == 1 || levelGenerator.levelMap[14 - posY, posX + 12] == 2 || levelGenerator.levelMap[14 - posY, posX + 12] == 3 || levelGenerator.levelMap[14 - posY, posX + 12] == 4 || levelGenerator.levelMap[14 - posY, posX + 12] == 7)
            {
                return false;
            }
        }
        else
        {          
            if (levelGenerator.levelMap[14 - posY, posX + 13] == 1 || levelGenerator.levelMap[14 - posY, posX + 13] == 2 || levelGenerator.levelMap[14 - posY, posX + 13] == 3 || levelGenerator.levelMap[14 - posY, posX + 13] == 4 || levelGenerator.levelMap[14 - posY, posX + 13] == 7)
            {
                return false;
            }
        }

        return true;
    }

    private void Move()
    {
        transform.Translate(new Vector3(movementSpeed, 0f, 0f) * Time.deltaTime, Space.Self);
    }
}
