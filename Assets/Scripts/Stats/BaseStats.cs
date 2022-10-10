using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression;
        [SerializeField] GameObject levelUpParticleEffect = null;

        public event Action onLevelUp;

        int currentLevel = 0;

        private void Start()
        {
            currentLevel = CalculateLevel();

            Experience experience = GetComponent<Experience>();
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
                LevelUpEffect();
                onLevelUp();
            }
        }

        private void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        public int GetLevel()
        {
            if (currentLevel < 1)
            {
                CalculateLevel();
            }
            return currentLevel;
        }

        public int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();

            if (experience == null ) return startingLevel;
            
            float currentXP = experience.GetExperienceValue();
            int penultimateLvl = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for (int level = 1; level <= penultimateLvl; level++)
            {
                float XPtoLvlUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if (XPtoLvlUp > currentXP)
                {
                    return level;
                }
            }
            return penultimateLvl + 1;
        }
    }
}
