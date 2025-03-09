

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
    public GameObject tileHighlightPrefab;

    private GameObject tileHighlight;
    private bool active = false;
    private GameManager gameManager;



    void Start ()
    {
        gameManager = GameManager.instance;

        Vector2Int gridPoint = Geometry.GridPoint(0, 0);
        Vector3 point = Geometry.PointFromGrid(gridPoint);
        tileHighlight = Instantiate(tileHighlightPrefab, point, Quaternion.identity, gameObject.transform);
        tileHighlight.SetActive(false);
    }

    void Update()
    {
        if (gameManager.currentPlayer.name == "white" /*&& gameManager.isAIPlayer== true*/)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 point = hit.point;
                Vector2Int gridPoint = Geometry.GridFromPoint(point);

                tileHighlight.SetActive(true);
                tileHighlight.transform.position = Geometry.PointFromGrid(gridPoint);
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject selectedPiece = GameManager.instance.PieceAtGrid(gridPoint); // check if activated 
                    if (GameManager.instance.DoesPieceBelongToCurrentPlayer(selectedPiece))
                    {
                        GameManager.instance.SelectPiece(selectedPiece);
                        ExitState(selectedPiece);
                    }

                }
            }
            else
            {
                tileHighlight.SetActive(false);
            }
        }
    }

        public void EnterState()
        {
            enabled = true;
        }

        private void ExitState(GameObject movingPiece)
        {
            this.enabled = false;
            tileHighlight.SetActive(false);
            MoveSelector move = GetComponent<MoveSelector>();
            move.EnterState(movingPiece);
        }
    
}
