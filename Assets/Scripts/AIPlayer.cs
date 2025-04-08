using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    private GameManager gameManager;
    public AI call; 
    void Start()
    {
        call = AI.instance;
        gameManager = GameManager.instance;
    }
    public void MakeBestMove()
    {
        if (gameManager.currentPlayer == gameManager.black)
        {
            Move bestMove = call.GetBestMove();
            
                gameManager.Move(bestMove.piece, bestMove.destination);
                gameManager.NextPlayer();
            
        }
    }
}

