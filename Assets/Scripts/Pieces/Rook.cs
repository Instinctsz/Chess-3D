using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rook : Piece
{

    public override List<Vector2> PossibleMoves
    {
        get
        {
            bool canGoUp = true;
            bool canGoDown = true;
            bool canGoLeft = true;
            bool canGoRight = true;
            List<Vector2> possibilities = new List<Vector2>();
                
            for (int i = 1; i <= 8; i++)
            {
                Vector2 newUp = new Vector2(position.x, position.y + i);
                Vector2 newRight = new Vector2(position.x + i, position.y);
                Vector2 newDown = new Vector2(position.x, position.y - i);
                Vector2 newLeft = new Vector2(position.x - i, position.y);

                if (ChessHelper.HasPiece(newUp) && canGoUp)
                {
                    canGoUp = false;

                    if (ChessHelper.GetPiece(newUp).isWhite != isWhite)
                        attacks.Add(newUp);
                }

                if (ChessHelper.HasPiece(newDown) && canGoDown)
                {
                    canGoDown = false;

                    if (ChessHelper.GetPiece(newDown).isWhite != isWhite)
                        attacks.Add(newDown);
                }

                if (ChessHelper.HasPiece(newLeft) && canGoLeft)
                {
                    canGoLeft = false;  

                    if (ChessHelper.GetPiece(newLeft).isWhite != isWhite)
                        attacks.Add(newLeft);
                }

                if (ChessHelper.HasPiece(newRight) && canGoRight)
                {
                    canGoRight = false;

                    if (ChessHelper.GetPiece(newRight).isWhite != isWhite)
                        attacks.Add(newRight);
                }

                if (canGoUp)
                    possibilities.Add(newUp);

                if (canGoRight)
                    possibilities.Add(newRight);

                if (canGoDown)
                    possibilities.Add(newDown);

                if (canGoLeft)
                    possibilities.Add(newLeft);
            }

            attacks = attacks.Concat(possibilities).ToList();

            return possibilities;
        }
    }
}
