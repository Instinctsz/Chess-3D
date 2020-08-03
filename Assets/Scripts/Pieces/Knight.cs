using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{

    public override List<Vector2> PossibleMoves
    {
        get
        {
            List<Vector2> possibilities = new List<Vector2>();
            possibilities.Add(new Vector2(position.x + 1, position.y + 2));
            possibilities.Add(new Vector2(position.x - 1, position.y + 2));
            possibilities.Add(new Vector2(position.x + 1, position.y - 2));
            possibilities.Add(new Vector2(position.x - 1, position.y - 2));

            possibilities.Add(new Vector2(position.x - 2, position.y - 1));
            possibilities.Add(new Vector2(position.x - 2, position.y + 1));
            possibilities.Add(new Vector2(position.x + 2, position.y - 1));
            possibilities.Add(new Vector2(position.x + 2, position.y + 1));

            attacks = new List<Vector2>(possibilities);

            ChessHelper.ClearMovesWithPieces(ref possibilities);

            return possibilities;
        }
    }
}
