using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessTracker : MonoBehaviour
{
    public static bool isWhiteTurn = true;
    public static ChessBoard board;

    public static Dictionary<GameObject, Piece> PieceConnections = new Dictionary<GameObject, Piece>();
}
