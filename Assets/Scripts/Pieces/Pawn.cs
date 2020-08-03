using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;

public class Pawn : Piece
{
    public bool isFirstMove = true;

    public override List<Vector2> PossibleMoves
    {
        get
        {
            List<Vector2> possibilities = new List<Vector2>();
            int modifier = 1;

            if (!isWhite)
                modifier = -1;

            Vector2 pos1 = new Vector2(position.x, position.y + 1 * modifier);
            Vector2 pos2 = new Vector2(position.x, position.y + 2 * modifier);

            if (!ChessHelper.HasPiece(pos1))
                possibilities.Add(pos1);

            if (isFirstMove && !ChessHelper.HasPiece(pos2) && !ChessHelper.HasPiece(pos1))
                possibilities.Add(pos2);
            
            Vector2 atkPos1 = new Vector2(position.x - 1, position.y + 1 * modifier);
            Vector2 atkPos2 = new Vector2(position.x + 1, position.y + 1 * modifier);
            attacks.Add(atkPos1);
            attacks.Add(atkPos2);

            return possibilities;
        }
    }
}
