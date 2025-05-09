using System.Collections.Generic;

namespace AutoChess.Library.Services
{
    public static class PokemonConstants {
        public static Dictionary<int, int> enumToTier = new Dictionary<int, int>() {
            { 1, 1 }, // Bulbasaur
            { 2, 1 }, // Ivysaur
            { 3, 1 }, // Venusaur
            { 4, 1 }, // Charmander
            { 5, 1 }, // Charmeleon
            { 6, 1 }, // Charizard
            { 74, 1 }, // Geodude
            { 75, 1 }, // Graveler
            { 76, 1 }, // Golem
            { 113, 1 }, // Chansey
            { 242, 1 }, // Blissey
            { 440, 1 }, // Happiny
            { 81, 1 }, // Magnemite
            { 82, 1 }, // Magneton
            { 462, 1 }, // Magnezone
            { 32, 1 }, // Nidoran
            { 33, 1 }, // Nidorino
            { 34, 1 }, // Nidoking
            { 43, 1 }, // Oddish
            { 44, 1 }, // Gloom
            { 45, 1 }, // Vileplume
            { 60, 1 }, // Poliwag
            { 61, 1 }, // Poliwhirl
            { 62, 1 }, // Poliwrath
            { 273, 1 }, // Seedot
            { 274, 1 }, // Nuzleaf
            { 275, 1 }, // Shiftry
            { 540, 1 }, // Sewaddle
            { 541, 1 }, // Swadloon
            { 542, 1 }, // Leavanny
            { 363, 1 }, // Spheal
            { 364, 1 }, // Sealeo
            { 365, 1 }, // Walrein
            { 7, 1 }, // Squirtle
            { 8, 1 }, // Wartortle
            { 9, 1 }, // Blastoise
            { 13, 1 }, // Weedle
            { 14, 1 }, // Kakuna
            { 15, 1 }, // Beedrill
            { 63, 2 }, // Abra
            { 64, 2 }, // Kadabra
            { 65, 2 }, // Alakazam
            { 315, 2 }, // Roselia
            { 406, 2 }, // Budew
            { 407, 2 }, // Roserade
            { 355, 2 }, // Duskull
            { 356, 2 }, // Dusclops
            { 477, 2 }, // Dusknoir
            { 599, 2 }, // Klink
            { 600, 2 }, // Klang
            { 601, 2 }, // Klinklang
            { 270, 2 }, // Lotad
            { 271, 2 }, // Lombre
            { 272, 2 }, // Ludicolo
            { 66, 2 }, // Machop
            { 67, 2 }, // Machoke
            { 68, 2 }, // Machamp
            { 280, 2 }, // Ralts
            { 281, 2 }, // Kirlia
            { 282, 2 }, // Gardevoir
            { 396, 2 }, // Starly
            { 397, 2 }, // Staravia
            { 398, 2 }, // Staraptor
            { 220, 2 }, // Swinub
            { 221, 2 }, // Piloswine
            { 473, 2 }, // Mamoswine
            { 175, 2 }, // Togepi
            { 176, 2 }, // Togetic
            { 468, 2 }, // Togekiss
            { 328, 2 }, // Trapinch
            { 329, 2 }, // Vibrava
            { 330, 2 }, // Flygon
            { 535, 2 }, // Tympole
            { 536, 2 }, // Palpitoad
            { 537, 2 }, // Seismitoad
            { 602, 2 }, // Tynamo
            { 603, 2 }, // Eelektrik
            { 604, 2 }, // Eelektross
            { 543, 2 }, // Venipede
            { 544, 2 }, // Whirlipede
            { 545, 2 }, // Scolipede
            { 304, 3 }, // Aron
            { 305, 3 }, // Lairon
            { 306, 3 }, // Aggron
            { 125, 3 }, // Electabuzz
            { 239, 3 }, // Elekid
            { 466, 3 }, // Electivire
            { 92, 3 }, // Gastly
            { 93, 3 }, // Haunter
            { 94, 3 }, // Gengar
            { 116, 3 }, // Horsea
            { 117, 3 }, // Seadra
            { 230, 3 }, // Kingdra
            { 607, 3 }, // Litwick
            { 608, 3 }, // Lampent
            { 609, 3 }, // Chandelure
            { 126, 3 }, // Magmar
            { 240, 3 }, // Magby
            { 467, 3 }, // Magmortar
            { 111, 3 }, // Rhyhorn
            { 112, 3 }, // Rhydon
            { 464, 3 }, // Rhyperior
            { 551, 3 }, // Sandile
            { 552, 3 }, // Krokorok
            { 553, 3 }, // Krookodile
            { 577, 3 }, // Solosis
            { 578, 3 }, // Duosion
            { 579, 3 }, // Reuniclus
            { 532, 3 }, // Timburr
            { 533, 3 }, // Gurdurr
            { 534, 3 }, // Conkeldurr
            { 582, 3 }, // Vanillite
            { 583, 3 }, // Vanillish
            { 584, 3 }, // Vanilluxe
            { 41, 3 }, // Zubat
            { 42, 3 }, // Golbat
            { 169, 3 }, // Crobat
            { 374, 4 }, // Beldum
            { 375, 4 }, // Metang
            { 376, 4 }, // Metagross
            { 633, 4 }, // Deino
            { 634, 4 }, // Zweilous
            { 635, 4 }, // Hydreigon
            { 147, 4 }, // Dratini
            { 148, 4 }, // Dragonair
            { 149, 4 }, // Dragonite
            { 443, 4 }, // Gible
            { 444, 4 }, // Gabite
            { 445, 4 }, // Garchomp
            { 246, 4 }, // Larvitar
            { 247, 4 }, // Pupitar
            { 248, 4 }, // Tyranitar
        };

        //public class PokemonConstantValues {
        //    public int tier;
        //    public int attackRange;
        //}
    }
}

