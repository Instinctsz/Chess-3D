using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    private Piece piece;

    public virtual void Start()
    {
        piece = ChessTracker.PieceConnections[gameObject];

        piece.selectedMat = Resources.Load<Material>("Materials/Selected");
        piece.moveableMat = Resources.Load<Material>("Materials/Moveable");
        piece.moveableSquare = Resources.Load<GameObject>("Prefabs/Square");
        piece.attackSquare = Resources.Load<GameObject>("Prefabs/AttackSquare");
    }

    private void OnMouseDown()
    {
        bool setMatOnCurrentSquare = true;
        bool displayMoveableSquares = true;

        if (piece.isWhite && !ChessTracker.isWhiteTurn)
        {
            if (ChessHistory.selectedPiece != null && ChessHistory.selectedPiece.PossibleAttacks.Contains(piece.position))
            {
                ChessHelper.AttackPiece(ChessHistory.selectedPiece, piece);
                piece.attacks = new List<Vector2>();
            }

            return;
        }
        else if (!piece.isWhite && ChessTracker.isWhiteTurn)
        {
            if (ChessHistory.selectedPiece != null && ChessHistory.selectedPiece.PossibleAttacks.Contains(piece.position))
            {
                ChessHelper.AttackPiece(ChessHistory.selectedPiece, piece);
                piece.attacks = new List<Vector2>();
            }

            return;
        }
        

        GameObject square = ChessHelper.GetSquare(piece.position);

        if (ChessHistory.selectedSquare != null)
        {
            // If clicks on same piece
            if (square.GetInstanceID() == ChessHistory.selectedSquare.GetInstanceID())
            {
                square.GetComponent<MeshRenderer>().material = ChessHistory.selectedSquareMat;
                setMatOnCurrentSquare = false;
                ChessHelper.ResetHistory();
                displayMoveableSquares = false;
            }
            else
            {
                ChessHistory.selectedSquare.GetComponent<MeshRenderer>().material = ChessHistory.selectedSquareMat;
            }
        }
        // Removing old moveable squares
        ChessHelper.ResetOldSquares();
        ChessHelper.ResetOldAttackSquares();
        piece.attacks = new List<Vector2>();

        Debug.Log(displayMoveableSquares);
        // Displaying moveable squares
        if (displayMoveableSquares)
        {
            foreach (Vector2 possibleMove in piece.PossibleMoves)
            {
                GameObject go = ChessHelper.PlaceSquare(possibleMove, piece.moveableSquare);

                ChessHistory.moveableSquares.Add(go);
            }
        }

        if (setMatOnCurrentSquare)
        {
            ChessHistory.selectedSquareMat = square.GetComponent<MeshRenderer>().material;
            square.GetComponent<MeshRenderer>().material = piece.selectedMat;
            ChessHistory.selectedSquare = square;
            ChessHistory.selectedPiece = piece;
        }


        // Display possible attacks
        if (displayMoveableSquares)
        {
            foreach (Vector2 possibleAttack in piece.PossibleAttacks)
            {
                if (ChessHelper.HasPiece(possibleAttack) && ChessHelper.GetPiece(possibleAttack).isWhite != piece.isWhite)
                {
                    GameObject go = ChessHelper.PlaceSquare(possibleAttack, piece.attackSquare);
                    ChessHistory.attackSquares.Add(go);
                }
            }
        }
    }
}
