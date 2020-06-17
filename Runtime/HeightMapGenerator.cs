using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LMZMapGenerator.Core
{
    public static class HeightMapGenerator
    {
        public static MapData GenerateHeightMap(MapGeneratorSettings settings, Vector2 sampleCenter)
        {
            PositionedTile positionedTiles = new PositionedTile(settings.mapSize * settings.mapSize);
            var falloffResult = FalloffGenerator.GenerateFalloff(settings.mapSize, settings);
            var falloff = falloffResult.Item1;
            var minFalloff = falloffResult.Item2;
            var maxFalloff = falloffResult.Item3;
            var lerpNoise = new float[settings.mapSize, settings.mapSize];
            if (settings.showFalloffOnly)
            {
                for (int y = 0; y < settings.mapSize; y++)
                {
                    for (int x = 0; x < settings.mapSize; x++)
                    {
                        var index = y * settings.mapSize + x;

                        lerpNoise[x, y] = Mathf.InverseLerp(maxFalloff, minFalloff, falloff[x, y]);
                        var roundX = Mathf.RoundToInt(sampleCenter.x) + x;
                        var roundY = Mathf.RoundToInt(sampleCenter.y) + y;
                        var tileBase = settings.GetTileHeight(lerpNoise[x, y]);
                        positionedTiles.Add(index, roundX, roundY, tileBase);
                    }
                }
                return new MapData(falloff, lerpNoise, positionedTiles);
            }

            var noiseResult = Noise.GenerateNoise(settings.mapSize, settings, sampleCenter);
            var rawNoise = noiseResult.Item1;
            var min = noiseResult.Item2;
            var max = noiseResult.Item3;
            for (int x = 0; x < settings.mapSize; x++)
            {
                for (int y = 0; y < settings.mapSize; y++)
                {
                    lerpNoise[x, y] = Mathf.InverseLerp(min, max, rawNoise[x, y]) - falloff[x, y];
                    var tileBase = settings.GetTileHeight(lerpNoise[x, y]);
                    var index = y * settings.mapSize + x;
                    var roundX = Mathf.RoundToInt(sampleCenter.x) + x;
                    var roundY = Mathf.RoundToInt(sampleCenter.y) + y;
                    positionedTiles.Add(index, roundX, roundY, tileBase);
                }
            }
            return new MapData(rawNoise, lerpNoise, positionedTiles);
        }
    }



    public struct MapData
    {
        public readonly float[,] rawNoise;
        public readonly float[,] lerpNoise;
        public readonly PositionedTile positionedTiles;
        public MapData(float[,] rawNoise, float[,] lerpNoise, PositionedTile positionedTiles)
        {
            this.rawNoise = rawNoise;
            this.lerpNoise = lerpNoise;
            this.positionedTiles = positionedTiles;
        }
    }
}