namespace JengaGame
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;
    using System.Collections;
    using TMPro;

    public class UIGame : MonoBehaviour
    {
        [SerializeField] GameObject blockLabelGo;
        [SerializeField] TMP_Text blockInfoLabel;

        const string BLOCK_INFO_FORMAT = "• {0}: {1}\n• {2}\n• {3}: {4}";

        void Awake()
        {
            blockLabelGo.SetActive(false);
        }

        void OnDestroy()
        {

        }
            
        public void UpdateBlockInfoDisplay(JengaBlock block)
        {
            if (block)
            {
                JengaBlockData data = block.GetData();
                blockInfoLabel.text = string.Format(BLOCK_INFO_FORMAT, data.grade, data.domain, data.cluster, data.standardid, data.standarddescription);
            }
            else
                blockInfoLabel.text = string.Empty;
            blockLabelGo.SetActive(block);
        }
    }
}