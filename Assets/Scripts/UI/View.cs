using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Animator))]
    public abstract class View : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public virtual void Init()
        {
        }

        protected virtual void OnOpen()
        {
            
        }
        
        protected virtual void OnClose()
        {
            
        }

        protected void Open()
        {
            _animator.SetInteger("State", 1);
            OnOpen(); 
        }

        protected void Close()
        {
            _animator.SetInteger("State", 2);
            OnClose();
        }
    }
}