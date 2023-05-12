namespace JengaGame
{
    using UnityEngine;
    using UnityEngine.Events;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;

    public class GameManager : MonoBehaviour
    {
        public static UnityEvent<JengaStack> OnCurrentStackChangedGlobal = new UnityEvent<JengaStack>();

        static GameManager instance;

        [SerializeField] JengaStack currentStack;
        int currentStackIndex = 1; // start with middle option

        JengaStack[] stacks;
        Dictionary<string, JengaStack> stackLookup = new Dictionary<string, JengaStack>();

        void Awake()
        {
            Debug.Assert(instance == null, "There are more than 1 GameManager's in the scene!");
            instance = this;

            stacks = Object.FindObjectsOfType<JengaStack>();
            foreach(JengaStack stack in stacks)
                stackLookup.Add(stack.GetStackId(), stack);

            // update starting potential target index to match assigned target in editor
            for (int i=0; i<stacks.Length; i++)
            {
                if (stacks[i] == currentStack)
                {
                    currentStackIndex = i;
                    break;
                }
            }
        }

        IEnumerator Start()
        {
            while (!AppManager.IsInitialized())
                yield return null;
            Reset();
            OnCurrentStackChangedGlobal.Invoke(currentStack);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                NextTarget();
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                PreviousTarget();
        }

        public void Reset()
        {
            foreach (JengaStack stack in stacks)
                stack.Clear();
            Initialize(AppManager.GetLoadedData());
        }

        public void Initialize(JengaBlockData[] dataArray)
        {
            // sorting data by requirements
            List<JengaBlockData> list = new List<JengaBlockData>(dataArray);
            List<JengaBlockData> sortedList = list
                .OrderBy(i => i.domain)
                .ThenBy(i => i.cluster)
                .ThenBy(i => i.standardid)
                .ToList();

            StartCoroutine(InitializeRoutine(sortedList.ToArray()));
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


        public void TestAllStacks()
        {
            foreach (JengaStack stack in stacks)
                stack.TestWithout(MasteryType.Glass);
        }
        public void TestMyStack()
        {
            currentStack.TestWithout(MasteryType.Glass);
        }

        void OnDestroy()
        {
            instance = null;
        }

        public static JengaStack GetCurrentStack()
        {
            if (instance)
                return instance.currentStack;
            else
                return null;
        }

        public static bool IsInitialized()
        {
            return instance;
        }

        void PreviousTarget()
        {
            currentStackIndex -= 1;
            if (currentStackIndex < 0)
                currentStackIndex = stacks.Length-1;

            currentStack = stacks[currentStackIndex];
            OnCurrentStackChangedGlobal.Invoke(currentStack);
        }

        void NextTarget()
        {
            currentStackIndex += 1;
            if (currentStackIndex >= stacks.Length)
                currentStackIndex = 0;

            currentStack = stacks[currentStackIndex];
            OnCurrentStackChangedGlobal.Invoke(currentStack);
        }
    }
}