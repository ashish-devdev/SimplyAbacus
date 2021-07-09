using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSizeByChildren : MonoBehaviour
{
    GridLayoutGroup gridLayout;
    int totalChildren;
    int cellSize;
    void Start()
    {
        gridLayout = this.gameObject.GetComponent<GridLayoutGroup>();
    }


    void Update()
    {
        totalChildren = gameObject.transform.childCount;
        if (totalChildren > 0)
        {
            switch (totalChildren)
            {
                case 1:
                    {
                        cellSize = 320;
                        break;
                    }
                case 2:
                    {
                        cellSize = 270;
                        break;
                    }
                case 3:
                    {
                        cellSize = 250;
                        break;
                    }
                case 4:
                    {
                        cellSize = 250;
                        break;
                    }
                case 5:
                    {
                        cellSize = 180;
                        break;
                    }        
                case 6:
                    {
                        cellSize = 50;
                        break;
                    }
                case 7:
                    {
                        cellSize = 50;
                        break;
                    }
                case 8:
                    {
                        cellSize = 50;
                        break;
                    }
                default:
                    {
                        cellSize = 50;
                        break;
                    }

            }

        try
        {
            gridLayout.cellSize = new Vector2(cellSize, cellSize);
        }
        catch
        {
            ;
        }

        }

    }
}

