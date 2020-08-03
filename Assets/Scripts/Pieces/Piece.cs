using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Piece
{
    public int id;
    public static int nextId = 0;

    public Vector2 position; // Row column
    public bool isWhite;

    public Material selectedMat;
    public Material moveableMat;
    public GameObject moveableSquare;
    public GameObject attackSquare;
    public List<Vector2> attacks = new List<Vector2>();

    public Piece()
    {
        id = nextId;
        nextId++;
    }

    public Piece Copy()
    {
        Piece piece = new Piece();

        piece.id = id;
        piece.position = position;
        piece.isWhite = isWhite;
        piece.selectedMat = selectedMat;
        piece.moveableMat = moveableMat;
        piece.moveableSquare = moveableSquare;
        piece.attackSquare = attackSquare;
        return piece;
    }

    public PieceController Controller
    {
        get { return ChessHelper.FindKeyByValue(ChessTracker.PieceConnections, this).GetComponent<PieceController>(); }
    }

    public virtual List<Vector2> PossibleMoves
    {
        get
        {
            return new List<Vector2>();
        }
    }

    public virtual List<Vector2> PossibleAttacks
    {
        get
        {
            attacks = new List<Vector2>();
            List<Vector2> temp = this.PossibleMoves;
            return attacks;
        }
    }
}
