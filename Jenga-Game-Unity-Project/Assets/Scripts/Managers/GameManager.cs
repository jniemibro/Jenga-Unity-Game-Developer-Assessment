namespace JengaGame
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    public class GameManager : MonoBehaviour
    {
        static GameManager instance;

        JengaStack[] stacks;
        Dictionary<string, JengaStack> stackLookup = new Dictionary<string, JengaStack>();

        void Awake()
        {
            Debug.Assert(instance == null, "There are more than 1 GameManager's in the scene!");
            instance = this;

            stacks = Object.FindObjectsOfType<JengaStack>();
            foreach(JengaStack stack in stacks)
                stackLookup.Add(stack.GetStackId(), stack);
        }

        IEnumerator Start()
        {
            while (!AppManager.IsInitialized())
                yield return null;
            Initialize(AppManager.GetLoadedData());
        }

        public void Initialize(JengaBlockData[] dataArray)
        {
            StartCoroutine(InitializeRoutine(dataArray));
            /*foreach (JengaBlockData data in dataArray)
            {
                Debug.Log(data.grade);
                JengaStack stack;
                stackLookup.TryGetValue(data.grade, out stack);
                if (stack)
                {
                    stack.AddBlock(data.mastery);
                }
                else
                    Debug.LogWarning(data.grade + " stack could not be found?");
            }*/
        }

        IEnumerator InitializeRoutine(JengaBlockData[] dataArray)
        {
            foreach (JengaBlockData data in dataArray)
            {
                //Debug.Log(data.grade);
                JengaStack stack;
                stackLookup.TryGetValue(data.grade, out stack);
                if (stack)
                {
                    stack.AddBlock(data);
                }
                else
                    Debug.LogWarning(data.grade + " stack could not be found?");
                yield return null;
            }
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