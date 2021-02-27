using UnityEditor;
using UnityEngine;

namespace Util.Events
{
    public abstract class GameEventEditor<T> : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GameEvent<T> gameEvent = (GameEvent<T>) target;

            GUI.enabled = Application.isPlaying;


            if (GUILayout.Button("Raise") && gameEvent.ValueToRaise != null)
            {
                gameEvent.Raise(gameEvent.ValueToRaise);
            }
        }
    }
}
