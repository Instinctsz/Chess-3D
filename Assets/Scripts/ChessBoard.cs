using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard
{
    public List<Piece> WhitePieces = new List<Piece>();
    public List<Piece> BlackPieces = new List<Piece>();

    public bool IsKingInCheck(bool isWhiteKing)
    {
        Piece king;
        List<Piece> pieces;

        if (isWhiteKing)
        {
            king = WhitePieces.Find(x => x.Controller.name == "King");
            pieces = BlackPieces;
        }
        else
        {
            king = BlackPieces.Find(x => x.Controller.name == "King");
            pieces = WhitePieces;
        }


        foreach (Piece piece in pieces)
        {
            if (piece.PossibleAttacks.Contains(king.position))
            {
                return true;
            }
        }

        return false;
    }

    public ChessBoard Copy()
    {
        ChessBoard board = new ChessBoard();

        foreach (Piece whitePiece in WhitePieces)
            board.WhitePieces.Add(whitePiece.Copy());

        foreach (Piece blackPiece in BlackPieces)
            board.BlackPieces.Add(blackPiece.Copy());

        return board;
    }
}

