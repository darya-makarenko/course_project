using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Курсач
{
    class CheckUserInput
    {
        private const string path = @"C:\Users\darya\Documents\Visual Studio 2012\Projects\Курсач\Курсач\users.txt";

        private static bool findUserAndPassword(string username, string password)
        {
            using (StreamReader myStream = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = myStream.ReadLine()) != null) 
                {
                    if (line == username)
                    {
                        if ((line = myStream.ReadLine()) == password)
                        {
                            return true;
                        }
                    }
                    
                }
            }
            return false;
        }

        private static bool registerNewUser(string username, string password)
        {
            using (StreamReader myStream = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = myStream.ReadLine()) != null)
                {
                    if (line == username)
                    {
                        return false;
                    }
                }
            }

            using (StreamWriter myNextStream = new StreamWriter(path, true, System.Text.Encoding.Default))
            {
                myNextStream.WriteLine(username);
                myNextStream.WriteLine(password);
                File.Create(username);
            }

            return true;
        }

        public static bool Login(string Name, string Password, int type)
        {
            if (type == 1)
            {
                return findUserAndPassword(Name, Password);
            }
            else if (type == 0)
            {
                return registerNewUser(Name, Password);
            }
            return false;
        }
    }
}
