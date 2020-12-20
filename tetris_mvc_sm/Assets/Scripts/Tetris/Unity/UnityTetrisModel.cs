using UnityEngine;

namespace Models
{
    public class UnityTetrisModel : MonoBehaviour
    {
        public ITetrisModel Model { get; private set; }

        void Awake()
        {
            Model = new TetrisModel();
        }
    }
}
