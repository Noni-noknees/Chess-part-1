using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;
        InvokeRepeating("MakeRandomMove", 1f, 2f); 
    }
    // this script allows for the AI to make random moves.
    public void MakeRandomMove()
    {
        if (gameManager.currentPlayer == gameManager.black) // the AI is in control of the Black pieces. it checks if the player is the AI since I made it so the AI control the black pieces
        {
          
           List<GameObject> aiPieces = gameManager.currentPlayer.pieces;  
            GameObject randomPiece = aiPieces[Random.Range(0, aiPieces.Count)]; //this allows for a random piece to be selected
            List<Vector2Int> possibleMoves = gameManager.MovesForPiece(randomPiece); //this check for the possible moves for the piece selected
            while (possibleMoves.Count == 0) //to ensure that the piece choosen has posisble moves, if not it will go through the loop until a piece is choosen
            {
                randomPiece = aiPieces[Random.Range(0, aiPieces.Count)];
                possibleMoves = gameManager.MovesForPiece(randomPiece);
            }
            Vector2Int randomMove = possibleMoves[Random.Range(0, possibleMoves.Count)]; //they picked the moved, then it return and then the turn is changed to white/back to the player
            gameManager.Move(randomPiece, randomMove);
            gameManager.NextPlayer();
        }
        else
            return;
    }
    
}
