using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayBoard : MonoBehaviour
{
    //Using this to make it more dynamic in future
    private int m_NumberOfRow = 5;

    [SerializeField] private GameObject m_SqarePrefab;
    [SerializeField] private GameObject m_Player;
    [SerializeField] private GameObject m_Block;
    [SerializeField] private GameObject m_Diamond;
    [SerializeField] private Transform m_ThisTransform;
    [SerializeField] private List<GameObject> m_Enemys;
    private List<List<Square>> m_Squares;

    private Player m_PlayerInstance;

    private void Start()
    {
        m_ThisTransform = transform;
        InfoPopup.CreateInfoPopup("To start with the game hit start button below", "START", SetupGameScreen);
    }

    #region GameSetup
    private void SetupGameScreen()
    {
        SetupSquares();
        SetupPlayer();
        SetupBlocks();
        SetupEnemy();
        SetupDiamonds();
    }

    private void SetupSquares()
    {
        float SquareSize = m_SqarePrefab.GetComponent<SpriteRenderer>().sprite.rect.width;

        float StartPosX = -SquareSize * Mathf.FloorToInt(m_NumberOfRow / 2f);
        float StartPosY = SquareSize * Mathf.FloorToInt(m_NumberOfRow / 2f);
        m_Squares = new List<List<Square>>();
        for (int i = 0; i < m_NumberOfRow; i++)
        {
            List<Square> tempSquare = new List<Square>();
            for (int j = 0; j < m_NumberOfRow; j++)
            {
                GameObject temp = Instantiate(m_SqarePrefab, m_ThisTransform, false);
                temp.transform.localPosition = new Vector3(StartPosX + SquareSize * j, StartPosY - SquareSize * i, 0f);
                tempSquare.Add(temp.GetComponent<Square>());
            }
            m_Squares.Add(tempSquare);
        }
        m_Squares[0][0].SetAsFirstSquare();
        m_Squares[m_NumberOfRow - 1][m_NumberOfRow - 1].SetAsLastSquare();
        //Connecting All Squares
        for (int i = 0; i < m_Squares.Count; i++)
        {
            for (int j = 0; j < m_Squares[i].Count; j++)
            {
                m_Squares[i][j]._downSquare = i + 1 < m_Squares.Count ? m_Squares[i + 1][j]:null;
                m_Squares[i][j]._upSquare = i - 1 >= 0 ? m_Squares[i - 1][j] : null;
                m_Squares[i][j]._leftSquare = j - 1 >= 0 ? m_Squares[i][j - 1] : null;
                m_Squares[i][j]._rightSquare = j + 1 < m_Squares[i].Count ? m_Squares[i][j + 1] : null;
            }
        }
    }

    private void SetupPlayer()
    {
        Transform squareTransform = m_Squares[0][0].transform;
        GameObject player = Instantiate(m_Player, squareTransform, false);
        player.transform.localPosition = new Vector3(0f, 0f, 0f);
        player.GetComponent<Player>().CurrentSquare = m_Squares[0][0];
        m_Squares[0][0]._hasPlayer = true;
        m_PlayerInstance = player.GetComponent<Player>();
    }

    private void SetupBlocks()
    {
        int PrevIndex = m_NumberOfRow;
        for (int i = 0; i < m_NumberOfRow; i++)
        {
            int Index = PrevIndex > 1 ? Random.Range(0, PrevIndex - 1) : Random.Range(PrevIndex + 2, m_NumberOfRow);
            if (m_Squares[i][Index].IsStart|| m_Squares[i][Index].IsEnd)
            {
                Index = PrevIndex > 1 ? Random.Range(1, PrevIndex - 1) : Random.Range(PrevIndex, m_NumberOfRow - 2);
            }
            Transform squareTransform = m_Squares[i][Index].transform;
            Instantiate(m_Block, squareTransform, false).transform.localPosition = new Vector3(0f, 0f, 0f);
            m_Squares[i][Index].SetAsBlocked();
            PrevIndex = Index;
        }
    }

    private void SetupEnemy()
    {
        int row = m_NumberOfRow - m_Enemys.Count;
        foreach(GameObject enemy in m_Enemys)
        {
            int column = Random.Range(1, m_NumberOfRow);
            if(m_Squares[row][column].IsBlocked)
            {
                int newColumn = column + 1 < m_NumberOfRow ? column + 1 : column - 1;
                GameObject enemyTemp = Instantiate(enemy, m_Squares[row][newColumn].transform, false);
                enemyTemp.GetComponent<Enemy>()._currentPlayer = m_PlayerInstance;
                enemyTemp.GetComponent<Enemy>()._currentSquare = m_Squares[row][newColumn];
            }
            else
            {
                GameObject enemyTemp = Instantiate(enemy, m_Squares[row][column].transform, false);
                enemyTemp.GetComponent<Enemy>()._currentPlayer = m_PlayerInstance;
                enemyTemp.GetComponent<Enemy>()._currentSquare = m_Squares[row][column];
            }
            row++;
        }
    }

    private void SetupDiamonds()
    {
        for (int i = 0; i < m_Squares.Count; i++)
        {
            for (int j = 0; j < m_Squares[i].Count; j++)
            {
                Square square = m_Squares[i][j];
                if (!square.IsBlocked && !square.IsStart && !square.IsEnd && !square._hasEnemy)
                {
                    square._diamond = Instantiate(m_Diamond, square.transform, false);
                    square._diamond.transform.localPosition = new Vector3(0f, 0f, 0f);
                }
            }
        }
    }
    #endregion
}
