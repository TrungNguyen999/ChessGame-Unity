using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 This script is for writing all the mechanics of the game (isCaptured(), canCastle(), isCheck(), canEnPassant(), isCheckMate())
 */
public class ChessMechanics : MonoBehaviour
{
    [SerializeField] GameObject controller;

    // MovePlate will need a reference of the chess pieces
    GameObject reference = null;

    // Board position, not world position
    int matrixX;
    int matrixY;

    // false: not attacking, true: attacking
    public bool attack = false;

    void Start()
    {
        if (attack)
        {
            // Change to red color
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        if (attack)
        {
            GameObject cp = controller.GetComponent<GameController>().GetPosition(matrixX, matrixY);
            Destroy(cp);
        }
        
        controller.GetComponent<GameController>().SetPositionEmpty((int)reference.GetComponent<ChessPieces>().GetXBoard(),
            (int)reference.GetComponent<ChessPieces>().GetYBoard());

        reference.GetComponent<ChessPieces>().SetXBoard(matrixX);
        reference.GetComponent<ChessPieces>().SetYBoard(matrixY);
        reference.GetComponent<ChessPieces>().SetCoords();

        controller.GetComponent<GameController>().SetPosition(reference);
        reference.GetComponent<ChessPieces>().DestroyedMovePlate();
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }


}
