using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMZMapGenerator.Core
{
    public static class Noise
    {
        public static Tuple<float[,], float, float> GenerateNoise(int size, MapGeneratorSettings settings, Vector2 sampleCenter)
        {
            float[,] noiseMap = new float[size, size];

            System.Random prng = new System.Random(settings.seed == 0 ? DateTime.Now.Millisecond : settings.seed);
            Vector2[] octavesOffsets = new Vector2[settings.octaves];

            float maxPossibleHeight = 0;
            float amplitude = 1;
            float frequency = 1;

            for (int i = 0; i < settings.octaves; i++)
            {
                float offsetX = prng.Next(-100000, 100000) + settings.offset.x + sampleCenter.x;
                float offsetY = prng.Next(-100000, 100000) + settings.offset.y + sampleCenter.y;
                octavesOffsets[i] = new Vector2(offsetX, offsetY);
                maxPossibleHeight += amplitude;
                amplitude *= settings.persistance;
            }

            float maxNoiseHeight = float.MinValue;
            float minNoiseHeight = float.MaxValue;

            float halfSize = size / 2f;
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    amplitude = 1;
                    frequency = 1;
                    float noiseHeight = 0;

                    for (int i = 0; i < settings.octaves; i++)
                    {
                        float sampleX = (x - halfSize + octavesOffsets[i].x) / settings.noiseScale * frequency;
                        float sampleY = (y - halfSize + octavesOffsets[i].y) / settings.noiseScale * frequency;
                        float perlinNoise = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                        noiseHeight += perlinNoise * amplitude;
                        amplitude *= settings.persistance;
                        frequency *= settings.lacunarity;
                    }

                    if (noiseHeight > maxNoiseHeight)
                    {
                        maxNoiseHeight = noiseHeight;
                    }

                    if (noiseHeight < minNoiseHeight)
                    {
                        minNoiseHeight = noiseHeight;
                    }

                    noiseMap[x, y] = noiseHeight;
                }
            }
            return new Tuple<float[,], float, float>(noiseMap, minNoiseHeight, maxNoiseHeight);
        }
    }
}