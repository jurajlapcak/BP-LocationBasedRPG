using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace LocationRPG
{
    public abstract class UnitController<T> : MonoBehaviour where T : Unit
    {
        [SerializeField] protected T unit;
        [SerializeField] protected GameObject unitModel;
        protected AnimationController _animationController;

        protected LerpingController _lerpingController;
        protected bool _isMoving;

        public T Unit
        {
            get => unit;
            set => unit = value;
        }
        
        public AnimationController AnimationController => _animationController;

        private void OnEnable()
        {
            Assert.IsNotNull(unitModel);
            Assert.IsNotNull(unit);
        }

        private void Awake()
        {
            _animationController = new AnimationController(unitModel.GetComponent<Animator>(),
                AnimationConstants.IDLE);
            // if (SceneManager.GetActiveScene().name == SceneNameConstants.COMBAT_SCENE)
            // {
                _lerpingController = new LerpingController(1.5f);
            // }
        }

        private void FixedUpdate()
        {
            if (_lerpingController.IsLerping)
            {
                gameObject.transform.position = _lerpingController.Lerp();
            }
        }
        
        protected void MoveToGameObject(GameObject monster, float offset)
        {
            Vector3 endPosition = monster.transform.position;
            endPosition.z += offset;
            _lerpingController.StartLerping(gameObject.transform.position, endPosition);
        }

        protected void MoveToPosition(Vector3 position)
        {
            _lerpingController.StartLerping(gameObject.transform.position, position);
        }
    }
}