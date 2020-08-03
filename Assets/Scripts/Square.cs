using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        int row = ChessHelper.GetRow(gameObject.transform.parent.gameObject);
        int column = transform.GetSiblingIndex() + 1;
        position = new Vector2(column, row);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (ChessHistory.selectedSquare != null && ChessHistory.selectedPiece.PossibleMoves.Contains(position))
        {
            // Move the pawn to chosen square
            ChessHistory.selectedPiece.position = position;
            ChessHistory.selectedPiece.Controller.transform.position = ChessHelper.CalcPosition(position, ChessHistory.selectedPiece);

            if (ChessHistory.selectedPiece is Pawn)
            {
                Pawn pawn = (Pawn) ChessHistory.selectedPiece;
                pawn.isFirstMove = false;
            }

            ChessTracker.isWhiteTurn = !ChessTracker.isWhiteTurn;
            ChessHistory.selectedSquare.GetComponent<MeshRenderer>().material = ChessHistory.selectedSquareMat;
            ChessHelper.ResetOldSquares();
            ChessHelper.ResetOldAttackSquares();
            ChessHelper.ResetHistory();

            // Calculate if any king is in check or checkmate
            ChessHelper.HandleKingsInCheck();
        }
    }
}
