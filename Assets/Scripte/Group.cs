using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class Group : MonoBehaviour
{
    float lastFall = 0;
    void Start()
    {
        // Default position not valid? Then it's game over
        if (!isValidGridPos())
        {
            Debug.Log("GAME OVER");
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (isValidGridPos())
                updateGrid();
            else
                transform.position += new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);
            if (isValidGridPos())
                updateGrid();
            else
                transform.Rotate(0, 0, 90);

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFall >= 1)
        {
            transform.position += new Vector3(0, -1, 0);
            if (isValidGridPos())
                updateGrid();
            else
            {
                transform.position += new Vector3(0, 1, 0);

                PlayerField.deleteFullRow();

                FindObjectOfType<Spawner>().spawnNext();

                //Desable Script
                enabled = false;
            }
            lastFall = Time.time;
        }
    }
    bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = PlayerField.roundVec2(child.position);
            if (!PlayerField.insideBorder(v))
            {
                return false;

            }

            if (PlayerField.grid[(int)v.x, (int)v.y] != null &&
                PlayerField.grid[(int)v.x, (int)v.y].parent != transform)
            //don't get this part? The function is really easy to understand. 
            //At first it loops through every child by using foreach, then it stores the child's rounded position in a variable. 
            //Afterwards it finds out if that position is inside the border, and then it finds out if there already is a block in the same grid entry or not.|
            {

                return false;
            }
        }
        return true;
    }
    void updateGrid()
    {
        // Remove old children from grid
        for (int y = 0; y < PlayerField.h; ++y)
        {
            for (int x = 0; x < PlayerField.w; ++x)
            {
                if (PlayerField.grid[x, y] != null)
                    if (PlayerField.grid[x, y].parent == transform)
                        PlayerField.grid[x, y] = null;
            }
        }
        //Adding the position
        foreach (Transform child in transform)
        {
            Vector2 v = PlayerField.roundVec2(child.position);
            PlayerField.grid[(int)v.x, (int)v.y] = child;
        }
    }
}
