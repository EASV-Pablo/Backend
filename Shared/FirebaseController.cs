using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;

namespace Backend.Shared
{
    public class FirebaseController
    {
        String destinationToken = "eqPlQeJFTxurQk8ggl8WCX:APA91bHCRmJcEKoXaFUdP5GMCigSiQmWYdHhi9QKzrcf4trFKyd1aJpsOKvR4t4gwhF6aC9lUda1CPhCXdrox_LBUPU38iJtelQk0ePcm_-dFK2G_xNNYO-3KpU_DsYb2UvVv2Cm4iFk";
        Message message;

        public FirebaseController()
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("credential.json")
            });
        }

        public void setMessage(Dictionary<string,string> data, String title, String body)
        {
            message = new Message()
            {
                Data = data,
                Token = destinationToken,
                Notification = new Notification()
                {
                    Title = title,
                    Body = body
                }
            };
        }

        public void sendNotification()
        {
            String response = FirebaseMessaging.DefaultInstance.SendAsync(message).Result;
        }

    }
}
