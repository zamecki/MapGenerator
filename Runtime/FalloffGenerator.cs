using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LMZMapGenerator.Core
{
    public static class FalloffGenerator
    {
        public static float GetFalloff(int x, int y, int size, MapGeneratorSettings settings)
        {
            float _x = x / (float)size * 2 - 1;
            float _y = y / (float)size * 2 - 1;
            float value = Mathf.Max(Mathf.Abs(_x), Mathf.Abs(_y));
            return Evaluate(value, settings);
        }

        public static Tuple<float[,], float, float> GenerateFalloff(int size, MapGeneratorSettings settings)
        {
            float maxHeight = float.MinValue;
            float minHeight = float.MaxValue;
            var result = new float[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    float _x = i / (float)size * 2 - 1;
                    float _y = j / (float)size * 2 - 1;
                    float value = Mathf.Max(Mathf.Abs(_x), Mathf.Abs(_y));
                    result[i, j] = Evaluate(value, settings);
                    if (result[i, j] > maxHeight)
                    {
                        maxHeight = result[i, j];
                    }

                    if (result[i, j] < minHeight)
                    {
                        minHeight = result[i, j];
                    }

                }
            }
            return new Tuple<float[,], float, float>(result, minHeight, maxHeight);
        }
        
        static float Evaluate(float value, MapGeneratorSettings settings)
        {
            float a = settings.fallOffA;
            float b = settings.fallOffB;
            return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value, a));
        }
    }
}