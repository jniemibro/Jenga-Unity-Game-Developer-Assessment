namespace JengaGame
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.Networking;
    using System.Collections;

    public class Initializer : MonoBehaviour
    {
        [SerializeField] AppManager appManagerPrefab;

        void Awake()
        {
            if (!AppManager.IsInitialized())
                Instantiate(appManagerPrefab);
        }
    }
}