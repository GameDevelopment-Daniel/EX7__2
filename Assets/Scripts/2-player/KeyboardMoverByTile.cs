using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

/**
 * This component allows the player to move by clicking the arrow keys,
 * but only if the new position is on an allowed tile.
 */
public class KeyboardMoverByTile: KeyboardMover {
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;
    private TilemapGraph tilemapGraph = null;
    bool check = false;



    private void Start()
    {
        

    }
    private TileBase TileOnPosition(Vector3 worldPosition) {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }
    private IEnumerator changePos()
    {
        Debug.Log("changePos before: " + transform.position);
        tilemapGraph = new TilemapGraph(tilemap, allowedTiles.Get());
        while (!BFS.checkPos(tilemapGraph, tilemap.WorldToCell(transform.position))) // until we find pos with 100 tiles we can get to
        {
            Debug.Log("changePos after: "+transform.position);
            transform.position = new Vector3(
                            Mathf.RoundToInt(UnityEngine.Random.Range(10f, 90f))+0.5f,
                            Mathf.RoundToInt(UnityEngine.Random.Range(10f, 90f))+0.5f,
                            0);
            yield return new WaitForSeconds(1.0f); // add delay of 0.5 seconds here
        }
        Debug.Log("changePos end we got true: " + transform.position);
        check = true;
    }
    void FixedUpdate()
    {
        if (!check)
        {
            StartCoroutine(changePos());
        }
    }
    void Update()  {
        
        Vector3 newPosition = NewPosition();
        TileBase tileOnNewPosition = TileOnPosition(newPosition);
        if (allowedTiles.Contain(tileOnNewPosition)) {
            transform.position = newPosition;
        } else {
            Debug.Log("You cannot walk on " + tileOnNewPosition + "!");
        }
    }
}
