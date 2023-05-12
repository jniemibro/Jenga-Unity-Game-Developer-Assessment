namespace JengaGame
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.Networking;
    using System.Collections;

    public class AppManager : MonoBehaviour
    {
        static AppManager instance;
        string jsonData;

        private const string URL = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";
        private const string GAME_SCENE = "Game";

        void Awake()
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        IEnumerator Start()
        {
            yield return StartCoroutine(ProcessRequest(URL));
            // TODO: option to try again, or proceed if successful

            if (string.IsNullOrEmpty(jsonData))
            {
                Debug.LogError("Failed to load data from, " + URL, this);
            }
            else
            {
                Debug.Log("Successfully loaded data from, " + URL, this);
                // proceed to gameplay scene after successfully loading data
                if (SceneManager.GetActiveScene().name != GAME_SCENE)
                    SceneManager.LoadScene(GAME_SCENE);
            }
        }

        public static void GenerateRequest()
        {
            if (!instance)
                return;
                
            instance.StartCoroutine(instance.ProcessRequest(URL));
        }

        public IEnumerator ProcessRequest(string url)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                yield return request.SendWebRequest();
                if (request.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(request.error);
                }
                else
                {
                    jsonData = request.downloadHandler.text;
                    Debug.Log(jsonData);
                }
            }
        }

        public static bool IsInitialized()
        {
            return instance;
        }
    }
}