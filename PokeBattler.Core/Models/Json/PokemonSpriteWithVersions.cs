using System.Collections.Generic;

namespace PokeBattler.Common.Models.Json
{
    public class PokemonSpriteWithVersions : PokemonSprite
    {
        public Dictionary<string, Dictionary<string, PokemonSpriteBlackWhite>> versions;
    }
}
