using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public GameObject piece;
    public Vector2Int origin;
    public Vector2Int destination;
    public GameObject capturedPiece; // hold any pieces that are captured just in case we need to undo a move

    public Move(GameObject piece, Vector2Int origin, Vector2Int destination, GameObject capturedPiece = null)
    {
        this.piece = piece;
        this.origin = origin;
        this.destination = destination;
        this.capturedPiece = capturedPiece;
    }
}