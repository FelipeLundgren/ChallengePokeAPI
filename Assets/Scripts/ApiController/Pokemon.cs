using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Pokemon
{
    public int hp;
    public string name;
    public int pokemonLevel;
    public int id;
    public List<MoveDetails> movesData;
    public Texture2D sprite;
    public List<PokemonTypeWrapper> types;
    public PokemonSprites sprites;
    public List<PokemonMoveWrapper> moves;
    public List<PokemonStatWrapper> stats;
}

// Classe de Recurso Básico (usada para Tipos e URLs de Movimentos)
[System.Serializable]
public class NamedApiResource
{
    public string name;
    public string url;
}

// Tipos do Pokémon
[System.Serializable]
public class PokemonTypeWrapper
{
    public int slot;
    public NamedApiResource type;
}

[System.Serializable]
public class PokemonStatWrapper
{
    public int base_stat;
    public int effort;
    public NamedApiResource stat;
}

// Moves 
[System.Serializable]
public class PokemonMoveWrapper
{
    public NamedApiResource move;


    public List<MoveVersionGroupDetail> version_group_details;
}

// Detalhes de Versão
[System.Serializable]
public class MoveVersionGroupDetail
{
    public int level_learned_at;
    public NamedApiResource version_group;
}

// Sprites
[System.Serializable]
public class PokemonSprites
{
    public string back_default;
    public string front_default;
    public string back_female;
    public string front_female;
    public string back_shiny;
    public string front_shiny;
    public string back_shiny_female;
    public string front_shiny_female;
}

[System.Serializable]
public class MoveDetails
{
    public string name;
    public int pp;
    public NamedApiResource type;
}