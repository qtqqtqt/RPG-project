using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/ New Progression", order = 1)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        public float GetHealth(CharacterClass characterClass, int level)
        {
            float characterHealth = 0;

            foreach (ProgressionCharacterClass characterProgression in characterClasses)
            {
                if (characterProgression.characterClass == characterClass)
                {
                   // characterHealth = characterProgression.health[level - 1]; 
                }
            }

            return characterHealth;
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats; 
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] level;
        }
    }
}
