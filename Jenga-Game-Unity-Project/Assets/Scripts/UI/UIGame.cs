namespace JengaGame
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;
    using System.Collections;
    using TMPro;

    public class UIGame : MonoBehaviour
    {
        [SerializeField] TMP_Text blockInfoLabel;

        const string BLOCK_INFO_FORMAT = "{0}: {1}\n{2}\n{3}: {4}";

        void Awake()
        {
            JengaBlock.OnRightClickedGlobal.AddListener(OnJengaBlockRightClicked);
        }

        void OnDestroy()
        {
            JengaBlock.OnRightClickedGlobal.RemoveListener(OnJengaBlockRightClicked);
        }

        void OnJengaBlockRightClicked(JengaBlock block)
        {
            JengaBlockData data = block.GetData();
            blockInfoLabel.text = string.Format(BLOCK_INFO_FORMAT, data.grade, data.domain, data.cluster, data.standardid, data.standarddescription);
        }
    }
}