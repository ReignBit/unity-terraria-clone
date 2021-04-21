using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapInteractor : MonoBehaviour
{
    [SerializeField]
    Camera main;

    [SerializeField]
    GameObject cursorGameObject;
    [SerializeField]
    BuildCursorCollisionCheck cursorCollisionChecker;
    Vector3Int cursorPos;

    [SerializeField]
    Tilemap foregroundTilemap;

    [SerializeField]
    Tile[] availableTiles = new Tile[3];


    // Update is called once per frame
    void Update()
    {
        SetCursorPosition();

        if (Input.GetMouseButton(0))
        {
            if (CanBuildHere())
            {
                foregroundTilemap.SetTile(cursorPos, availableTiles[UnityEngine.Random.Range(0, availableTiles.Length)]);
            }
        }
        else if (Input.GetMouseButton(1))
        {
            foregroundTilemap.SetTile(cursorPos, null);
        }

    }

    bool GetSupportedStatus(Vector3Int tilePos)
    {
        if (foregroundTilemap.HasTile(tilePos + -Vector3Int.right))
        {
            return true;
        }
        else if (foregroundTilemap.HasTile(tilePos + Vector3Int.right))
        {
            return true;
        }
        else if (foregroundTilemap.HasTile(tilePos + Vector3Int.up))
        {
            return true;
        }
        else if (foregroundTilemap.HasTile(tilePos + -Vector3Int.up))
        {
            return true;
        }
        return false;
    }

    void SetCursorPosition()
    {

        Vector3 mousePos = Input.mousePosition;
        //Debug.Log(mousePos);
        Vector3 worldMousePos = main.ScreenToWorldPoint(mousePos);
        worldMousePos.z = 0;

        cursorGameObject.transform.position = new Vector3(
            Mathf.Round(worldMousePos.x),
            Mathf.Round(worldMousePos.y),
            worldMousePos.z
        );
        // Using Vector3.RountToInt casues rounding issues with selecting Tiles,
        // Here we are using the same method as above to get the correct tile.
        cursorPos = new Vector3Int(
            (int) Mathf.Round(worldMousePos.x),
            (int) Mathf.Round(worldMousePos.y),
            (int) worldMousePos.z);
    }


    public bool CanBuildHere()
    {
        Debug.Log(String.Format("Supported: {0} | Has Tile: {1} | Site Clear: {2}", GetSupportedStatus(cursorPos), foregroundTilemap.HasTile(cursorPos), cursorCollisionChecker.isBuildSiteClear));
        return (GetSupportedStatus(cursorPos) || foregroundTilemap.HasTile(cursorPos)) && cursorCollisionChecker.isBuildSiteClear;
    }

}