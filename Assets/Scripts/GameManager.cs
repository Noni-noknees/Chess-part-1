﻿/*
 * Copyright (c) 2018 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Board board;

    public GameObject whiteKing;
    public GameObject whiteQueen;
    public GameObject whiteBishop;
    public GameObject whiteKnight;
    public GameObject whiteRook;
    public GameObject whitePawn;

    public GameObject blackKing;
    public GameObject blackQueen;
    public GameObject blackBishop;
    public GameObject blackKnight;
    public GameObject blackRook;
    public GameObject blackPawn;

    public GameObject[,] pieces;
    private List<GameObject> movedPawns;

    public Player white;
    public Player black;
    public Player currentPlayer;
    public Player otherPlayer;
    public AIPlayer protoype;
    public bool isAIPlayer = true;
    public Button PVPButton;
    public Button AIButton;
    public Button Button2, Button3, Button4, Button5, Button6;
    public TMP_Text Chess;
    public GameObject backdrop;


    void Awake()
    {
        instance = this;
        Chess.gameObject.SetActive(true);
        PVPButton.gameObject.SetActive(true);
        AIButton.gameObject.SetActive(true);
        backdrop.gameObject.SetActive(true);

    }

    void Start()
    {

        pieces = new GameObject[8, 8];
        movedPawns = new List<GameObject>();
        white = new Player("white", true);
        black = new Player("black", false);
        otherPlayer = black; // AI
        currentPlayer = white;// real player       
        InitialSetup();
    }


    public void OnPVPButton() 
    {
        isAIPlayer = false;
        Chess.gameObject.SetActive(false);
        PVPButton.gameObject.SetActive(false);
        PVPButton.gameObject.SetActive(false);
        AIButton.gameObject.SetActive(false);
        backdrop.gameObject.SetActive(false);
        Button2.gameObject.SetActive(false);
        Button3.gameObject.SetActive(false);
        Button4.gameObject.SetActive(false);
        Button5.gameObject.SetActive(false);
        Button6.gameObject.SetActive(false);

    }
    public void OnAIButton() //
    {
        Chess.gameObject.SetActive(false);
        PVPButton.gameObject.SetActive(false);
        PVPButton.gameObject.SetActive(false);
        AIButton.gameObject.SetActive(false);
        backdrop.gameObject.SetActive(false);
        Button2.gameObject.SetActive(false);
        Button3.gameObject.SetActive(false);
        Button4.gameObject.SetActive(false);
        Button5.gameObject.SetActive(false);
        Button6.gameObject.SetActive(false);
    }

    private void InitialSetup()
    {
        AddPiece(whiteRook, white, 0, 0);
        AddPiece(whiteKnight, white, 1, 0);
        AddPiece(whiteBishop, white, 2, 0);
        AddPiece(whiteQueen, white, 3, 0);
        AddPiece(whiteKing, white, 4, 0);
        AddPiece(whiteBishop, white, 5, 0);
        AddPiece(whiteKnight, white, 6, 0);
        AddPiece(whiteRook, white, 7, 0);

        for (int i = 0; i < 8; i++)
        {
            AddPiece(whitePawn, white, i, 1);
        }

        AddPiece(blackRook, black, 0, 7);
        AddPiece(blackKnight, black, 1, 7);
        AddPiece(blackBishop, black, 2, 7);
        AddPiece(blackQueen, black, 3, 7);
        AddPiece(blackKing, black, 4, 7);
        AddPiece(blackBishop, black, 5, 7);
        AddPiece(blackKnight, black, 6, 7);
        AddPiece(blackRook, black, 7, 7);

        for (int i = 0; i < 8; i++)
        {
            AddPiece(blackPawn, black, i, 6);
        }
    }

    public void AddPiece(GameObject prefab, Player player, int col, int row)
    {
        GameObject pieceObject = board.AddPiece(prefab, col, row);
        player.pieces.Add(pieceObject);
        pieces[col, row] = pieceObject;
    }

    public void SelectPieceAtGrid(Vector2Int gridPoint)
    {
        GameObject selectedPiece = pieces[gridPoint.x, gridPoint.y];
        if (selectedPiece)
        {
            board.SelectPiece(selectedPiece);
        }
    }

    public List<Vector2Int> MovesForPiece(GameObject pieceObject) //this presents all the location for the pieces
    {
        Piece piece = pieceObject.GetComponent<Piece>();
        Vector2Int gridPoint = GridForPiece(pieceObject);
        List<Vector2Int> locations = piece.MoveLocations(gridPoint);

        locations.RemoveAll(gp => gp.x < 0 || gp.x > 7 || gp.y < 0 || gp.y > 7);

        locations.RemoveAll(gp => FriendlyPieceAt(gp));

        if (piece.type == PieceType.King)
        {
            AddCastlingMoves(piece, gridPoint, locations); //castling 
        }

        return locations;
    }
  
    private void AddCastlingMoves(Piece kingPiece, Vector2Int gridPoint, List<Vector2Int> locations) 
    {
        if (kingPiece.Moved) //checking if the king moved since it cannot move in order for castling to occur
            return;

        if (!HasPieceAt(gridPoint.x + 1, gridPoint.y) && !HasPieceAt(gridPoint.x + 2, gridPoint.y)) 
        {
            GameObject rook = PieceAtGrid(new Vector2Int(7, gridPoint.y));
            if (rook != null && rook.GetComponent<Piece>().type == PieceType.Rook && !rook.GetComponent<Piece>().Moved)
            {
                locations.Add(new Vector2Int(gridPoint.x + 2, gridPoint.y));  //adds to the list

            }
        }

        if (!HasPieceAt(gridPoint.x - 1, gridPoint.y) && !HasPieceAt(gridPoint.x - 2, gridPoint.y) && !HasPieceAt(gridPoint.x - 3, gridPoint.y))//checks if there is any pieces between king and rook
        {
            GameObject rook = PieceAtGrid(new Vector2Int(0, gridPoint.y));
            if (rook != null && rook.GetComponent<Piece>().type == PieceType.Rook && !rook.GetComponent<Piece>().Moved)
            {
                locations.Add(new Vector2Int(gridPoint.x - 2, gridPoint.y));              }
        }
    }
    public bool HasPieceAt(int x, int y)
    {
        return PieceAtGrid(new Vector2Int(x, y)) != null;
    }

    public void Move(GameObject piece, Vector2Int gridPoint)
    {
        Piece pieceComponent = piece.GetComponent<Piece>();
        pieceComponent.Moved = true;

        if (pieceComponent.type == PieceType.Pawn && !HasPawnMoved(piece))
        {
            movedPawns.Add(piece); 
        }

        Vector2Int startGridPoint = GridForPiece(piece);
        GameObject pieceAtDestination = PieceAtGrid(gridPoint);
        if (pieceAtDestination != null && !FriendlyPieceAt(gridPoint))
        {
            CapturePieceAt(gridPoint); 
        }

        pieces[startGridPoint.x, startGridPoint.y] = null;
        pieces[gridPoint.x, gridPoint.y] = piece;
        board.MovePiece(piece, gridPoint);

        if (pieceComponent.type == PieceType.King && Mathf.Abs(startGridPoint.x - gridPoint.x) == 2)
        {
            int rookStartX = gridPoint.x == 2 ? 0 : 7;
            int rookEndX = gridPoint.x == 2 ? 3 : 5;

            GameObject rook = PieceAtGrid(new Vector2Int(rookStartX, startGridPoint.y));
            pieces[rookStartX, startGridPoint.y] = null;
            pieces[rookEndX, startGridPoint.y] = rook;
            board.MovePiece(rook, new Vector2Int(rookEndX, startGridPoint.y));
        }

        CheckPawnPromotion(piece, gridPoint);
        
    }



    public bool HasPawnMoved(GameObject pawn)
    {
        return movedPawns.Contains(pawn);
    }
    private void CheckPawnPromotion(GameObject pawn, Vector2Int gridPoint)
    {
        Piece pieceComponent = pawn.GetComponent<Piece>();

        if (pieceComponent.type == PieceType.Pawn)
        {
            if (gridPoint.y == 7)
            {
                PromotePawn(pawn, gridPoint);
            }
            else if (gridPoint.y == 0)
            {
                PromotePawn(pawn, gridPoint);
            }
        }
    }

    private void PromotePawn(GameObject pawn, Vector2Int gridPoint) //promotion. need to fix 
    {
        if (pawn.name == "white")
        {
            AddPiece(whiteQueen, white, gridPoint.x, gridPoint.y);
        }
        else
            AddPiece(blackQueen, black, gridPoint.x, gridPoint.y);

        Destroy(pawn);

    }


   public void CapturePieceAt(Vector2Int gridPoint)
{
    GameObject piececaptured = PieceAtGrid(gridPoint);
    if (piececaptured == null) return;

        Player owner;
        if (white.pieces.Contains(piececaptured))
            owner= white;
        else 
            owner= black;

        if (owner == null) return;

    pieces[gridPoint.x, gridPoint.y] = null;
    
    owner.pieces.Remove(piececaptured);
    
    currentPlayer.capturedPieces.Add(piececaptured);
    
    Destroy(piececaptured);
    
}
    public void SelectPiece(GameObject piece)
    {
        board.SelectPiece(piece);
    }

    public void DeselectPiece(GameObject piece)
    {
        board.DeselectPiece(piece);
    }

    public bool DoesPieceBelongToCurrentPlayer(GameObject piece)
    {
        return currentPlayer.pieces.Contains(piece);
    }

    public GameObject PieceAtGrid(Vector2Int gridPoint)
    {
        if (gridPoint.x < 0 || gridPoint.x > 7 || gridPoint.y < 0 || gridPoint.y > 7)
            return null;
        return pieces[gridPoint.x, gridPoint.y];
    }


    public Vector2Int GridForPiece(GameObject piece)
    {
        if (piece == null) return new Vector2Int(-1, -1);

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (pieces[i, j] == piece)
                {
                    return new Vector2Int(i, j);
                }
            }
        }
        return new Vector2Int(-1, -1); 
    }

    public bool FriendlyPieceAt(Vector2Int gridPoint)
    {
        GameObject piece = PieceAtGrid(gridPoint);

        if (piece == null)
        {
            return false;
        }

        if (otherPlayer.pieces.Contains(piece))
        {
            return false;
        }

        return true;
    }




    public void NextPlayer()
    {
        Player temp = currentPlayer;
        currentPlayer = otherPlayer;
        otherPlayer = temp;

        // If it's AI's turn and  is enabled
        if (currentPlayer == black && isAIPlayer)
        {
            AIPlayer aiPlayer = FindObjectOfType<AIPlayer>();
                aiPlayer.MakeBestMove();
        }
    }
}