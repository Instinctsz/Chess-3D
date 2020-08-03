using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ChessHelper : MonoBehaviour
{
    public static GameObject GetSquare(Vector2 position)
    {
        if (position.x - 1 >= 8 || position.x - 1 < 0 || position.y > 8 || position.y < 1)
            return null;

        Transform row = GameObject.Find("Chessboard").transform.Find("Row" + (int)position.y);

        GameObject square = row.GetChild((int)position.x - 1).gameObject;

        return square;
    }

    public static Piece GetPiece(Vector2 position)
    {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("Piece");

        foreach (GameObject goPiece in pieces)
        {
            Piece piece = ChessTracker.PieceConnections[goPiece];

            if (piece.position == position)
                return piece;
        }

        return null;
    }

    public static bool HasPiece(Vector2 position)
    {
        Transform whitePieces = GameObject.Find("Pieces").transform.GetChild(0);

        foreach (Transform whitePiece in whitePieces)
        {
            Vector2 chessPosition = ChessTracker.PieceConnections[whitePiece.gameObject].position;
            if (chessPosition == position)
                return true;
        }

        Transform blackPieces = GameObject.Find("Pieces").transform.GetChild(1);

        foreach (Transform blackPiece in blackPieces)
        {
            Vector2 chessPosition = ChessTracker.PieceConnections[blackPiece.gameObject].position;
            if (chessPosition == position)
                return true;
        }

        return false;
    }

    public static int GetRow(GameObject row)
    {
        return Int32.Parse(row.name.Split(new[] { "Row" }, StringSplitOptions.None)[1]);
    }

    public static Vector3 CalcPosition(Vector2 pos, Piece piece)
    {
        Vector3 SquarePos = ChessHelper.GetSquare(pos).transform.position;
        return new Vector3(SquarePos.x, piece.Controller.transform.position.y, SquarePos.z);
    }

    public static void ResetHistory()
    {
        ChessHistory.selectedSquare = null;
        ChessHistory.selectedSquareMat = null;
        ChessHistory.selectedPiece = null;
    }

    public static void ResetOldSquares()
    {
        foreach (GameObject oldMoveableSquare in ChessHistory.moveableSquares)
        {
            Destroy(oldMoveableSquare);
        }
    }

    public static void ResetOldAttackSquares()
    {
        foreach (GameObject oldAttackSquare in ChessHistory.attackSquares)
        {
            Destroy(oldAttackSquare);
        }
    }

    public static void ClearMovesWithPieces(ref List<Vector2> moves)
    {
        for (int i = moves.Count - 1; i >= 0; i--)
            if (ChessHelper.HasPiece(moves[i]))
                moves.Remove(moves[i]);
    }

    public static GameObject PlaceSquare(Vector2 position, GameObject prefab)
    {
        GameObject squarePossibleMove = ChessHelper.GetSquare(position);

        if (squarePossibleMove == null)
            return null;

        Vector3 squarePosition = squarePossibleMove.transform.position;

        Vector3 moveableSquarePos = new Vector3(squarePosition.x, squarePosition.y + 0.1f, squarePosition.z);

        GameObject GOmoveableSquare = Instantiate(prefab, moveableSquarePos, Quaternion.identity);
        GOmoveableSquare.transform.SetParent(squarePossibleMove.transform);
        GOmoveableSquare.transform.position = moveableSquarePos;

        return GOmoveableSquare;
    }

    public static void AttackPiece(Piece attacker, Piece attacked)
    {
        ChessBoard board = ChessTracker.board;

        if (attacked.isWhite)
            board.WhitePieces.Remove(board.WhitePieces.Find(x => x.Controller.name == attacked.Controller.name));
        else
            board.BlackPieces.Remove(board.BlackPieces.Find(x => x.Controller.name == attacked.Controller.name));

        Destroy(ChessHelper.GetPiece(attacked.position).Controller.gameObject);
        attacker.position = attacked.position;
        attacker.Controller.transform.position = ChessHelper.CalcPosition(attacked.position, attacked);

        ChessHistory.selectedSquare.GetComponent<MeshRenderer>().material = ChessHistory.selectedSquareMat;
        ChessHelper.ResetOldSquares();
        ChessHelper.ResetHistory();
        ChessHelper.ResetOldAttackSquares();
        ChessTracker.isWhiteTurn = !ChessTracker.isWhiteTurn;

        ChessHelper.HandleKingsInCheck();
    }

    

    public static List<Piece> GetAllPieces(bool IsWhite)
    {
        List<Piece> pieces = new List<Piece>();
        int colorIndex;

        if (IsWhite)
            colorIndex = 0;
        else
            colorIndex = 1;
        //
        Transform tPieces = GameObject.Find("Pieces").transform.GetChild(colorIndex);

        foreach (Transform piece in tPieces)
        {
            Piece Ppiece = ChessTracker.PieceConnections[piece.gameObject];
            
            pieces.Add(Ppiece);
        }


        return pieces;
    }

    public static void HandleKingsInCheck()
    {
        if (ChessTracker.board.IsKingInCheck(true))
        {
            Debug.Log("White is in check");
        }
        if (ChessTracker.board.IsKingInCheck(false))
        {
            Debug.Log("Black is in check");
        }
    }

    // Checks for any moves where the king would still be in check
    public static List<Vector2> RemoveInvalidMoves(Piece piece, List<Vector2> moves)
    {
        ChessBoard newBoard = ChessTracker.board.Copy();

        Piece copyPiece;

        if (piece.isWhite)
            copyPiece = newBoard.WhitePieces.Find(x => x.Controller.name == piece.Controller.name);
        else
            copyPiece = newBoard.BlackPieces.Find(x => x.Controller.name == piece.Controller.name);

        Debug.Log(copyPiece.id);
        for (int i = 0; i < moves.Count; i++)
        {
            Vector2 move = moves[i];
            Vector2 originalPos = copyPiece.position;

            copyPiece.position = move;

            if (newBoard.IsKingInCheck(piece.isWhite))
            {
                Debug.Log("Here");
                moves.Remove(move);
            }

            copyPiece.position = originalPos;
        }

        return moves;
    }

    public static Piece GetCorrectPieceType(GameObject piece)
    {
        switch (piece.name)
        {
            case "King":
                return new King();
            case "Queen":
                return new Queen();
            case "Bishop":
                return new Bishop();
            case "Knight":
                return new Knight();
            case "Rook":
                return new Rook();
        }

        if (piece.name.StartsWith("Pawn"))
            return new Pawn();
        else
            return new Piece();
    }

    public static GameObject FindKeyByValue(Dictionary<GameObject, Piece> dictionary, Piece value)
    {

        foreach (KeyValuePair<GameObject, Piece> entry in dictionary)
        {
            if (entry.Value.id == value.id)
                return entry.Key;
        }

        Debug.Log("Couldnt find key for: " + value.id);

        return null;
    }
}
