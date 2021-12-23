using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;

namespace Backend.Shared
{
    public class FirebaseController
    {
        String destinationToken = "fMAUaRkOQoyzkGprDK01tt:APA91bFV7GC_69ZXdDBPirGK2XkrRSDtOMwT4UM5cQ9VIZD7TzPsvOUHlwF5bgHxyzVSCx3ttg06RMOVdqL7553LsyI6tTGX3ux3ZWS8r5Cmkk1rpAWrxszZRajkkSqcN0MK3rGbDI8n";
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
