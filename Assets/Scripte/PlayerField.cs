using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerField : MonoBehaviour
{
    // The Grid itself
    public static int w = 10;
    public static int h = 20;

    public static Transform[,] grid = new Transform[w, h];
    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x),
                          Mathf.Round(v.y));
    }
    public static bool insideBorder(Vector2 pos)
    {
        return (
            (int)pos.x >= 0 &&
            (int)pos.x < w &&
            (int)pos.y >= 0);
    }
    public static void deleteRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }
    public static void decreaseRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] != null)
            {
                //moving down to y axic by 1unit
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                //updating the game
                grid[x, y - 1].position += new Vector3(0, -1, 0);

            }
        }
    }
    public static void decreaseRowAbove(int y)
    {
        for (int i = y; i < h; ++i)
        {
            decreaseRow(i);
        }
    }
    public static bool isRowFull(int y)
    {
        for (int x = 0; x < w; ++x)
            if (grid[x, y] == null)
                return false;
        return true;
    }
    public static void deleteFullRow()
    {
        for (int y = 0; y < h; ++y)
        {
            if (isRowFull(y))
            {
                deleteRow(y);
                decreaseRowAbove(y + 1);
                --y;
            }
        }
    }

}
