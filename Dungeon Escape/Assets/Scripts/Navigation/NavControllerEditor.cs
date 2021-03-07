using System;
using System.IO;
using Dungeon.Navigation.Waypoints;
using UnityEditor;

namespace Dungeon.Navigation
{
    [CustomEditor(typeof(NavController))]
    public class NavControllerEditor : Editor
    {
        private SerializedProperty _usePath;
        private SerializedProperty _patrolPath;
        private SerializedProperty _toleranceDistance;

        // is called once when the object gains focus in hierarchy
        private void OnEnable()
        {
            _usePath = serializedObject.FindProperty("usePath");
            _patrolPath = serializedObject.FindProperty("_patrolPath");
            _toleranceDistance = serializedObject.FindProperty("_toleranceDistance");
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            serializedObject.Update();

            EditorGUILayout.PropertyField(_usePath);

            if (_usePath.boolValue)
            {
                EditorGUILayout.PropertyField(_patrolPath);
                EditorGUILayout.PropertyField(_toleranceDistance);
            }
            else
            {
                _patrolPath.objectReferenceValue = null;
                _toleranceDistance.floatValue = 0.1f;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}