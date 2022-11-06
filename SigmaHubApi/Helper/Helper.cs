using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SigmaHubApi.Helper
{
    public class Helper
    {
        #region CheckSpecialCharacter and Number
        public static bool CheckCharacter(string Name)
        {
            if (Name == null)
            {
                return true;
            }
            else
            {
                char[] charName = Name.ToCharArray();
                char[] CharNameLen = new char[charName.Length];
                int i = 0;
                for (i = 0; i <= charName.Length - 1; i++)
                {
                    if (Char.IsLetter(charName[i]) || Char.IsWhiteSpace(charName[i]) || charName[i].ToString().Contains("'"))
                    {
                        if (i == charName.Length - 1)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
        }
        public static bool CheckDigit(string Name)
        {
            if (Name == null)
            {
                return true;
            }
            else
            {
                char[] charName = Name.ToCharArray();
                char[] CharNameLen = new char[charName.Length];
                int i = 0;
                for (i = 0; i <= charName.Length - 1; i++)
                {
                    if (Char.IsDigit(charName[i]) || charName[i].ToString().Contains(",") || charName[i].ToString().Contains("."))
                    {
                        if (i == charName.Length - 1)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
        }
        #endregion
        #region Validate Email
        public static bool validateEmail(string Email)
        {
            string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            //check Email validite
            if (Regex.IsMatch(Email, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
