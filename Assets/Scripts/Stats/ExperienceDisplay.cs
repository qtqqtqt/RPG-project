using TMPro;
using UnityEngine;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;
        TextMeshProUGUI experienceUItext;

        private void Awake()
        {
            experience = GameObject.FindGameObjectWithTag("Player").GetComponent<Experience>();
            experienceUItext = GetComponent<TextMeshProUGUI>();

        }

        private void Update()
        {
            experienceUItext.text = string.Format("{0:0}", experience.GetExperienceValue());
        }
    }
}
