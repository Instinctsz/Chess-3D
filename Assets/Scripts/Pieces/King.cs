using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override List<Vector2> PossibleMoves
    {
        get
        {
            List<Vector2> possibilities = new List<Vector2>();
            List<Vector2> allEnemyAttackMoves = new List<Vector2>();
            
            foreach (Piece piece in ChessHelper.GetAllPieces(!isWhite))
            {
                foreach (Vector2 piecePossibleAttack in piece.PossibleAttacks)
                {
                    allEnemyAttackMoves.Add(piecePossibleAttack);
                }
            }

            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    Vector2 newPos = new Vector2(position.x + x, position.y + y);

                    if (!allEnemyAttackMoves.Contains(newPos))
                        possibilities.Add(newPos);

                }
            }

            ChessHelper.ClearMovesWithPieces(ref possibilities);

            return possibilities;
        }
    }

    public override List<Vector2> PossibleAttacks
    {
        get
        {
            List<Vector2> attacks = new List<Vector2>();
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    Vector2 newPos = new Vector2(position.x + x, position.y + y);

                    attacks.Add(newPos);

                }
            }

            return attacks;
        }
    }
}
