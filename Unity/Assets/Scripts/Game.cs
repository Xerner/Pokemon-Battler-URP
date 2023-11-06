using System;

namespace Poke.Core
{
    public class Game
    {
        public TrainerManager TrainerManager = new TrainerManager();
        public Trainer ClientsTrainer;
        public PokemonPool PokemonPool;

        public Action OnPokemonDataLoaded;

        public Trainer CreateTrainer(Account hostAccount)
        {
            Trainer trainer = new Trainer(hostAccount);
            TrainerManager.Add(trainer);
            return trainer;
        }
    }
}
