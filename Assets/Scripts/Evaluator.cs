using System;
using UnityEngine;

public  class Evaluator
{
    private GameManager gameManager;


    void Awake ()
    {
        gameManager = GameManager.instance;


} // this is now placed within the AI script

public static int GetPieceValue(GameObject piece)
    {
        if (piece.name.Contains("Pawn"))
            return 75;
        else if (piece.name.Contains("Knight"))
            return 200;
        else if (piece.name.Contains("Bishop"))
            return 200;
        else if (piece.name.Contains("Rook"))
            return 300;
        else if (piece.name.Contains("Queen"))
            return 500;
        else if (piece.name.Contains("King"))
            return 1500;
        else
            return 0;
    }

    public  int EvaluateBoard()
    {
        int score = 0;
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                GameObject piece = gameManager.PieceAtGrid(new Vector2Int(x, y));
                if (piece != null)
                {
                    if (gameManager.black.pieces.Contains(piece)) //for the AI 
                    {
                        score += GetPieceValue(piece);
                    }
                    else if (gameManager.white.pieces.Contains(piece))
                    {
                        score -= GetPieceValue(piece);
                    }
                }
            }
        }
        return score;
    }

   
}
