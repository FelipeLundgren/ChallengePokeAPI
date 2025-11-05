using System;
using System.Collections.Generic;

// 1. Classe Principal do Pokémon (snake_case para JsonUtility)
[System.Serializable]
public class Pokemon
{
    // Campos essenciais da Primeira Requisição:
    public string name;
    public int pokemonLevel;
    public int id; 
    public List<PokemonTypeWrapper> types;
    public PokemonSprites sprites;
    public List<PokemonMoveWrapper> moves;
    public List<PokemonStatWrapper> stats;
}

// 2. Classe de Recurso Básico (usada para Tipos e URLs de Movimentos)
[System.Serializable]
public class NamedApiResource
{
    public string name;
    public string url;
}

// 3. Tipos do Pokémon
[System.Serializable]
public class PokemonTypeWrapper
{
    public int slot;
    public NamedApiResource type;
}
[System.Serializable]
public class PokemonStatWrapper
{
    public int base_stat; // O valor base que você precisa
    public int effort;
    public NamedApiResource stat; // Contém "name": "hp", "attack", etc.
}

// 4. Moves (Wrapper do Movimento no Pokémon)
[System.Serializable]
public class PokemonMoveWrapper
{
    public NamedApiResource move; // Contém nome e a URL (para buscar PP/Tipo)
    
    // Detalhes de Versão/Aprendizagem
    public List<MoveVersionGroupDetail> version_group_details; 
}

// 5. Detalhes de Versão/Aprendizagem (Mantido para match de JSON)
[System.Serializable]
public class MoveVersionGroupDetail
{
    public int level_learned_at;
    public NamedApiResource version_group; 
}

// 6. Sprites (Incluindo todas as URLs de nível superior)
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

// ====================================================================
// CLASSE DE DETALHES DO GOLPE (SEGUNDA REQUISIÇÃO)
// *Necessário para obter o TIPO e PP do golpe.*
// ====================================================================

[System.Serializable]
public class MoveDetails
{
    public string name;
    public int pp; 
    public NamedApiResource type; 
}