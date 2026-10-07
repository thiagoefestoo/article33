using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public class ApiClient : MonoBehaviour
{

    public static ApiClient Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    // =====================================
    // GET REQUEST
    // =====================================

    public IEnumerator Get(
        string url,
        System.Action<string> callback
    )
    {

        using (UnityWebRequest request =
            UnityWebRequest.Get(url))
        {


            request.SetRequestHeader(
                "Content-Type",
                "application/json"
            );


            yield return request.SendWebRequest();



            if (request.result ==
                UnityWebRequest.Result.Success)
            {

                callback(
                    request.downloadHandler.text
                );

            }
            else
            {

                Debug.LogError(
                    "API ERROR: "
                    + request.error
                );


                callback(null);

            }

        }

    }







    // =====================================
    // POST REQUEST
    // =====================================


    public IEnumerator Post(
        string url,
        string json,
        System.Action<string> callback
    )
    {


        using (UnityWebRequest request =
            new UnityWebRequest(
                url,
                "POST"
            ))
        {


            byte[] body =
                System.Text.Encoding.UTF8
                .GetBytes(json);



            request.uploadHandler =
                new UploadHandlerRaw(body);



            request.downloadHandler =
                new DownloadHandlerBuffer();



            request.SetRequestHeader(
                "Content-Type",
                "application/json"
            );



            yield return request.SendWebRequest();




            if (request.result ==
                UnityWebRequest.Result.Success)
            {

                callback(
                    request.downloadHandler.text
                );

            }
            else
            {

                Debug.LogError(
                    "API ERROR: "
                    + request.error
                );


                callback(null);

            }

        }

    }


}