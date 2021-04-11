using UnityEngine.UIElements;
using UnityEngine;

namespace LocationRPG
{
    public class CombatUIManager : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<CombatUIManager, UxmlTraits>
        {
        }
        
        public CombatUIManager()
        {
            RegisterCallback<GeometryChangedEvent>(Init);
        }

        void Init(GeometryChangedEvent evt)
        {
            //Init Combat UI
            Debug.Log("CombatUIManager Init");
        }
    }
}