using PokeBattler.Common.Models;
using UnityEngine;

namespace PokeBattler.Client.Models.SO
{
    [CreateAssetMenu(fileName = "New Pokemon Move", menuName = "PokeBattler/Pokemon/Move")]
	public class PokemonMoveSO : ScriptableObject
	{
		public PokemonMove PokemonMove;
	}
}