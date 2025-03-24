using System.Collections.Generic;

namespace AutoChess.Contracts.Models.Json
{
    public class PokemonSpriteWithVersions : PokemonSprite
    {
        public Dictionary<string, Dictionary<string, PokemonSpriteBlackWhite>> versions = [];
    }
}
