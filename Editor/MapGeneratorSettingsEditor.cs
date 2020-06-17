using UnityEditor;
using UnityEngine;
using LMZMapGenerator.Core;

namespace LMZMapGenerator.Utilities
{
    [CustomEditor(typeof(MapGeneratorSettings), true)]
    public class MapGeneratorSettingsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            MapGeneratorSettings data = (MapGeneratorSettings)target;
            if (GUILayout.Button("Update"))
            {
                data.NotifyOfUpdatedValues();
            }
            if (GUILayout.Button("Clear"))
            {
                data.NotifyOfClear();
            }
        }
    }
}