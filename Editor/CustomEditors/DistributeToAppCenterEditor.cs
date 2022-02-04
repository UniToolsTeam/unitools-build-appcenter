using UnityEditor;
using UnityEngine;

namespace UniTools.Build.AppCenter
{
    [CustomEditor(typeof(DistributeToAppCenter))]
    public sealed class DistributeToAppCenterEditor : Editor
    {
        private AppCenterCli m_cli = default;

        private SerializedProperty m_apiToken = default;
        private SerializedProperty m_appName = default;

        private SerializedProperty m_group = default;
        private SerializedProperty m_builtFilePath = default;

        private void OnEnable()
        {
            m_cli = Cli.Tool<AppCenterCli>();
            m_apiToken = serializedObject.FindProperty(nameof(m_apiToken));
            m_appName = serializedObject.FindProperty(nameof(m_appName));

            m_group = serializedObject.FindProperty(nameof(m_group));
            m_builtFilePath = serializedObject.FindProperty(nameof(m_builtFilePath));
        }

        public override void OnInspectorGUI()
        {
            if (!IsCliInstalled())
            {
                return;
            }

            if (!IsTokenCreated())
            {
                return;
            }

            SetAppName();
            EditorGUILayout.PropertyField(m_group);
            EditorGUILayout.PropertyField(m_builtFilePath);

            serializedObject.ApplyModifiedProperties();
        }

        private bool IsCliInstalled()
        {
            if (m_cli == null || !m_cli.IsInstalled)
            {
                bool check = false;
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.HelpBox("AppCenter CLI is not installed!", MessageType.Error);
                    check = GUILayout.Button("Check");
                }
                EditorGUILayout.EndHorizontal();

                if (check)
                {
                    SettingsService.OpenProjectSettings($"Project/{nameof(UniTools)}/CLI");
                }

                return false;
            }

            return true;
        }

        private bool IsTokenCreated()
        {
            EditorGUILayout.PropertyField(m_apiToken);
            if (string.IsNullOrWhiteSpace(m_apiToken.stringValue))
            {
                bool create = false;
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.HelpBox("Api toke is missing. Please create a toke with Full Access.", MessageType.Error);
                    create = GUILayout.Button("Create");
                }
                EditorGUILayout.EndHorizontal();

                if (create)
                {
                    Application.OpenURL("https://docs.microsoft.com/en-us/appcenter/api-docs/#creating-an-app-center-app-api-token");
                }

                return false;
            }

            return true;
        }

        private void SetAppName()
        {
            Color c = GUI.color;
            if (string.IsNullOrWhiteSpace(m_appName.stringValue))
            {
                GUI.color = Color.red;
            }

            bool find = false;
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.PropertyField(m_appName);
                GUI.color = c;
                find = GUILayout.Button("Find");
            }
            EditorGUILayout.EndHorizontal();

            if (find)
            {
                string command = $"apps list --token {m_apiToken.stringValue}";
                ToolResult result = m_cli.Execute(command);
                if (result.ExitCode == 0)
                {
                    EditorUtility.DisplayDialog("All apps", result.Output, "Thanks");
                    Debug.Log($"Get apps result: {result}");
                }
                else
                {
                    EditorUtility.DisplayDialog("Error!", $"Failed to get apps due to {result.Error}", "Oops");
                    Debug.LogError(result);
                }
            }
        }
    }
}