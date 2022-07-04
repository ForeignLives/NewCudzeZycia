using UnityEditor;
using CleverCrow.Fluid.UniqueIds;

// Wyœwietlanie unikalnego id przy ContentTrigger (u¿ywane przy tworzeniu sejwów)
[CustomEditor(typeof(ContentTrigger))]
class ContentTriggerGUIInspector : Editor
{
    public override void OnInspectorGUI()
    {
        ContentTrigger ct = (ContentTrigger)target;
        var uniqueId = ct.GetComponent<UniqueId>();
        EditorGUILayout.SelectableLabel(uniqueId.Id);
        base.OnInspectorGUI();
    }
}
