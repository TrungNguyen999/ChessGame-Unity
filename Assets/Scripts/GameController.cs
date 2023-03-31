using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject chessPiece;
    // Map positions
    private readonly GameObject[,] positionOnBoard = new GameObject[8,8];
    // player chess pieces amount
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    [SerializeField] private TMP_Text whiteSideText;
    [SerializeField] private TMP_Text blackSideText;
    [SerializeField] GameObject RestartBtn;

    private string currPlayer = "white";
    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
        //// Using Instantiate() to create a gameObject when start the game 
        //Instantiate(chessPiece, new Vector3(0,0,-1), Quaternion.identity);
        playerWhite = new GameObject[]
        {
            Create("white_rook", 0, 0), Create("white_knight", 1, 0), Create("white_bishop", 2, 0), Create("white_queen", 3, 0), Create("white_king", 4, 0),
            Create("white_rook", 7, 0), Create("white_knight", 6, 0), Create("white_bishop", 5, 0), Create("white_pawn", 0, 1),
            Create("white_pawn", 1, 1), Create("white_pawn", 2, 1), Create("white_pawn", 3, 1), Create("white_pawn", 4, 1), Create("white_pawn", 5, 1),
            Create("white_pawn", 6, 1), Create("white_pawn", 7, 1)
        };

        playerBlack = new GameObject[]
        {
            Create("black_rook", 0, 7), Create("black_knight", 1, 7), Create("black_bishop", 2, 7), Create("black_queen", 3, 7), Create("black_king", 4, 7),
            Create("black_rook", 7, 7), Create("black_knight", 6, 7), Create("black_bishop", 5, 7), Create("black_pawn", 0, 6),
            Create("black_pawn", 1, 6), Create("black_pawn", 2, 6), Create("black_pawn", 3, 6), Create("black_pawn", 4, 6), Create("black_pawn", 5, 6),
            Create("black_pawn", 6, 6), Create("black_pawn", 7, 6)
        };

        // Set all piece position to the corresponding board position
        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }
    }

    public void SetPosition(GameObject obj)
    {
        ChessPieces cp = obj.GetComponent<ChessPieces>();
        positionOnBoard[(int)cp.GetXBoard(), (int)cp.GetYBoard()] = obj;
    }

    private GameObject Create(string spriteName, int xCoord, int yCoord)
    {
        GameObject obj = Instantiate(chessPiece, new Vector3(xCoord, yCoord, -1), Quaternion.identity);
        ChessPieces cp = obj.GetComponent<ChessPieces>();
        cp.name = spriteName;
        cp.SetXBoard(xCoord);
        cp.SetYBoard(yCoord);
        cp.Activate();
        return obj;
    }
    // Set the position of the piece after move is null
    public void SetPositionEmpty(int x, int y)
    {
        positionOnBoard[x, y] = null;
    }

    // Get the position base of the board
    public GameObject GetPosition(int x, int y)
    {
        return positionOnBoard[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positionOnBoard.GetLength(0) || y >= positionOnBoard.GetLength(1))
        {
            return false;
        }
        return true;
    }

    public string GetCurrentPlayer()
    {
        return currPlayer;
    }

    public void NextTurn()
    {
        if (currPlayer == "white")
        {
            currPlayer = "black";
        }
        else
        {
            currPlayer = "white";
        }
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    private void Update()
    {
        if (IsGameOver() && Input.GetMouseButtonDown(0))
        {
            isGameOver = false;
            SceneManager.LoadScene("Game");
        }
    }

    public void Winner(string playerWin, string playerLose)
    {
        isGameOver = true;
        string winMessage = $"{playerWin} is the winner!!!";
        string loseMessage = $"{playerLose} is the loser!!!";


        whiteSideText.enabled = true;
        blackSideText.enabled = true;

        if (playerWin == "white")
        {
            whiteSideText.text = winMessage;
            blackSideText.text = loseMessage;
        }
        if (playerWin == "black")
        {
            whiteSideText.text = loseMessage;
            blackSideText.text = winMessage;
        }
        RestartBtn.SetActive(true);
    }
}
