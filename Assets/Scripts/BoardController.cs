using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardController : MonoBehaviour
{

    [SerializeField]
    public BoardConfig boardConfig;

    private int boardLength = 21;
    private int boardHeight = 15;
    private int boardArea;

    [SerializeField]
    private GameObject breakableTilesParent;
    [SerializeField]
    private GameObject gameOverCanvas;

    private BreakableTile[,] breakableTilesMatrix;

    private bool flag = false;

    [SerializeField]
    private GameObject flagWarn;

    void Start()
    {

        breakableTilesMatrix = new BreakableTile[boardLength, boardHeight];
        InitBoard();
    }

    private void Update()
    {

        BreakableTile clickedTile = BoardInput.hasClickedTile();
        if (!flag)
        {
            if (clickedTile != null)
            {
                if (clickedTile.GetId() == -1)
                {

                    BreakAllTiles();
                    GameOver();
                }
                else
                {

                    clickedTile.Break();
                }
            }
        }
        else
        {
            if (clickedTile != null)
            {

                SetFlag(clickedTile);
            }
        }
        
    }

    private void InitBoard()
    {

        boardArea = boardLength * boardHeight;

        //getting breakable tiles from the board
        int count = 0;
        foreach (Transform breakableTile in breakableTilesParent.transform)
        {

            int y = count / (boardLength);
            int x = count - boardLength * y;
            breakableTilesMatrix[x, y] = breakableTile.GetComponent<BreakableTile>();
            breakableTilesMatrix[x, y].SetX(x);
            breakableTilesMatrix[x, y].SetY(y);
            count++;
        }

        SpawnBombs();
        SpawnNumbers();
    }

    public void SpawnBombs()
    {

        double random;
        foreach (BreakableTile breakableTile in breakableTilesMatrix)
        {

            double p = boardConfig.bombProbability;
            random = Random.Range(0.0f, 1.0f);

            if (random < p)
            {

                breakableTile.SetId(-1);
                breakableTile.SetUnderneathImage(boardConfig.bomb);
            }
        }
    }


    private bool OutOfBounds(int x, int y)
    {

        if(x < 0 || y < 0 || x >= boardLength || y >= boardHeight)
        {

            return true;
        }
        else
        {

            return false;
        }
    }

    private int CountBombsAround(int x, int y)
    {

        int count = 0;

        if (!OutOfBounds(x + 1, y) && breakableTilesMatrix[x + 1, y].GetId() == -1)
        {

            count++;
        }
        if (!OutOfBounds(x - 1, y) && breakableTilesMatrix[x - 1, y].GetId() == -1)
        {

            count++;
        }
        if (!OutOfBounds(x, y + 1) && breakableTilesMatrix[x, y + 1].GetId() == -1)
        {

            count++;
        }
        if (!OutOfBounds(x, y - 1) && breakableTilesMatrix[x, y - 1].GetId() == -1)
        {

            count++;
        }
        if (!OutOfBounds(x - 1, y + 1) && breakableTilesMatrix[x - 1, y + 1].GetId() == -1)
        {

            count++;
        }
        if (!OutOfBounds(x + 1, y + 1) && breakableTilesMatrix[x + 1, y + 1].GetId() == -1)
        {

            count++;
        }
        if (!OutOfBounds(x - 1, y - 1) && breakableTilesMatrix[x - 1, y - 1].GetId() == -1)
        {

            count++;
        }
        if (!OutOfBounds(x + 1, y - 1) && breakableTilesMatrix[x + 1, y - 1].GetId() == -1)
        {

            count++;
        }

        return count;

    }

    private void SpawnNumbers()
    {

        for(int y = 0; y < boardHeight; y++)
        {

            for(int x = 0; x < boardLength; x++)
            {

                if (breakableTilesMatrix[x, y].GetId() != -1)
                {

                    int nBombs = CountBombsAround(x, y);
                    Sprite image = boardConfig.numbersImages[nBombs];
                    breakableTilesMatrix[x, y].SetId(nBombs);
                    breakableTilesMatrix[x, y].SetUnderneathImage(image);
                }
            }
        }
    }

 /*   public void BreakTiles(int x, int y)
    {

        Queue<BreakableTile> adjs = new Queue<BreakableTile>();
        adjs.Enqueue(breakableTilesMatrix[x, y]);

        while (adjs.Count > 0)
        {

            BreakableTile curBreakableTile = adjs.Dequeue();

            if (!OutOfBounds(curBreakableTile.GetX(), curBreakableTile.GetY()) && 
                breakableTilesMatrix[curBreakableTile.GetX(), curBreakableTile.GetY()].GetId() == 0)
            {

                curBreakableTile.Break();
                adjs.Enqueue(breakableTilesMatrix[x + 1, y]);
                adjs.Enqueue(breakableTilesMatrix[x - 1, y]);
                adjs.Enqueue(breakableTilesMatrix[x, y + 1]);
                adjs.Enqueue(breakableTilesMatrix[x, y - 1]);
            }
        }

    }*/

    public void PlayAgain()
    {

        gameOverCanvas.SetActive(false);

        foreach (BreakableTile breakableTile in breakableTilesMatrix)
        {

            breakableTile.Reset();
            breakableTile.SetCurrentImage(boardConfig.defaultImage);
        }

        InitBoard();
    }

    public void Flag()
    {

        flag = !flag;
        if (flag)
        {

            flagWarn.SetActive(true);
        }
        else
        {

            flagWarn.SetActive(false);
        }
    }

    public void SetFlag(BreakableTile breakableTile)
    {

        if (!breakableTile.GetFlaged())
        {

            breakableTile.SetFlaged(true);
            breakableTile.SetCurrentImage(boardConfig.flag);
        }
        else
        {

            breakableTile.SetFlaged(false);
            breakableTile.SetCurrentImage(boardConfig.defaultImage);
        }
    }

    public void BreakAllTiles()
    {

        foreach(BreakableTile breakableTile in breakableTilesMatrix)
        {
            breakableTile.Break();
        }
    }

    private void GameOver()
    {

        gameOverCanvas.SetActive(true);
    }
}
