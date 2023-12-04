using System;

namespace PokeBattler.Common.Models
{
    public partial class Trainer
    {
        public Guid Id { get => Account.Id; }
        public Account Account;
        bool Ready = false;
        int CurrentHealth = 100;
        int TotalHealth = 100;
        int experience = 0;
        public int Money = 10;
        int level = 2;
        public int Level { get => level; }
        public int Experience { get => experience; }
        public string ExperienceStr { get => experience + "/" + LevelToExpNeededToLevelUp[level]; }
        Action<bool> onReady;

        public void OnReadySubscribe(Action<bool> action) => onReady += action;
        public void OnReadyUnsubscribe(Action<bool> action) => onReady -= action;

        public Trainer(Account account)
        {
            Account = account;
        }

        public bool SetReady(bool ready)
        {
            Ready = ready;
            onReady?.Invoke(ready);
            return ready;
        }

        public int CalculateInterest() => Money / 10;

        /// <summary>Create experience to the Trainer. Rolls over exp if they level up.</summary>
        /// <returns>True if the player levels up</returns>
        public bool AddExperience(int expToAdd)
        {
            int expNeeded = LevelToExpNeededToLevelUp[level];
            int newExp = experience + expToAdd;
            int difference = expNeeded - newExp;
            if (difference <= 0)
            { // leveled up
              // edge case: player is already max level
                if (!LevelToExpNeededToLevelUp.ContainsKey(level + 1))
                {
                    experience = LevelToExpNeededToLevelUp[level];
                    difference = 1;
                }
                else
                {
                    level++;
                    experience = Math.Abs(difference);
                }
            }
            else
            {
                experience = newExp;
            }
            return difference <= 0;
        }
    }
}
