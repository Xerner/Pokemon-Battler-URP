using PokeBattler.Common.Models;
using UnityEngine;

namespace PokeBattler.Client.Models.SO
{
    [CreateAssetMenu(fileName = "New PokemonGO Move", menuName = "PokemonGO/PokemonGO Move")]
	public class PokemonMoveSO : ScriptableObject
	{
		public PokemonMove PokemonMove;
	}
}