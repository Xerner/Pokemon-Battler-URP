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
        public int Level { get; set; } = 1;
        public int Experience { get; set; } = 0;
        public int ExperienceNeededToLevelUp { get; set; }
        public string ExperienceStr { get => experience + "/" + ExperienceNeededToLevelUp; }
        Action<bool> onReady;

        public void OnReadySubscribe(Action<bool> action) => onReady += action;
        public void OnReadyUnsubscribe(Action<bool> action) => onReady -= action;

        public Trainer(Account account, int experienceNeededToLevelUp)
        {
            Account = account;
            ExperienceNeededToLevelUp = experienceNeededToLevelUp;
        }

        public bool SetReady(bool ready)
        {
            Ready = ready;
            onReady?.Invoke(ready);
            return ready;
        }
    }
}
