using Firebase.Database;
using Firebase.Database.Query;

using reto_intercorp.Models;
using reto_intercorp.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace reto_intercorp.Util
{
    public class FirebaseUTIL
    {
        private static String auth = "DICS7itKqmku45xsO6UpeeLrpgCTu5WwrO0XtzR0"; // your app secret
        private static FirebaseClient firebaseClient = new FirebaseClient(
                          "https://retointercorp-d1926.firebaseio.com/",
                          new FirebaseOptions
                          {
                              AuthTokenAsyncFactory = () => Task.FromResult(auth)
                          });

        //public void StartAuth()
        //{

        //    try
        //    {

        //        // Initialize the default app
        //        String path_file = Path.Combine("App_Data", "tawaapp_a3228_firebase_adminsdk.json");

        //        var defaultApp = FirebaseApp.Create(new AppOptions()
        //        {
        //            Credential = GoogleCredential.FromFile(path_file),
        //        });

        //        // Retrieve services by passing the defaultApp variable...
        //        var defaultAuth = FirebaseAuth.GetAuth(defaultApp);

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        public async Task<bool> saveClientAsync(Client client)
        {
            bool register = false;

            try
            {

                var dino = await firebaseClient
                    .Child("clients")
                    .PostAsync(client);
                
                register = true; 

            }
            catch (Exception e)
            {
                throw;
            }

            return register;

        }

        public async Task<List<ClientViewModel>> getClientsAsync()
        {

            List<ClientViewModel> clients = new List<ClientViewModel>();

            try
            {

                var arrClients = await firebaseClient
                    .Child("clients")
                    .OrderByKey()
                    .OnceAsync<Client>();


                foreach (var client in arrClients)
                {
                    var o = client.Object;
                 
                    clients.Add(new ClientViewModel
                    {

                        ClientId = client.Object.ClientId,
                        Name = client.Object.Name,
                        Last_Name = client.Object.Last_Name,
                        Age = client.Object.Age,
                        Birthday = DateTime.ParseExact(client.Object.Birthday, "dd/MM/yyyy", null),

                    });

                }

            }
            catch (Exception e)
            {
                throw;
            }

            return clients;

        }

        //public async Task sendMessage_To_Specific_DeviceAsync(String registrationToken, String title, String body)
        //{

        //    try
        //    {

        //        var message = new Message()
        //        {
        //            Data = new Dictionary<string, string>()
        //            {
        //                { "Title", title },
        //                { "Body", body },
        //            },
        //            Token = registrationToken,
        //            Notification = new Notification()
        //            {
        //                Title = title,
        //                Body = body
        //            },
        //        };

        //        string response = await FirebaseMessaging.DefaultInstance.SendAsync(message).ConfigureAwait(false); ;

        //        Console.WriteLine("Successfully sent message: " + response);

        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }

        //}

        //public async Task sendMessage_To_Multiple_DeviceAsync(List<String> registrationTokens, String title, String body)
        //{

        //    var message = new MulticastMessage()
        //    {

        //        Tokens = registrationTokens,
        //        Data = new Dictionary<string, string>()
        //        {
        //            { "Title", title },
        //            { "Body", body },
        //        },
        //        Notification = new Notification()
        //        {
        //            Title = title,
        //            Body = body
        //        },

        //    };

        //    var response = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);

        //    Console.WriteLine($"{response.SuccessCount} messages were sent successfully");

        //    if (response.FailureCount > 0)
        //    {

        //        var failedTokens = new List<string>();

        //        for (var i = 0; i < response.Responses.Count; i++)
        //        {
        //            if (!response.Responses[i].IsSuccess)
        //            {
        //                // The order of responses corresponds to the order of the registration tokens.
        //                failedTokens.Add(registrationTokens[i]);
        //            }
        //        }

        //        Console.WriteLine($"List of tokens that caused failures: {failedTokens}");

        //    }

        //}

    }
}
