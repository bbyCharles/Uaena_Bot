﻿using System;
using System.IO;
using System.Windows.Forms;

namespace Uaena_Bot
{
    public class UserAccount
    {
        // PARAMETERS
        private string UserName { get; set; }
        private string Channel { get; set; }
        private string TwitchOAuth { get; set; }
        private bool Remember { get; set; }
        private bool AccountExist { get; set; }

        private string UserAccountHeaders = "UserName,Channel,TwitchOAuth,Remember";

        // File Locations 
        private string ConfigLocation = @".\Config\";
        private string UserAccountFileLocation = @".\Config\UserAccount.csv";

        // Default Constructor
        public UserAccount()
        {
            // Check for directory existence
            if (!Directory.Exists(ConfigLocation))
            {
                Directory.CreateDirectory(ConfigLocation);
            }

            // Check for LibraryFile existence
            if (!File.Exists(UserAccountFileLocation))
            {
                File.WriteAllText(UserAccountFileLocation, UserAccountHeaders);
                AccountExist = false;
            }
            else
            {
                AccountExist = LoadUserAccount();  
            }
        }

        // Full Constructor
        public UserAccount(string userName, string channel, string twitchOAuth, bool remember)
        {
            // PARAMETERS
            UserName = userName;
            Channel = channel;
            TwitchOAuth = twitchOAuth;
            Remember = remember;

            // Bypass
            AccountExist = true;
        }

        // METHODS

        // Produce a string containing all of the UserAccount's parameters 
        public string GetUserAccountCSVString()
        {
            string CSVString = string.Format("{0},{1},{2},{3}", UserName, Channel, TwitchOAuth, Remember);
            return CSVString;
        }

        // Check for Account Existence
        public bool GetAccountExistence()
        {
            return AccountExist;
        }

        // Save UserAccount to File
        public void SaveUserAccount()
        {
            string CSVString = GetUserAccountCSVString();
            File.WriteAllText(UserAccountFileLocation, CSVString);
        }

        // Load UserAccount from File
        public bool LoadUserAccount()
        {
            string UserAccountFile = File.ReadAllText(UserAccountFileLocation);

            if (!string.Equals(UserAccountFile,UserAccountHeaders))
            {
                string[] UserAccountInfo = UserAccountFile.Split(',');

                UserName = UserAccountInfo[0];
                Channel = UserAccountInfo[1];
                TwitchOAuth = UserAccountInfo[2];
                Remember = Convert.ToBoolean(UserAccountInfo[3]);

                return true;
            }
            else
            {
                return false;
            }
        }

        // Remove UserAccount in File
        public bool RemoveUserAccount()
        {
            // Check for LibraryFile existence
            if (File.Exists(UserAccountFileLocation))
            {
                File.Delete(UserAccountFileLocation);
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}