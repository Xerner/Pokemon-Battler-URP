using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace KevinCastejon.Tools
{
    public class StateMachineGenerator : EditorWindow
    {
        string prefixName = "";
        List<string> states = new List<string>();
        int defaultState = 0;
        bool useRegions = false;
        bool spaceRegions = false;
        bool groupByPhase = false;

        bool codeStyleOpen = false;

        static GUIStyle ErrorStyle;
        static GUIStyle BoldStyle;
        static TextInfo TInfo;

        [MenuItem("Window/StateMachineGenerator")]
        public static void ShowWindow()
        {
           EditorWindow window = GetWindow(typeof(StateMachineGenerator));
            window.minSize = new Vector2(550, 300);
            window.titleContent = new GUIContent("StateMachine Generator");
        }

        private void Awake()
        {
        }

        void OnGUI()
        {
            ErrorStyle = new GUIStyle(EditorStyles.label);
            BoldStyle = new GUIStyle(EditorStyles.label);
            ErrorStyle.normal.textColor = Color.red;
            BoldStyle.fontStyle = FontStyle.Bold;
            TInfo = new CultureInfo("en-US", false).TextInfo;
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("    Naming parameters", BoldStyle);
            EditorGUILayout.Space(5);
            prefixName = EditorGUILayout.TextField("Prefix name", prefixName.Trim()).Replace(" ", "_");
            if (prefixName.Length > 0)
            {
                prefixName = prefixName[0].ToString().ToUpper() + prefixName.Substring(1);
            }
            EditorGUILayout.Space(5);
            if (prefixName.Length > 0)
            {
                EditorGUILayout.BeginHorizontal();
                int newCount = Mathf.Max(0, EditorGUILayout.DelayedIntField("Number of states", states.Count));
                if (GUILayout.Button("-"))
                {
                    newCount--;
                }
                if (GUILayout.Button("+"))
                {
                    newCount++;
                }
                EditorGUILayout.EndHorizontal();
                while (newCount < states.Count)
                    states.RemoveAt(states.Count - 1);
                while (newCount > states.Count)
                    states.Add("    STATE_NAME_" + states.Count);
                for (int i = 0; i < states.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    states[i] = EditorGUILayout.TextField("  State " + i, states[i]).Trim().ToUpper().Replace(" ", "_");
                    bool isDefault = EditorGUILayout.ToggleLeft(defaultState == i ? "Default" : "", defaultState == i);
                    if (isDefault)
                    {
                        defaultState = i;
                    }
                    EditorGUILayout.EndHorizontal();
                }
                if (defaultState >= states.Count)
                {
                    defaultState = 0;
                }
                if (newCount > 0)
                {
                    EditorGUILayout.Space(5);
                    codeStyleOpen = EditorGUILayout.Foldout(codeStyleOpen, "Code styling parameters");
                    if (codeStyleOpen)
                    {
                        EditorGUILayout.Space(5);
                        EditorGUILayout.BeginHorizontal();
                        useRegions = EditorGUILayout.Toggle("Use regions", useRegions);
                        if (useRegions)
                        {
                            spaceRegions = EditorGUILayout.Toggle("Space regions", spaceRegions);
                        }
                        else
                        {
                            spaceRegions = false;
                        }
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.Space(5);
                        groupByPhase = !EditorGUILayout.ToggleLeft(new GUIContent("Group by state", "Methods will be grouped by state"), !groupByPhase);
                        groupByPhase = EditorGUILayout.ToggleLeft(new GUIContent("Group by phase", "Methods will be grouped by phase (enter/update/exit)"), groupByPhase);
                    }
                    if (!FileExists())
                    {
                        EditorGUILayout.Space(5);
                        if (GUILayout.Button("Generate"))
                        {
                            Generate();
                        }
                    }
                }
                if (prefixName.Length > 0)
                {
                    EditorGUILayout.Space(10);
                    EditorGUILayout.LabelField("The file " + prefixName + "StateMachine.cs will be generated");
                    EditorGUILayout.Space(10);
                    if (FileExists())
                    {
                        EditorGUILayout.HelpBox("This file already exists!", MessageType.Error);
                        EditorGUILayout.Space(10);
                    }

                }
            }
        }

        bool FileExists()
        {
            string copyPath = Application.dataPath + "/" + prefixName + "StateMachine.cs";
            return (File.Exists(copyPath));
        }

        void Generate()
        {
            if (!(prefixName.Length > 0 || states.Count > 0))
            {
                return;
            }
            string copyPath = Application.dataPath + "/" + prefixName + "StateMachine.cs";
            if (!FileExists())
            { // do not overwrite
                using (StreamWriter outfile =
                    new StreamWriter(copyPath))
                {
                    outfile.WriteLine($"using UnityEngine;");
                    outfile.WriteLine($"");
                    if (useRegions) outfile.WriteLine($"#region States");
                    outfile.WriteLine($"public enum {prefixName}State");
                    outfile.WriteLine($"{{");
                    foreach (string state in states)
                    {
                        outfile.WriteLine($"    {state},");
                    }
                    outfile.WriteLine($"}}");
                    outfile.WriteLine($"");
                    if (useRegions) outfile.WriteLine($"#endregion");
                    outfile.WriteLine($"");
                    outfile.WriteLine($"public class {prefixName}StateMachine : MonoBehaviour");
                    outfile.WriteLine($"{{");
                    outfile.WriteLine($"");
                    outfile.WriteLine($"    private {prefixName}State _currentState;");
                    outfile.WriteLine($"");
                    if (useRegions) outfile.WriteLine($"    #region Public properties");
                    outfile.WriteLine($"    public {prefixName}State CurrentState {{ get => _currentState; private set => _currentState = value; }}");
                    if (useRegions) outfile.WriteLine($"    #endregion");
                    if (spaceRegions) outfile.WriteLine($"");
                    if (useRegions) outfile.WriteLine($"    #region Init");
                    outfile.WriteLine($"    private void Start()");
                    outfile.WriteLine($"    {{");
                    outfile.WriteLine($"        TransitionToState(CurrentState, {prefixName}State.{states[defaultState]});");
                    outfile.WriteLine($"    }}");
                    if (useRegions) outfile.WriteLine($"    #endregion");
                    if (spaceRegions) outfile.WriteLine($"");
                    if (useRegions) outfile.WriteLine($"    #region Update");
                    outfile.WriteLine($"    private void Update()");
                    outfile.WriteLine($"    {{");
                    outfile.WriteLine($"        OnStateUpdate(CurrentState);");
                    outfile.WriteLine($"    }}");
                    if (useRegions) outfile.WriteLine($"    #endregion");
                    if (spaceRegions) outfile.WriteLine($"");
                    if (useRegions) outfile.WriteLine($"    #region State Machine");
                    outfile.WriteLine($"    private void OnStateEnter({prefixName}State state)");
                    outfile.WriteLine($"    {{");
                    outfile.WriteLine($"        switch (state)");
                    outfile.WriteLine($"        {{");
                    foreach (string state in states)
                    {
                        string stateMethod = TInfo.ToTitleCase(state.Replace("_", " ").ToLower()).Replace(" ", "");
                        outfile.WriteLine($"            case {prefixName}State.{state}:");
                        outfile.WriteLine($"                OnEnter{stateMethod}();");
                        outfile.WriteLine($"                break;");
                    }
                    outfile.WriteLine($"            default:");
                    outfile.WriteLine($"                Debug.LogError(\"OnStateEnter: Invalid state \" + state.ToString());");
                    outfile.WriteLine($"                break;");
                    outfile.WriteLine($"        }}");
                    outfile.WriteLine($"    }}");

                    outfile.WriteLine($"    private void OnStateUpdate({prefixName}State state)");
                    outfile.WriteLine($"    {{");
                    outfile.WriteLine($"        switch (state)");
                    outfile.WriteLine($"        {{");
                    foreach (string state in states)
                    {
                        string stateMethod = TInfo.ToTitleCase(state.Replace("_", " ").ToLower()).Replace(" ", "");
                        outfile.WriteLine($"            case {prefixName}State.{state}:");
                        outfile.WriteLine($"                OnUpdate{stateMethod}();");
                        outfile.WriteLine($"                break;");
                    }
                    outfile.WriteLine($"            default:");
                    outfile.WriteLine($"                Debug.LogError(\"OnStateUpdate: Invalid state \" + state.ToString());");
                    outfile.WriteLine($"                break;");
                    outfile.WriteLine($"        }}");
                    outfile.WriteLine($"    }}");
                    outfile.WriteLine($"    private void OnStateExit({prefixName}State state)");
                    outfile.WriteLine($"    {{");
                    outfile.WriteLine($"        switch (state)");
                    outfile.WriteLine($"        {{");
                    foreach (string state in states)
                    {
                        string stateMethod = TInfo.ToTitleCase(state.Replace("_", " ").ToLower()).Replace(" ", "");
                        outfile.WriteLine($"            case {prefixName}State.{state}:");
                        outfile.WriteLine($"                OnExit{stateMethod}();");
                        outfile.WriteLine($"                break;");
                    }
                    outfile.WriteLine($"            default:");
                    outfile.WriteLine($"                Debug.LogError(\"OnStateExit: Invalid state \" + state.ToString());");
                    outfile.WriteLine($"                break;");
                    outfile.WriteLine($"        }}");
                    outfile.WriteLine($"    }}");
                    outfile.WriteLine($"    private void TransitionToState({prefixName}State fromState, {prefixName}State toState)");
                    outfile.WriteLine($"    {{");
                    outfile.WriteLine($"        OnStateExit(fromState);");
                    outfile.WriteLine($"        CurrentState = toState;");
                    outfile.WriteLine($"        OnStateEnter(toState);");
                    outfile.WriteLine($"    }}");

                    outfile.WriteLine($"    private void TransitionToState({prefixName}State toState)");
                    outfile.WriteLine($"    {{");
                    outfile.WriteLine($"        TransitionToState(CurrentState, toState);");
                    outfile.WriteLine($"    }}");
                    if (useRegions) outfile.WriteLine($"    #endregion");
                    if (spaceRegions) outfile.WriteLine($"");
                    if (!groupByPhase)
                    {
                        foreach (string state in states)
                        {
                            string stateMethod = TInfo.ToTitleCase(state.Replace("_", " ").ToLower()).Replace(" ", "");
                            if (useRegions) outfile.WriteLine($"    #region State {state}");
                            outfile.WriteLine($"    private void OnEnter{stateMethod}()");
                            outfile.WriteLine($"    {{");
                            outfile.WriteLine($"    }}");
                            outfile.WriteLine($"    private void OnUpdate{stateMethod}()");
                            outfile.WriteLine($"    {{");
                            outfile.WriteLine($"    }}");
                            outfile.WriteLine($"    private void OnExit{stateMethod}()");
                            outfile.WriteLine($"    {{");
                            outfile.WriteLine($"    }}");
                            if (useRegions) outfile.WriteLine($"    #endregion");
                            if (spaceRegions) outfile.WriteLine($"");
                        }
                    }
                    else
                    {
                        if (useRegions) outfile.WriteLine($"    #region EnterState");
                        foreach (string state in states)
                        {
                            string stateMethod = TInfo.ToTitleCase(state.Replace("_", " ").ToLower()).Replace(" ", "");
                            outfile.WriteLine($"    private void OnEnter{stateMethod}()");
                            outfile.WriteLine($"    {{");
                            outfile.WriteLine($"    }}");
                        }
                        if (useRegions) outfile.WriteLine($"    #region UpdateState");
                        foreach (string state in states)
                        {
                            string stateMethod = TInfo.ToTitleCase(state.Replace("_", " ").ToLower()).Replace(" ", "");
                            outfile.WriteLine($"    private void OnUpdate{stateMethod}()");
                            outfile.WriteLine($"    {{");
                            outfile.WriteLine($"    }}");
                        }
                        if (useRegions) outfile.WriteLine($"    #region ExitState");
                        foreach (string state in states)
                        {
                            string stateMethod = TInfo.ToTitleCase(state.Replace("_", " ").ToLower()).Replace(" ", "");
                            outfile.WriteLine($"    private void OnExit{stateMethod}()");
                            outfile.WriteLine($"    {{");
                            outfile.WriteLine($"    }}");
                        }
                    }
                    outfile.WriteLine($"}}");
                }
            }
            AssetDatabase.Refresh();
        }
    }
}