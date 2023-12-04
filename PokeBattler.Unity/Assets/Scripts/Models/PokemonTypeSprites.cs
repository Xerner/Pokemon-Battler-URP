using UnityEngine;

namespace PokeBattler.Client.Models
{
    public struct PokemonTypeSprites
    {
        public Sprite Sprite;
        public Sprite MiniSprite;
        public Sprite BoxSprite;
        public Sprite MiniBoxSprite;

        public PokemonTypeSprites(Sprite sprite, Sprite miniSprite, Sprite boxSprite, Sprite miniBoxSprite)
        {
            Sprite = sprite;
            MiniSprite = miniSprite;
            BoxSprite = boxSprite;
            MiniBoxSprite = miniBoxSprite;
        }
    }
}
