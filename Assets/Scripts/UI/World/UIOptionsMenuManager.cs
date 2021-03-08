using UnityEngine.UIElements;

namespace LocationRPG
{
    public class UIOptionsMenuManager : VisualElement
    {
        public UIOptionsMenuManager()
        {
            RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }

        void OnGeometryChange(GeometryChangedEvent evt)
        {
        }
    }
}