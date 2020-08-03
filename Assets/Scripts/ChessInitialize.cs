using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessInitialize : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
        ChessTracker.board = new ChessBoard();
        InitializePieces(true);
        InitializePieces(false);
    }

    public static void InitializePieces(bool isWhite)
    {
        Transform pieces;

        if (isWhite)
            pieces = GameObject.Find("Pieces").transform.Find("White");
        else
            pieces = GameObject.Find("Pieces").transform.Find("Black");

        int counter = 1;

        foreach (Transform Piece in pieces)
        {
            GameObject piece = Piece.gameObject;
            Piece pieceComponent = ChessHelper.GetCorrectPieceType(piece);
            pieceComponent.isWhite = isWhite;

            ChessTracker.PieceConnections.Add(piece, pieceComponent);

            if (isWhite)
                ChessTracker.board.WhitePieces.Add(pieceComponent);
            else
                ChessTracker.board.BlackPieces.Add(pieceComponent);

            if (piece.name.StartsWith("Pawn"))
            {
                int pawnIndex = Int32.Parse(piece.name.Split(new[] { "Pawn" }, StringSplitOptions.None)[1]);

                int yPosition;

                if (isWhite)
                    yPosition = 2;
                else
                    yPosition = 7;

                pieceComponent.position = new Vector2(pawnIndex + 1, yPosition);
            }
            else
            {
                int yPosition;

                if (isWhite)
                    yPosition = 1;
                else
                    yPosition = 8;

                pieceComponent.position = new Vector2(counter, yPosition);
                counter++;
            }
        }


        
    }
}
