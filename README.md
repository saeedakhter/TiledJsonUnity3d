# TiledJsonUnity3d

Parse json files from the Tiled Map Editor in Unity3d

## Usage

Using this library is pretty simple:

1. Save a .json map file from the Tiled Map Editor and save it in a folder called `Resources`.
1. Call the static function `TiledMap.ReadMap(resourceName)` which will return a `TiledMap` object.
1. `TiledMap.GetTiles()` will return all of the tiles
1. Enumerate the tiles and create instantiate your GameObjects in Unity3d (not part of the demo scene yet)

```C#
using UnityEngine;
using TiledJsonUtility;
public class SampleUsage : MonoBehaviour
{
    public string TextAssetName;
    public void Start()
    {
        TiledMap map = TiledMap.ReadMap(TextAssetName);
        foreach (TiledLayer layer in map.layers)
        {
            TiledLayerTile[] tiles = layer.GetTiles();
            if (tiles != null)
            {
                foreach (TiledLayerTile tile in tiles)
                {
                    Debug.LogFormat("Tile({0},{1}) = {2} H={3} V={4} D={5}", tile.X, tile.Y, tile.Gid, tile.IsFlippedHorz, tile.IsFlippedVert, tile.IsFlippedDiag);
                }
            }
        }
    }
}
```
