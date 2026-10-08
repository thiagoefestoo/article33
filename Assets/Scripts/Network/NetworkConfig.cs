
using UnityEngine;

// =====================================================
// ARTIGO33 - NETWORK CONFIG
// UNITY 6 + AWS EC2 + NEON POSTGRESQL
//
// Configuracao central de comunicacao HTTP.
//
// API: HTTPS via Nginx na AWS
// Banco: Neon PostgreSQL
//
// O multiplayer FishNet utiliza configuracao
// de transporte separada desta classe.
// =====================================================

public static class NetworkConfig
{
    // =====================================================
    // ENDERECO PRINCIPAL DA API
    // =====================================================

    // Utiliza a configuracao central ApiConfig.cs.
    // Mantem compatibilidade com os scripts existentes.

    public static string ServerURL
    {
        get
        {
            return ApiConfig.BaseURL.TrimEnd('/');
        }

        set
        {
            ApiConfig.BaseURL = value;
        }
    }

    // =====================================================
    // ENDPOINT - CARREGAMENTO DE JOGADOR
    // =====================================================

    public static string UnityLoadEndpoint =
        "/api/unity/load/";

    // =====================================================
    // URL COMPLETA - CARREGAMENTO
    // =====================================================

    public static string GetUnityLoadURL(int userId)
    {
        return ServerURL +
               UnityLoadEndpoint +
               userId;
    }
}
