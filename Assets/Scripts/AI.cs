using UnityEngine;
using System.Collections.Generic;
/* the class contains the logic for the Ai.t hold the evolatuion and the value of each piece. it also hold the minmax function and
 the alpha beta pruning.it needs to be conneted with the AIplayer script in order for the Ai to proper function and evaulate positons*/
public class AI : MonoBehaviour 
{
    private GameManager gameManager;
    

    public int searchDepth = 3; //  

    public void Start()
    {
        gameManager = GameManager.instance;
    }


    public Move GetBestMove()
    {
        int bestScore = int.MinValue; //this is the defaut value of the score. the low score and will help determine the best move
        Move bestMove = null;

        List<Move> moves = GenerateMoves(gameManager.currentPlayer); //this produces the list of moves

        foreach (Move move in moves)
        {
            GameManager.instance.Move(move.piece, move.destination);
            int score = Minimax(searchDepth - 1, int.MinValue, int.MaxValue, false);
            GameManager.instance.UndoMove(move.piece, move.origin, move.destination); // Undo after evaluation

            if (score > bestScore)//this check for each scor for the move, and would be used to replaced each move.-> higher the score the best move for the AI.
            {
                bestScore = score;
                bestMove = move;
            }
        }
        return bestMove;
    }

   
    public int Minimax(int depth, int alpha, int beta, bool maximizingPlayer)
    {
        if (depth == 0) //the dpeth/moves the AI will reach
        {
            return EvaluateBoard();
        }

        List<Move> moves = GenerateMoves(gameManager.currentPlayer);

        if (maximizingPlayer)// goal of maxmazing the score
        {
            int maxEval = int.MinValue;//start with the highest value -> min value

            foreach (Move move in moves)
            {
                GameManager.instance.Move(move.piece, move.destination);
                int eval = Minimax(depth - 1, alpha, beta, false); //use of recursion -> based on the depth and return  calucated evulation
                GameManager.instance.UndoMove(move.piece, move.origin, move.destination); // Undo after evaluation ->  return the original state
                maxEval = Mathf.Max(maxEval, eval); // see if the eval is less or more than the orgignal maxevual. -. trying to get the highest score
                alpha = Mathf.Max(alpha, eval);// then the alpha value is check to be less or more the then eval calaucalted 

                if (beta <= alpha) /*if the beta value is less than alpha then it will return, this will cut off a branch so less moves will be calaclated. -> similar to min
                                    function. This elminate any possible moves that will make it easier for the player. produce a higher score for the player */
                    break; 
            }
            return maxEval;
        }
        else// goal of minimizing  the score
        {
            int minEval = int.MaxValue; //start with the highest value -> max value
            foreach (Move move in moves)
            {
                GameManager.instance.Move(move.piece, move.destination);
                int eval = Minimax(depth - 1, alpha, beta, true); //use of recursion -> based on the depth and return  calucated evulation
                GameManager.instance.UndoMove(move.piece, move.origin, move.destination); // Undo after evaluation -> return the original state 
                minEval = Mathf.Min(minEval, eval); // see if the eval is less or more than the orgignal mineval
                beta = Mathf.Min(beta, eval); // then the beta value is check to be less or more the then eval calaucalted 

                if (beta <= alpha) //if the beta value is less than alpha then it will return, this will cut off a branch so less moves will be calaclated. 
                    break; 
            }
            return minEval;
        }
    }

    List<Move> GenerateMoves(Player player) //this gives the list of moves
    {
        List<Move> moves = new List<Move>();

        foreach (var pieceObject in player.pieces) //run through every single piece for the player. 
        {
            Piece piece = pieceObject.GetComponent<Piece>();   
            Vector2Int currentPos = gameManager.GridForPiece(pieceObject);
            List<Vector2Int> possibleMoves = gameManager.MovesForPiece(pieceObject); // add to the list for every piece 
            foreach (Vector2Int move in possibleMoves)
            {
                moves.Add(new Move(pieceObject, currentPos, move));//-> move into the class. the current positon and potential postion.
            }
        }
        return moves;
    }

    public static int GetPieceValue(GameObject piece) // this places the value of each piece which will help determine how the AI functions
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

    public int EvaluateBoard() //this would calculate the score depending on the total amount of material
    {
        int score = 0;
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                GameObject piece = gameManager.PieceAtGrid(new Vector2Int(x, y));
                if (piece != null)
                {
                    if (gameManager.black.pieces.Contains(piece))  
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


