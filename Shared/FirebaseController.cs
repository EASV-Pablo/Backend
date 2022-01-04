using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;

namespace Backend.Shared
{
    public class FirebaseController
    {
        String destinationToken = "dW_LZBVjQ0eJjCychkFJs4:APA91bFOL-UZq_-GdvrnKm1AJGEnxsKZVKkcLuW0RNKRhZGycsjA8cObA1hAD2tex0ksIs-a4_7qb1t0Dr35wlm-EWTBAn2k_N2bvnbOvVgRaCmCfbZz6Yy0SpuBYt9u-hYWc_Q_I-r0";
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
