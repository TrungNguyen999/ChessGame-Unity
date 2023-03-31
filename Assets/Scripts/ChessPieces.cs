using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieces : MonoBehaviour
{
    [SerializeField] private GameObject controller;
    [SerializeField] private GameObject MovePlate;

    // Positions on chess board
    private float xBoard = -1;
    private float yBoard = -1;
    // does the player is "black" or "white"?
    private string playerType;

    // References chess pieces
    [SerializeField] private Sprite queen_blck, knight_blck, bishop_blck, king_blck, rook_blck, pawn_blck;
    [SerializeField] private Sprite queen_wht, knight_wht, bishop_wht, king_wht, rook_wht, pawn_wht;
    // Start is called before the first frame update

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        // take the init location and adjust it
        SetCoords();

        switch (this.name)
        {
            case "black_queen":
                this.GetComponent<SpriteRenderer>().sprite = queen_blck; playerType = "black"; break;
            case "black_knight":
                this.GetComponent<SpriteRenderer>().sprite = knight_blck; playerType = "black"; break;
            case "black_bishop":
                this.GetComponent<SpriteRenderer>().sprite = bishop_blck; playerType = "black"; break;
            case "black_king":
                this.GetComponent<SpriteRenderer>().sprite = king_blck; playerType = "black"; break;
            case "black_rook":
                this.GetComponent<SpriteRenderer>().sprite = rook_blck; playerType = "black"; break;
            case "black_pawn":
                this.GetComponent<SpriteRenderer>().sprite = pawn_blck; playerType = "black"; break;

            case "white_queen":
                this.GetComponent<SpriteRenderer>().sprite = queen_wht; playerType = "white"; break;
            case "white_knight":
                this.GetComponent<SpriteRenderer>().sprite = knight_wht; playerType = "white"; break;
            case "white_bishop":
                this.GetComponent<SpriteRenderer>().sprite = bishop_wht; playerType = "white"; break;
            case "white_king":
                this.GetComponent<SpriteRenderer>().sprite = king_wht; playerType = "white"; break;
            case "white_rook":
                this.GetComponent<SpriteRenderer>().sprite = rook_wht; playerType = "white"; break;
            case "white_pawn":
                this.GetComponent<SpriteRenderer>().sprite = pawn_wht; playerType = "white"; break;
        }
    }
    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        // WTH are these numbers??? :v
        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        this.transform.position = new Vector3(x, y, -1.0f);
    }

    // Getter & Setter of the x and y coordinates
    public float GetXBoard()
    {
        return xBoard;
    }

    public float GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }
    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    private void OnMouseUp()
    {
        if (!controller.GetComponent<GameController>().IsGameOver() && controller.GetComponent<GameController>().GetCurrentPlayer() == playerType)
        {
            DestroyedMovePlate();
            InitMovePlate();
        }
    }

    public void DestroyedMovePlate()
    {
        GameObject[] MovePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < MovePlates.Length; i++)
        {
            Destroy(MovePlates[i]);
        }
    }
    void InitMovePlate()
    {
        switch (this.name)
        {
            case "black_queen":
            case "white_queen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "black_knight":
            case "white_knight":
                LMovePlate();
                break;
            case "black_bishop":
            case "white_bishop":
                LineMovePlate(1, 1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "black_rook":
            case "white_rook":
                LineMovePlate(0, 1);
                LineMovePlate(1, 0);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "black_king":
            case "white_king":
                MoveSurroundPlate();
                break;
            case "black_pawn":
                if (yBoard == 6)
                {
                    PawnMovePlate(xBoard, yBoard - 1);
                    PawnMovePlate(xBoard, yBoard - 2);
                }
                else
                {
                    PawnMovePlate(xBoard, yBoard - 1);
                }
                break;
            case "white_pawn":
                if (yBoard == 1)
                {
                    PawnMovePlate(xBoard, yBoard + 1);
                    PawnMovePlate(xBoard, yBoard + 2);
                }
                else
                {
                    PawnMovePlate(xBoard, yBoard + 1);
                }
                break;
            default:
                break;
        }
    }

    public void LineMovePlate(int xDirection, int yDirection)
    {
        GameController sc = controller.GetComponent<GameController>();

        int x = (int)xBoard + xDirection;
        int y = (int)yBoard + yDirection;

        while (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
            x += xDirection;
            y += yDirection;
        }
        if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<ChessPieces>().playerType != playerType)
        {
            MovePlateAttackSpawn(x, y);
        }
    }

    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard + 1, yBoard - 2);

    }

    public void MoveSurroundPlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard - 1, yBoard);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard);
    }

    public void PointMovePlate(float x, float y)
    {
        GameController gc = controller.GetComponent<GameController>();
        if (gc.PositionOnBoard((int)x, (int)y))
        {
            GameObject go = gc.GetPosition((int)x, (int)y);
            if (go == null)
            {
                MovePlateSpawn(x, y);
            }
            else if (go.GetComponent<ChessPieces>().playerType != playerType)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    public void PawnMovePlate(float x, float y)
    {
        GameController gc = controller.GetComponent<GameController>();
        if (gc.PositionOnBoard((int)x, (int)y))
        {
            if (gc.GetPosition((int)x, (int)y) == null)
            {
                MovePlateSpawn(x, y);
            }
            if (gc.GetPosition((int)x + 1, (int)y) && gc.GetPosition((int)x + 1, (int)y) != null
                && gc.GetPosition((int)x + 1, (int)y).GetComponent<ChessPieces>().playerType != playerType)
            {
                MovePlateAttackSpawn(x + 1, y);
            }

            if (gc.GetPosition((int)x - 1, (int)y) && gc.GetPosition((int)x - 1, (int)y) != null
                && gc.GetPosition((int)x - 1, (int)y).GetComponent<ChessPieces>().playerType != playerType)
            {
                MovePlateAttackSpawn(x - 1, y);
            }
        }
    }

    private void MovePlateSpawn(float xMatrix, float yMatrix)
    {
        float x = xMatrix;
        float y = yMatrix;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject map = Instantiate(MovePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        ChessMechanics mapScript = map.GetComponent<ChessMechanics>();
        mapScript.SetReference(gameObject);
        mapScript.SetCoords((int)xMatrix, (int)yMatrix);
    }

    private void MovePlateAttackSpawn(float xMatrix, float yMatrix)
    {
        float x = xMatrix;
        float y = yMatrix;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject map = Instantiate(MovePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        ChessMechanics mapScript = map.GetComponent<ChessMechanics>();
        mapScript.attack = true;
        mapScript.SetReference(gameObject);
        mapScript.SetCoords((int)xMatrix, (int)yMatrix);
    }
}
