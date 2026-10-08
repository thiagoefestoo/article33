
using UnityEngine;

// =====================================
// ARTIGO33 - CONFIGURACAO DA API
// UNITY 6 + AWS EC2 + NEON POSTGRESQL
// =====================================

public class ApiConfig : MonoBehaviour
{
    // =====================================
    // API PRINCIPAL - AWS HTTPS
    // =====================================

    public static string BaseURL =
        "https://api-artigo33.duckdns.org";

    // =====================================
    // API LOCAL - DESENVOLVIMENTO
    // =====================================

    // Para testar com o backend local,
    // utilize este endereco:
    //
    // http://localhost:5160
    //
    // Para voltar ao servidor AWS,
    // utilize o endereco HTTPS acima.
}
