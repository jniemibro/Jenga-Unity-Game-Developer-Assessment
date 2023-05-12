namespace JengaGame
{
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        static GameManager instance;

        [SerializeField] JengaStack[] stacks;

        void Awake()
        {
            Debug.Assert(instance == null, "There are more than 1 GameManager's in the scene!");
            instance = this;

            stacks = Object.FindObjectsOfType<JengaStack>();
        }

        void Start()
        {
            // TODO: populate stacks based on loaded data
        }

        void OnDestroy()
        {
            instance = null;
        }

        public static bool IsInitialized()
        {
            return instance;
        }
    }
}