using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMZMapGenerator.Core
{
    public static class TextureGenerator
    {

        public static Texture2D TextureFromColourMap(Color[] colourMap, int width, int height)
        {
            Texture2D texture = new Texture2D(width, height);
            texture.filterMode = FilterMode.Point;
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.SetPixels(colourMap);
            texture.Apply();
            return texture;
        }


        public static Texture2D TextureFromHeightMap(MapData mapData)
        {
            int width = mapData.lerpNoise.GetLength(0);
            int height = mapData.lerpNoise.GetLength(1);

            Color[] colourMap = new Color[width * height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, mapData.lerpNoise[x, y]);
                }
            }

            return TextureFromColourMap(colourMap, width, height);
        }

    }
}