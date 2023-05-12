namespace JengaGame
{
    using UnityEngine;

    public class JengaBlock : MonoBehaviour
    {
        [SerializeField] MasteryType masteryType = MasteryType.Glass;

        public MasteryType GetMasteryType()
        {
            return masteryType;
        }
    }

}