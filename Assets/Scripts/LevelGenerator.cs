using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject outsideCorner;    //1
    public GameObject outsideWall;      //2
    public GameObject insideCorner;     //3
    public GameObject insideWall;       //4
    public GameObject standardPellet;   //5
    public GameObject powerPellet;      //6
    public GameObject tJunction;        //7

    public GameObject pacStudent;
    public GameObject ghost_1;
    public GameObject ghost_2;
    public GameObject ghost_3;
    public GameObject ghost_4;
    public GameObject topLeftMaze;
    public GameObject grid;

    int[,] levelMap =
    {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };
    // Start is called before the first frame update
    void Start()
    {   
        //Clear manual level
        grid.SetActive(false);

        //PacStudent Position
        pacStudent.transform.position = new Vector2(-levelMap.GetLength(1) + 2, levelMap.GetLength(0) - 2);

        //Ghost Position
        ghost_1.transform.position = new Vector2(2f, 1f);
        ghost_2.transform.position = new Vector2(-2f, 1f);
        ghost_3.transform.position = new Vector2(2f, -1f);
        ghost_4.transform.position = new Vector2(-2f, -1f);

        //Array to store wall direction
        string[,] dir = new string[levelMap.GetLength(0), levelMap.GetLength(1)];

        for (int i = 0; i < levelMap.GetLength(0); i++)
        {
            for (int j = 0; j < levelMap.GetLength(1); j++)
            {
                dir[i, j] = "N";
            }
        }

        dir[0, 0] = "0";

        //Create maze
        for (int i = 0; i < levelMap.GetLength(0); i++)
        {
            for (int j = 0; j < levelMap.GetLength(1); j++)
            {            
                int spriteNum = levelMap[i,j];
                if (spriteNum != 0)
                {
                    GameObject sprite = GameObject.FindWithTag(spriteNum + "");
                    GameObject rotateSprite = Instantiate(sprite, new Vector2(j - levelMap.GetLength(1) + 1, -i + levelMap.GetLength(0) - 1), Quaternion.identity);

                    rotateSprite.transform.parent = topLeftMaze.transform;
                    if (i == 0 && j == 0)
                        continue;

                    if (spriteNum == 1 || spriteNum == 3)
                    {
                        if (j >= 1)
                        {
                            if (dir[i, j - 1] == "N" || dir[i, j - 1] == "V" || dir[i, j - 1] == "180" || dir[i, j - 1] == "270")
                            {
                                if (dir[i - 1, j] == "0" || dir[i - 1, j] == "270" || dir[i - 1, j] == "V")
                                {
                                    rotateSprite.transform.Rotate(0, 0, 90);
                                    dir[i, j] = "90";
                                }
                                else
                                    dir[i, j] = "0";
                            }
                            else
                            {
                                if (dir[i - 1, j] == "0" || dir[i - 1, j] == "270" || dir[i - 1, j] == "V")
                                {
                                    rotateSprite.transform.Rotate(0, 0, 180);
                                    dir[i, j] = "180";
                                }
                                else
                                {
                                    rotateSprite.transform.Rotate(0, 0, 270);
                                    dir[i, j] = "270";
                                }                                
                            }
                        }
                        else
                        {
                            if (dir[i - 1, j] == "V")
                            {
                                rotateSprite.transform.Rotate(0, 0, 90);
                                dir[i, j] = "90";
                            }
                        }
                    }
                    
                    if (spriteNum == 2 || spriteNum == 4)
                    {
                        if (i >= 1)
                        {
                            if (dir[i - 1, j] == "0" || dir[i - 1, j] == "270" || dir[i - 1, j] == "V" || dir[i - 1, j] == "T")
                            {
                                rotateSprite.transform.Rotate(0, 0, 90);
                                dir[i, j] = "V";
                            }
                            else
                                dir[i, j] = "H";
                        }
                    }

                    if (spriteNum == 7)
                        dir[i, j] = "T";
                }
            }
        }

        // Deactive all LevelSprites game objects in LevelManager after instantiating 
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        //Debug.Log(gameObject.transform.childCount);

        // Instantiate the rest of the grids. Place within the LevelManager as a child. 
        //Top-Right
        GameObject.Instantiate(gameObject.transform.GetChild(1), new Vector2(-topLeftMaze.transform.position.x + 1, topLeftMaze.transform.position.y), Quaternion.Euler(0, 180, 0), gameObject.transform.GetChild(1).parent);
        //Bottem-Right
        GameObject.Instantiate(gameObject.transform.GetChild(1), new Vector2(-topLeftMaze.transform.position.x + 1, -topLeftMaze.transform.position.y), Quaternion.Euler(-180, 180, 0), gameObject.transform.GetChild(1).parent);
        //Bottem-Left
        GameObject.Instantiate(gameObject.transform.GetChild(1), new Vector2(topLeftMaze.transform.position.x, -topLeftMaze.transform.position.y), Quaternion.Euler(-180, 0, 0), gameObject.transform.GetChild(1).parent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
