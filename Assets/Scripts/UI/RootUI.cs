using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class RootUI : MonoBehaviour
    {
        [SerializeField] private List<View> _views;

        private void Awake()
        {
            foreach (var view in _views)
            {
                view.Init();
            }
        }
    }
}