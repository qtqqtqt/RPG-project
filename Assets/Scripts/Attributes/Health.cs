using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        float healthPoints = -1f;
        bool isDead = false;

        private void Start()
        {
            GetComponent<BaseStats>().onLevelUp += SetToMax;
            if (healthPoints < 0)
            {
                SetToMax();
            }
        }

        public bool IsDead()
        {
            return isDead;
        }

        private void SetToMax()
        {
            healthPoints = GetMaxHealthPoints();
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        public float GetHealthPoints()
        {
            return healthPoints;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health) * 100f;
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }


        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;

            if (healthPoints <= 0)
            {
                Die();
            }
        }
    }
}