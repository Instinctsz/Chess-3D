using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessHistory
{
    public static GameObject selectedSquare;
    public static Material selectedSquareMat;
    public static List<GameObject> moveableSquares = new List<GameObject>();
    public static List<GameObject> attackSquares = new List<GameObject>();
    public static Piece selectedPiece;
}
