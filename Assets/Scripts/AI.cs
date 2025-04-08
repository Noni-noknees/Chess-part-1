using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class AI : MonoBehaviour
{

    // Opening scores
    private int[,] pawnPositionScoresOpening = new int[8, 8] {
    {0,  0,  0,  0,  0,  0,  0,  0},
    {10,  10,  10, 10, 10,  10,  10,  10},
    {-10,  5,  -20, 10, 10,  -20,  5,  -10},
    {-10,  -10,  -10, 40, 40, -10,  -10,  -10},
    {-10,  -10,  -10, 4, 40,  -10,  -10,  -10},
    {-10,  5,  -20, 10, 10,  -20,  5,  -10},
    {10,  10,  10, 10, 10,  10,  10,  10},
    {0,  0,  0,  0,  0,  0,  0,  0}  //fix 
};

    private int[,] knightPositionScoresOpening = new int[8, 8] {
    {-50,-40,-30,-30,-30,-30,-40,-50},
    {-40,-20,  0,  5,  5,  0,-20,-40},
    {-30,  0, 20, 0, 0, 20,  0,-30},
    {-30,  0, -15, -20, -20, -15,  0,-30},
    {-30,  0, -15, -20, -20, -15,  0,-30},
    {-30,  0, 20, 0, 0, 0,  0,-30},
    {-40,-20,  0,  0,  0,  0,-20,-40},
    {-50,-40,-30,-30,-30,-30,-40,-50}
};
     private int[,] bishopPositionScoresOpening = new int[8, 8] {
    {-50,-40,-30,-30,-30,-30,-40,-50},
    {-40,-20,  0,  5,  5,  0,-20,-40},
    {-30,  5, -10, 15, 15, -10,  5,-30},
    {-30,  30, 40, 0, 0, 40,  30,-30},
    {-30,  30, 40, 0, 0, 40,  30,-30},
    {-30,  0, -10, 15, 15, -10,  0,-30},
    {-40,-20,  0,  0,  0,  0,-20,-40},
    {-50,-40,-30,-30,-30,-30,-40,-50}
};
    private int[,] rookPositionScoresOpening = new int[8, 8] {
    {-50,-1000,0,-30,-30,0,-1000,-50},
    {-40,-20,  0,  0,  0,  0,-20,-40},
    {-30,  0, 0, 0, 0, 0,  5,-30},
    {-30,  0, 0, 0, 0, 0,  0,-30},
    {-30,  0, 0, 0, 0, 0,  5,-30},
    {-30,  0, 0, 0, 0, 0,  0,-30},
    {-40,-20,  0,  0,  0,  0,-20,-40},
    {-50,-1000,0,-30,0,-30,-1000,-50}
};
    private int[,] queenPositionScoresOpening = new int[8, 8] {
    {0,0,0,0,0,0,0,0},
    {0,0,0,0,30,30,0,0},
    {0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0},
    {0,0,0,0,30,30,0,0},
    {0,0,0,0,0,0,0,0}
};
    private int[,] kingPositionScoresOpening = new int[8, 8] {
    {10,20,10,0,0,10,20,10},
    {-20,-20,  -20,  -20,  -20,  -20,-20,-20},
    {-100,  -100, -100, -100, -100, -100,  -100,-100},
    {-100,  -100, -100, -100, -100, -100,  -100,-100},
    {-100,  -100, -100, -100, -100, -100,  -100,-100},
    {-100,  -100, -100, -100, -100, -100,  -100,-100},
    {-20,-20,  -20,  -20,  -20,  -20,-20,-20},
    {10,20,10,0,0,10,20,-10}
};


    // middle game
    private int[,] pawnPositionScoresMiddleGame = new int[8, 8] {
        {0,  0,  0,  0,  0,  0,  0,  0},
    {10,  10,  10, 10, 10,  10,  10,  10},
    {-10,  5,  -20, 10, 10,  -20,  5,  -10},
    {-10,  -10,  -10, 40, 40, -10,  -10,  -10},
    {-10,  -10,  -10, 4, 40,  -10,  -10,  -10},
    {-10,  5,  -20, 10, 10,  -20,  5,  -10},
    {10,  10,  10, 10, 10,  10,  10,  10},
    {0,  0,  0,  0,  0,  0,  0,  0}  //fix 
};

    private int[,] knightPositionScoresMiddleGame = new int[8, 8] {
{-50,-40,-30,-30,-30,-30,-40,-50},
    {-40,-20,  0,  5,  5,  0,-20,-40},
    {-30,  0, 20, 0, 0, 20,  0,-30},
    {-30,  0, -15, -20, -20, -15,  0,-30},
    {-30,  0, -15, -20, -20, -15,  0,-30},
    {-30,  0, 20, 0, 0, 0,  0,-30},
    {-40,-20,  0,  0,  0,  0,-20,-40},
    {-50,-40,-30,-30,-30,-30,-40,-50}
};

    private int[,] rookPositionScoresMiddleGame = new int[8, 8] {
  {-50,-1000,0,2000,100,0,-1000,-50},
    {-40,-20,  0,  0,  0,  0,-20,-40},
    {-30,  0, 0, 0, 0, 0,  5,-30},
    {-30,  0, 0, 0, 0, 0,  0,-30},
    {-30,  0, 0, 0, 0, 0,  5,-30},
    {-30,  0, 0, 0, 0, 0,  0,-30},
    {-40,-20,  0,  0,  0,  0,-20,-40},
    {-50,-1000,1000,2000,1000,-30,-1000,-50}
};
    private int[,] queenPositionScoresMiddleGame = new int[8, 8] {
    {-10,-10,-10,-10,-10,-10,-10,-10},
    {0,0,30,30,30,30,0,0},
    {0,50,40,0,0,40,50,0},
    {0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0},
    {0,50,40,0,0,40,50,0},
    {0,0,30,30,30,30,0,0},
    {-10,-10,-10,-10,-10,-10,-10,-10}
};
    private int[,] kingPositionScoresMiddleGame = new int[8, 8] {
     {10,20,10,0,0,10,20,10},
    {-20,-20,  -20,  -20,  -20,  -20,-20,-20},
    {-100,  -100, -100, -100, -100, -100,  -100,-100},
    {-100,  -100, -100, -100, -100, -100,  -100,-100},
    {-100,  -100, -100, -100, -100, -100,  -100,-100},
    {-100,  -100, -100, -100, -100, -100,  -100,-100},
    {-20,-20,  -20,  -20,  -20,  -20,-20,-20},
    {10,20,10,0,0,10,20,-10}
};
    //end game
    private int[,] pawnPositionScoresEndGame = new int[8, 8] {
 {100,  100,  100,  100,  100,  100, 100,  100},
    {90, 90, 90,90,90, 90, 90,  90},
    {80, 80, 80, 80, 80, 80, 80, 80},
    {50,  50,  50, 50, 50,  50,  50,  50},
    {50,  50,  50, 50, 50,  50,  50,  50},
    {80, 80, 80, 80, 80, 80, 80, 80},
    {90, 90, 90,90,90, 90, 90,  90},
    {100,  100,  100,  100,  100,  100, 100,  100}
};

    private int[,] knightPositionScoresEndGame = new int[8, 8] {
    {-50,-40,-30,-30,-30,-30,-40,-50},
    {-40,-20,  0,  5,  5,  0,-20,-40},
    {-30,  5, 10, 15, 15, 10,  5,-30},
    {-30,  0, 15, 20, 20, 15,  0,-30},
    {-30,  5, 15, 20, 20, 15,  5,-30},
    {-30,  0, 10, 15, 15, 10,  0,-30},
    {-40,-20,  0,  0,  0,  0,-20,-40},
    {-50,-40,-30,-30,-30,-30,-40,-50}
};

    private int[,] rookPositionScoresEndGame = new int[8, 8] {
    {-10,0,0,10,10,0,0,-10},
    {-10,10, 10,  10,  10,  10,10,-10},
    {-10, 20, 20, 25, 25, 20,  20,-10},
    {-10,  0, 15, 30, 30, 15,  0,-10},
    {-10,  5, 15, 30, 30, 15,  5,-10},
    {-10,  0, 10, 15, 15, 10,  0,-10},
    {-10, 20, 20, 25, 25, 20,  20,-10},
    {-10,0,0,10,10,0,0,-10},
};
    private int[,] queenPositionScoresEndGame = new int[8, 8] {
    {-50,-40,-30,-30,-30,-30,-40,-50},
    {-40,-20,  0,  5,  5,  0,-20,-40},
    {-30,  5, 10, 15, 15, 10,  5,-30},
    {-30,  0, 15, 20, 20, 15,  0,-30},
    {-30,  5, 15, 20, 20, 15,  5,-30},
    {-30,  0, 10, 15, 15, 10,  0,-30},
    {-40,-20,  0,  0,  0,  0,-20,-40},
    {-50,-40,-30,-30,-30,-30,-40,-50}
};
  

    private GameManager gameManager;
    private List<Move> moveslist = new List<Move>();

    public int searchDepth = 1;    
    internal static AI instance; 
    public Button Button2, Button3, Button4, Button5, Button6;


    public void Start()
    {
        gameManager = GameManager.instance;
    }
    public void On2Button() // setup for the AI depth 
    {
        searchDepth = 2;
        Button2.gameObject.SetActive(false);
        Button3.gameObject.SetActive(false);
        Button4.gameObject.SetActive(false);
        Button5.gameObject.SetActive(false);
        Button6.gameObject.SetActive(false);

    }

    public void On3Button() 
    {
        searchDepth = 3;
        Button2.gameObject.SetActive(false);
        Button3.gameObject.SetActive(false);
        Button4.gameObject.SetActive(false);
        Button5.gameObject.SetActive(false);
        Button6.gameObject.SetActive(false);
    }
    public void On4Button() 
    {
        searchDepth = 4;
        Button2.gameObject.SetActive(false);
        Button3.gameObject.SetActive(false);
        Button4.gameObject.SetActive(false);
        Button5.gameObject.SetActive(false);
        Button6.gameObject.SetActive(false);
    }
    public void On5Button() 
    {
        searchDepth = 5;
        Button2.gameObject.SetActive(false);
        Button3.gameObject.SetActive(false);
        Button4.gameObject.SetActive(false);
        Button5.gameObject.SetActive(false);
        Button6.gameObject.SetActive(false);
    }

    public void On6Button() 
    {
        searchDepth = 6;
        Button2.gameObject.SetActive(false);
        Button3.gameObject.SetActive(false);
        Button4.gameObject.SetActive(false);
        Button5.gameObject.SetActive(false);
        Button6.gameObject.SetActive(false);
    }
    private void Awake()
    {
        Button2.gameObject.SetActive(true);
        Button3.gameObject.SetActive(true);
        Button4.gameObject.SetActive(true);
        Button5.gameObject.SetActive(true);
        Button6.gameObject.SetActive(true);

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    public Move GetBestMove()
    {
        List<Move> moves = GenerateValidMoves(gameManager.currentPlayer); //get the list of moves the AI can do. 

        int bestScore = int.MinValue;
        Move bestMove = null;
        foreach (Move move in moves)
        {
            if (move.piece == null) continue;


            GameObject ogpiece = gameManager.PieceAtGrid(move.destination);
            Player originalOwner =  GetOwner(ogpiece);

            MovePiece(move);

            if (ogpiece != null) //captured pieces
            {
                originalOwner.pieces.Remove(ogpiece);
                gameManager.currentPlayer.capturedPieces.Add(ogpiece);
                ogpiece.SetActive(false);
            }

            int score = Minimax(searchDepth - 1, int.MinValue, int.MaxValue, false); 
                 

            UndoMove(move,ogpiece);

            if (ogpiece != null) //readds any captured pieces
            {
                originalOwner.pieces.Add(ogpiece);
                gameManager.currentPlayer.capturedPieces.Remove(ogpiece);
                ogpiece.SetActive(true);
            }

            if (score > bestScore || bestMove == null)
            {
                bestScore = score;
                bestMove = move;
            }
        }

        if (bestMove != null)
        {
            moveslist.Add(bestMove); // the best move will be added to the list and will be played 
        }

        return bestMove;
    }

    public void MovePiece( Move move)
    {
        gameManager.pieces[move.origin.x, move.origin.y] = null;
        gameManager.pieces[move.destination.x, move.destination.y] = move.piece;

    }

    public void UndoMove(Move move, GameObject piece)
    {
        gameManager.pieces[move.origin.x, move.origin.y] = move.piece;
        gameManager.pieces[move.destination.x, move.destination.y] = piece;

    }
    private List<Move> GenerateValidMoves(Player player)
    {
        List<Move> validMoves = new List<Move>();

        var piecesToCheck = new List<GameObject>(player.pieces);

        foreach (GameObject piece in piecesToCheck)
        {
            if (piece == null) continue;

            Vector2Int currentPos = gameManager.GridForPiece(piece);
            List<Vector2Int> possibleMoves = gameManager.MovesForPiece(piece);

            if (possibleMoves == null) continue;

            foreach (Vector2Int move in possibleMoves)
            {
                GameObject target = gameManager.PieceAtGrid(move);
                if (target != null && player.pieces.Contains(target))
                    continue;

                validMoves.Add(new Move(piece, currentPos, move));
            }
        }

        return validMoves;
    }


   public int Minimax(int depth, int alpha, int beta, bool maximizingPlayer)
{
    if (depth == 0)
        return EvaluateBoard();

        List<Move> moves;
        if (maximizingPlayer)
        {
           moves= GenerateMoves(gameManager.currentPlayer);
        }
        else
           moves= GenerateMoves(GetOpponent());



    int bestEval = int.MaxValue;

    foreach (Move move in moves)
    {
        if (!IsValidGrid(move.origin) || !IsValidGrid(move.destination))
            continue;

        GameObject originalPiece = gameManager.pieces[move.origin.x, move.origin.y];
        GameObject capturedPiece = gameManager.pieces[move.destination.x, move.destination.y];
        Player capturedOwner = capturedPiece != null ? GetOwner(capturedPiece) : null;
            //moving the piece 
        gameManager.pieces[move.origin.x, move.origin.y] = null;
        gameManager.pieces[move.destination.x, move.destination.y] = originalPiece;
          

            if (capturedPiece != null)
        {
            capturedOwner.pieces.Remove(capturedPiece);
            gameManager.currentPlayer.capturedPieces.Add(capturedPiece);
                capturedPiece.SetActive(false);
        }

        int eval = Minimax(depth - 1, alpha, beta, !maximizingPlayer);

            //undo move
        gameManager.pieces[move.origin.x, move.origin.y] = originalPiece;
        gameManager.pieces[move.destination.x, move.destination.y] = capturedPiece;

        if (capturedPiece != null)// undo captures 
        {
            capturedOwner.pieces.Add(capturedPiece);
            gameManager.currentPlayer.capturedPieces.Remove(capturedPiece);
            if (!capturedPiece.activeSelf) 
                capturedPiece.SetActive(true);
        }

        if (maximizingPlayer)
        {
            bestEval = Mathf.Max(bestEval, eval);
            alpha = Mathf.Max(alpha, eval);
        }
        else
        {
            bestEval = Mathf.Min(bestEval, eval);
            beta = Mathf.Min(beta, eval);
        }

        if (beta <= alpha)
            break;
    }

    return bestEval;
}

private bool IsValidGrid(Vector2Int gridPoint)
{
        if (gridPoint.x >= 0 && gridPoint.x < 8 && gridPoint.y >= 0 && gridPoint.y < 8)
            return true;
        else
            return false;
}
    private Player GetOwner(GameObject piece)
    {
        if(gameManager.white.pieces.Contains(piece))
            return gameManager.white;
        else
            return gameManager.black;
    }


    private Player GetOpponent()
    {
        if(gameManager.currentPlayer == gameManager.white) //get the opposite player
        {
            return  gameManager.black;
        }
        else
            return gameManager.white;
    }

  
  
    private List<Move> GenerateMoves(Player player) //gets the list of moves
    {
        List<Move> moves = new List<Move>();

        foreach (GameObject piece in player.pieces)
        {
            Vector2Int currentPos = gameManager.GridForPiece(piece);
            List<Vector2Int> possibleMoves = gameManager.MovesForPiece(piece);

            foreach (Vector2Int move in possibleMoves)
            {
                moves.Add(new Move(piece: piece, origin: currentPos,destination: move  ));
            }
        }
        return moves;
    }
   
    public static int GetPieceValue(GameObject piece)  //the values of each piece
    {
        if (piece.name.Contains("Pawn"))
            return  2000;
        else if (piece.name.Contains("Knight"))
            return 6000;
        else if (piece.name.Contains("Bishop"))
            return 6000;
        else if (piece.name.Contains("Rook"))
            return 9000;
        else if (piece.name.Contains("Queen"))
            return 10000;
        else if (piece.name.Contains("King")) //most valueable pieces-> more likely to defend it 
            return 20000;
        else
            return 0;
    }
  
    public int EvaluateBoard() // this will evaluate the board. -> different situations/
    {
        int score = 0;

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                GameObject piece = gameManager.PieceAtGrid(new Vector2Int(x, y));
                if (piece == null) continue;

                int totalValue = GetPieceValue(piece)+ GetPositionScore(piece, x, y);

                
                score += EvaluatePawnStructure(gameManager.black);
                score -= EvaluatePawnStructure(gameManager.white);

                if (gameManager.black.pieces.Contains(piece))
                    score += totalValue;
                else
                    score -= totalValue;
            }
        }

        return score;
    }

   
    private int EvaluatePawnStructure(Player player) // calculates the amount of pawns within a file. and will lead to a decrease iin score
    {
        int decrease = 0;
        int[] pawnCounts = new int[8]; 

        foreach (GameObject piece in player.pieces)
        {
            if (piece == null || !piece.name.Contains("Pawn"))
                continue;

            else
            {
                Vector2Int pos = gameManager.GridForPiece(piece);
                pawnCounts[pos.x]++; //files -. the amount in a certain file -> lead to penatly 
            }
        }

        for (int i = 0; i < 8; i++)
        {
            if (pawnCounts[i] > 1)
                decrease -= 400 * (pawnCounts[i] - 1);
        }

        return decrease;
    }
  

    private int GetPositionScore(GameObject piece, int x, int y) //positions for each part of the 
    {
        Piece pieceComponent = piece.GetComponent<Piece>();
        bool blackPlayer = gameManager.black.pieces.Contains(piece);

        int Y = y;
        if (blackPlayer == true)
            Y = 7 - y;

        if (moveslist.Count < 15) //opening
        {
            switch (pieceComponent.type)
            {
                case PieceType.Pawn:
                    return pawnPositionScoresOpening[Y, x];
                case PieceType.Knight:
                    return knightPositionScoresOpening[Y, x];
                case PieceType.Bishop:
                    return bishopPositionScoresOpening[Y, x]; 
                case PieceType.Rook:
                    return rookPositionScoresOpening[Y, x]; 
                case PieceType.Queen:
                    return queenPositionScoresOpening[Y, x]; 
                case PieceType.King:
                    return kingPositionScoresOpening[Y, x];
                default:
                    return 0;
            }
        }
        else if(moveslist.Count< 30) //middle game
        {
            switch (pieceComponent.type)
            {
                case PieceType.Pawn:
                    return pawnPositionScoresMiddleGame[Y, x];
                case PieceType.Knight:
                    return knightPositionScoresMiddleGame[Y, x];
                case PieceType.Rook:
                    return rookPositionScoresMiddleGame[Y, x]; 
                case PieceType.Queen:
                    return queenPositionScoresMiddleGame[Y, x];
                case PieceType.King:
                    return kingPositionScoresMiddleGame[Y, x];
                default:
                    return 0;
            }
        } else //endgame
        {
            switch (pieceComponent.type)
            {
                case PieceType.Pawn:
                    return pawnPositionScoresEndGame[Y, x];
                case PieceType.Knight:
                    return knightPositionScoresEndGame[Y, x];
                case PieceType.Rook:
                    return rookPositionScoresEndGame[Y, x];
                case PieceType.Queen:
                    return queenPositionScoresEndGame[Y, x]; 
                default:
                    return 0;
            }
        }
    }
}


