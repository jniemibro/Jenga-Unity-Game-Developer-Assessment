namespace JengaGame
{
    using UnityEngine;
    using TMPro;
    using System.Collections.Generic;

    public class JengaStack : MonoBehaviour
    {
        [SerializeField] string stackId = "6th Grade";
        [SerializeField] JengaBlockPrefabs prefabs;

        [Space()]
        [SerializeField] TMP_Text gradeLabel;
        List<JengaBlock> blocks = new List<JengaBlock>();
        JengaBlockData data;

        const int BLOCKS_PER_ROW = 3;
        //const string DISPLAY_FORMAT = "{0} Grade";


        void Start()
        {
            UpdateGradeLabelDisplay(stackId);
        }

        public void AddBlock(JengaBlockData newData)
        {
            //Debug.Log("Adding block, " + mastery, this);
            JengaBlock prefab = prefabs.GetBlockPrefab(newData.mastery);
            JengaBlock instance = Instantiate(prefab, GetSpawnPosition(), GetSpawnRotation());
            instance.SetData(newData);
            instance.transform.SetParent(transform, true);
            blocks.Add(instance);
        }

        public void RemoveBlock(JengaBlock block)
        {

        }

        public void DisableMasteryTypes(MasteryType type)
        {
            // TODO: disable blocks of a sepcific type
            for (int i=0; i<blocks.Count; i++)
            {
                blocks[i].SetEnabled(blocks[i].GetMasteryType() != type);
            }
        }

        public void UpdateGradeLabelDisplay(string gradeDisplay)
        {
            gradeLabel.text = gradeDisplay;
        }

        public Vector3 GetMidPoint()
        {
            return transform.position + ((Vector3.up * GetHeight()) / 2.0f);
        }

        public int GetRows()
        {
            return blocks.Count % BLOCKS_PER_ROW;
        }

        public float GetBlockHeight()
        {
            return prefabs.GetBlockHeight();
        }
        public float GetBlockWidth()
        {
            return prefabs.GetBlockWidth();
        }

        public float GetHeight()
        {
            return GetBlockHeight() * GetRows();
        }

        public string GetStackId()
        {
            return stackId;
        }

        Vector3 GetSpawnPosition()
        {
            return transform.position + 

                (((blocks.Count % BLOCKS_PER_ROW) % 2 == 0 ? Vector3.forward : Vector3.right) * 
                GetBlockWidth() * (blocks.Count % BLOCKS_PER_ROW)) +

                (Vector3.up * GetBlockHeight() * (blocks.Count / BLOCKS_PER_ROW));
        }

        Quaternion GetSpawnRotation()
        {
            return Quaternion.identity;

            if (blocks.Count % BLOCKS_PER_ROW == 0)
                return Quaternion.identity;
            else
                return Quaternion.Euler(0, 90, 0);
        } 
    }

}