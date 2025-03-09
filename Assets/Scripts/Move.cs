using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{ //needed for the AI script and the minmax function. it gives the piece and it need locations
    public Vector2Int origin;        
    public Vector2Int destination;
    public GameObject piece;

    public Move(GameObject piece, Vector2Int origin, Vector2Int destination)
    {
        this.piece = piece;
        this.origin = origin;
        this.destination = destination;
    }

}
