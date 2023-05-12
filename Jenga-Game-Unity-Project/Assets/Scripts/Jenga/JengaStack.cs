namespace JengaGame
{
    using UnityEngine;
    using TMPro;
    using System.Collections.Generic;

    public class JengaStack : MonoBehaviour
    {
        [SerializeField] int gradeLevel;

        [Space()]
        [SerializeField] TMP_Text gradeLabel;
        List<JengaBlock> blocks = new List<JengaBlock>();

        const string DISPLAY_FORMAT = "{0} Grade";

        public void AddBlock(JengaBlock block)
        {

        }

        public void RemoveBlock(JengaBlock block)
        {

        }

        public void DisableMasteryTypes(MasteryType type)
        {
            // TODO: disable blocks of a sepcific type
        }

        public void UpdateGradeLabelDisplay(int grade)
        {
            // TODO: 1st, 2nd, etc.
            gradeLabel.text = string.Format(DISPLAY_FORMAT, grade);
        }

        public Vector3 GetMidPoint()
        {
            return transform.position + ((Vector3.up * GetHeight()) / 2.0f);
        }

        // TODO: calculate by rows and block size
        public float GetHeight()
        {
            return 0;
        }
    }

}