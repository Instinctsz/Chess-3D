using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bishop : Piece
{
    public override List<Vector2> PossibleMoves
    {
        get
        {
            bool canGoTopLeft = true;
            bool canGoTopRight = true;
            bool canGoDownLeft = true;
            bool canGoDownRight = true;

            List<Vector2> possibilities = new List<Vector2>();

            for (int i = 1; i <= 8; i++)
            {
                Vector2 newTopLeft = new Vector2(position.x - i, position.y + i);
                Vector2 newTopRight = new Vector2(position.x + i, position.y + i);
                Vector2 newDownLeft = new Vector2(position.x - i, position.y - i);
                Vector2 newdownRight = new Vector2(position.x + i, position.y - i);

                if (ChessHelper.HasPiece(newTopLeft) && canGoTopLeft)
                {
                    canGoTopLeft = false;

                    if (ChessHelper.GetPiece(newTopLeft).isWhite != isWhite)
                        attacks.Add(newTopLeft);
                }
                if (ChessHelper.HasPiece(newTopRight) && canGoTopRight)
                {
                    canGoTopRight = false;

                    if (ChessHelper.GetPiece(newTopRight).isWhite != isWhite)
                        attacks.Add(newTopRight);
                }
                if (ChessHelper.HasPiece(newDownLeft) && canGoDownLeft)
                {
                    canGoDownLeft = false;

                    if (ChessHelper.GetPiece(newDownLeft).isWhite != isWhite)
                        attacks.Add(newDownLeft);
                }
                if (ChessHelper.HasPiece(newdownRight) && canGoDownRight)
                {
                    canGoDownRight = false;

                    if (ChessHelper.GetPiece(newdownRight).isWhite != isWhite)
                        attacks.Add(newdownRight);
                }

                if (canGoTopLeft)
                    possibilities.Add(newTopLeft);

                if (canGoTopRight)
                    possibilities.Add(newTopRight);

                if (canGoDownLeft)
                    possibilities.Add(newDownLeft);

                if (canGoDownRight)
                    possibilities.Add(newdownRight);
            }

            attacks = attacks.Concat(possibilities).ToList();

            return possibilities;
        }
    }
}
